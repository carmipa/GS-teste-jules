// File: gsApi/controller/ContatosController.cs
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using gsApi.dto.request;
using gsApi.dto.response;
using gsApi.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gsApi.controller
{
    [ApiController]
    [Route("api/contatos")]
    public class ContatosController : ControllerBase
    {
        private readonly ILogger<ContatosController> _logger;

        // Em uma aplicação completa, você injetaria IContatoService aqui
        public ContatosController(ILogger<ContatosController> logger)
        {
            _logger = logger;
        }

        // POST: api/contatos
        [HttpPost]
        [ProducesResponseType(typeof(ContatoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CriarContato([FromBody] ContatoRequestDto contatoRequestDto)
        {
            _logger.LogInformation("Endpoint POST /api/contatos chamado.");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // TODO: Implementar criação (camada de serviço).
            // Simulação:
            var contatoSalvo = new ContatoResponseDto
            {
                IdContato = new Random().Next(1, 1000),
                Ddd = contatoRequestDto.Ddd,
                Telefone = contatoRequestDto.Telefone,
                Celular = contatoRequestDto.Celular,
                Whatsapp = contatoRequestDto.Whatsapp,
                Email = contatoRequestDto.Email,
                TipoContato = contatoRequestDto.TipoContato
            };
            return CreatedAtAction(nameof(BuscarContatoPorId), new { id = contatoSalvo.IdContato }, contatoSalvo);
        }

        // GET: api/contatos
        [HttpGet]
        [ProducesResponseType(typeof(List<ContatoResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarTodosContatos(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "email")
        {
            _logger.LogInformation("Endpoint GET /api/contatos chamado com pageNumber: {PageNumber}, pageSize: {PageSize}, sortBy: {SortBy}", pageNumber, pageSize, sortBy);
            // TODO: Implementar listagem e paginação (camada de serviço).
            var contatosExemplo = new List<ContatoResponseDto>
            {
                new ContatoResponseDto { IdContato = 1, Email = "contato1@example.com", TipoContato = "Comercial", Ddd="11", Telefone="11112222"},
                new ContatoResponseDto { IdContato = 2, Email = "contato2@example.com", TipoContato = "Pessoal", Ddd="21", Telefone="33334444"}
            };
            var totalCount = contatosExemplo.Count;
            var items = contatosExemplo.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var pagedResponse = new { Content = items, TotalElements = totalCount, PageNumber = pageNumber, PageSize = pageSize, TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize) };

            return Ok(pagedResponse);
        }

        // GET: api/contatos/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ContatoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BuscarContatoPorId(long id)
        {
            _logger.LogInformation("Endpoint GET /api/contatos/{Id} chamado.", id);
            // TODO: Implementar busca por ID (camada de serviço).
            if (id == 1) // Simulação
            {
                return Ok(new ContatoResponseDto { IdContato = 1, Email = "contato1@example.com", TipoContato = "Comercial" });
            }
            // throw new RecursoNaoEncontradoException($"Contato com ID {id} não encontrado.");
            return NotFound(new { message = $"Contato com ID {id} não encontrado." });
        }

        // GET: api/contatos/email/{email}
        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(ContatoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BuscarContatoPorEmail(string email)
        {
            _logger.LogInformation("Endpoint GET /api/contatos/email/{Email} chamado.", email);
            // TODO: Implementar busca por email (camada de serviço).
            if (email.Equals("contato1@example.com", StringComparison.OrdinalIgnoreCase)) // Simulação
            {
                return Ok(new ContatoResponseDto { IdContato = 1, Email = "contato1@example.com", TipoContato = "Comercial" });
            }
            // throw new RecursoNaoEncontradoException($"Contato com email {email} não encontrado.");
            return NotFound(new { message = $"Contato com email {email} não encontrado." });
        }

        // PUT: api/contatos/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ContatoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarContato(long id, [FromBody] ContatoRequestDto contatoRequestDto)
        {
            _logger.LogInformation("Endpoint PUT /api/contatos/{Id} chamado.", id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // TODO: Implementar atualização (camada de serviço).
            // Simulação:
            if (id != 1) return NotFound(new { message = $"Contato com ID {id} não encontrado para atualização." });

            var contatoAtualizado = new ContatoResponseDto
            {
                IdContato = id,
                Ddd = contatoRequestDto.Ddd,
                Telefone = contatoRequestDto.Telefone,
                Celular = contatoRequestDto.Celular,
                Whatsapp = contatoRequestDto.Whatsapp,
                Email = contatoRequestDto.Email,
                TipoContato = contatoRequestDto.TipoContato
            };
            return Ok(contatoAtualizado);
        }

        // DELETE: api/contatos/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletarContato(long id)
        {
            _logger.LogInformation("Endpoint DELETE /api/contatos/{Id} chamado.", id);
            // TODO: Implementar deleção (camada de serviço).
            // Simulação:
            if (id != 1) return NotFound(new { message = $"Contato com ID {id} não encontrado para deleção." });

            return NoContent();
        }
    }
}
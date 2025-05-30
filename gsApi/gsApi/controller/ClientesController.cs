// File: gsApi/controller/ClientesController.cs
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using gsApi.dto.request;
using gsApi.dto.response;
using gsApi.exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace gsApi.controller
{
    /// <summary>
    /// Gerencia as operações relacionadas a Clientes (Usuários).
    /// </summary>
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(ILogger<ClientesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Lista todos os clientes de forma paginada.
        /// </summary>
        /// <param name="pageNumber">Número da página (inicia em 1).</param>
        /// <param name="pageSize">Tamanho da página.</param>
        /// <param name="sortBy">Campo para ordenação (padrão: nome).</param>
        /// <response code="200">Retorna a lista paginada de clientes.</response>
        /// <response code="500">Se ocorrer um erro interno no servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)] // Usando 'object' para o tipo de resposta paginada anônima
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListarTodosClientes(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string sortBy = "nome")
        {
            _logger.LogInformation("Endpoint GET /api/clientes chamado com pageNumber: {PageNumber}, pageSize: {PageSize}, sortBy: {SortBy}", pageNumber, pageSize, sortBy);
            // TODO: Implementar lógica real de busca e paginação (camada de serviço).

            var clientesExemplo = new List<ClienteResponseDto>
            {
                new ClienteResponseDto { IdCliente = 1, Nome = "Ana", Sobrenome = "Silva", Documento = "11122233344", DataNascimento = "1990-01-01", Contatos = new List<ContatoResponseDto>(), Enderecos = new List<EnderecoResponseDto>() },
                new ClienteResponseDto { IdCliente = 2, Nome = "Bruno", Sobrenome = "Costa", Documento = "55566677788", DataNascimento = "1985-05-15", Contatos = new List<ContatoResponseDto>(), Enderecos = new List<EnderecoResponseDto>() }
            };
            var totalCount = clientesExemplo.Count;
            var items = clientesExemplo.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var pagedResponse = new { Content = items, TotalElements = totalCount, PageNumber = pageNumber, PageSize = pageSize, TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize) };

            return Ok(pagedResponse);
        }

        /// <summary>
        /// Busca um cliente específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do cliente.</param>
        /// <response code="200">Retorna o cliente encontrado.</response>
        /// <response code="404">Se o cliente não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro interno.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClienteResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarClientePorId(long id)
        {
            _logger.LogInformation("Endpoint GET /api/clientes/{Id} chamado.", id);
            // TODO: Implementar busca por ID (camada de serviço).
            if (id == 1)
            {
                var clienteExemplo = new ClienteResponseDto { IdCliente = 1, Nome = "Ana", Sobrenome = "Silva", Documento = "11122233344", DataNascimento = "1990-01-01", Contatos = new List<ContatoResponseDto>(), Enderecos = new List<EnderecoResponseDto>() };
                return Ok(clienteExemplo);
            }
            // Exemplo de como seria com exceção:
            // try { /* ... lógica do serviço ... */ } catch (RecursoNaoEncontradoException ex) { return NotFound(new { message = ex.Message }); }
            return NotFound(new { message = $"Cliente com ID {id} não encontrado." });
        }

        /// <summary>
        /// Busca um cliente pelo seu Documento (CPF/CNPJ).
        /// </summary>
        /// <param name="documento">O número do documento do cliente.</param>
        /// <response code="200">Retorna o cliente encontrado.</response>
        /// <response code="404">Se o cliente não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro interno.</response>
        [HttpGet("documento/{documento}")]
        [ProducesResponseType(typeof(ClienteResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarClientePorDocumento(string documento)
        {
            _logger.LogInformation("Endpoint GET /api/clientes/documento/{Documento} chamado.", documento);
            // TODO: Implementar busca por documento.
            if (documento == "11122233344")
            {
                var clienteExemplo = new ClienteResponseDto { IdCliente = 1, Nome = "Ana", Sobrenome = "Silva", Documento = "11122233344", DataNascimento = "1990-01-01", Contatos = new List<ContatoResponseDto>(), Enderecos = new List<EnderecoResponseDto>() };
                return Ok(clienteExemplo);
            }
            return NotFound(new { message = $"Cliente com documento {documento} não encontrado." });
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="clienteRequestDto">Os dados do cliente a ser criado.</param>
        /// <response code="201">Retorna o cliente recém-criado.</response>
        /// <response code="400">Se os dados da requisição forem inválidos.</response>
        /// <response code="500">Se ocorrer um erro interno.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ClienteResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CriarCliente([FromBody] ClienteRequestDto clienteRequestDto)
        {
            _logger.LogInformation("Endpoint POST /api/clientes chamado.");
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState); // Retorna ValidationProblemDetails
            }
            // TODO: Implementar criação.
            var clienteSalvo = new ClienteResponseDto
            {
                IdCliente = new Random().Next(100, 1000),
                Nome = clienteRequestDto.Nome,
                Sobrenome = clienteRequestDto.Sobrenome,
                DataNascimento = clienteRequestDto.DataNascimento,
                Documento = clienteRequestDto.Documento,
                Contatos = new List<ContatoResponseDto>(),
                Enderecos = new List<EnderecoResponseDto>()
            };
            return CreatedAtAction(nameof(BuscarClientePorId), new { id = clienteSalvo.IdCliente }, clienteSalvo);
        }

        /// <summary>
        /// Atualiza um cliente existente.
        /// </summary>
        /// <param name="id">O ID do cliente a ser atualizado.</param>
        /// <param name="clienteRequestDto">Os novos dados para o cliente.</param>
        /// <response code="200">Retorna o cliente atualizado.</response>
        /// <response code="400">Se os dados da requisição forem inválidos.</response>
        /// <response code="404">Se o cliente não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro interno.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ClienteResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarCliente(long id, [FromBody] ClienteRequestDto clienteRequestDto)
        {
            _logger.LogInformation("Endpoint PUT /api/clientes/{Id} chamado.", id);
            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }
            // TODO: Implementar atualização.
            if (id != 10) return NotFound(new { message = $"Cliente com ID {id} não encontrado." });

            var clienteAtualizado = new ClienteResponseDto
            {
                IdCliente = id,
                Nome = clienteRequestDto.Nome,
                Sobrenome = clienteRequestDto.Sobrenome,
                DataNascimento = clienteRequestDto.DataNascimento,
                Documento = clienteRequestDto.Documento,
                Contatos = new List<ContatoResponseDto>(), // Simulação
                Enderecos = new List<EnderecoResponseDto>()  // Simulação
            };
            return Ok(clienteAtualizado);
        }

        /// <summary>
        /// Deleta um cliente pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do cliente a ser deletado.</param>
        /// <response code="204">Cliente deletado com sucesso.</response>
        /// <response code="404">Se o cliente não for encontrado.</response>
        /// <response code="500">Se ocorrer um erro interno.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletarCliente(long id)
        {
            _logger.LogInformation("Endpoint DELETE /api/clientes/{Id} chamado.", id);
            // TODO: Implementar deleção.
            if (id != 10) return NotFound(new { message = $"Cliente com ID {id} não encontrado." });

            return NoContent();
        }

        /// <summary>
        /// Pesquisa clientes por nome ou sobrenome.
        /// </summary>
        /// <param name="termo">Termo para buscar no nome ou sobrenome do cliente.</param>
        /// <param name="pageNumber">Número da página.</param>
        /// <param name="pageSize">Tamanho da página.</param>
        /// <response code="200">Retorna a lista paginada de clientes encontrados.</response>
        /// <response code="400">Se o termo de pesquisa for inválido.</response>
        /// <response code="500">Se ocorrer um erro interno.</response>
        [HttpGet("pesquisar")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)] // Resposta paginada anônima
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PesquisarClientes(
            [FromQuery, Required(ErrorMessage = "O termo de pesquisa é obrigatório.")] string termo,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Endpoint GET /api/clientes/pesquisar chamado com termo: {Termo}", termo);
            // TODO: Implementar lógica de pesquisa.
            var clientesExemplo = new List<ClienteResponseDto>
            {
                new ClienteResponseDto { IdCliente = 1, Nome = $"Cliente {termo} Encontrado", Sobrenome = "Teste", Documento = "99988877766", Contatos = new List<ContatoResponseDto>(), Enderecos = new List<EnderecoResponseDto>() }
            };
            var totalCount = clientesExemplo.Count;
            var items = clientesExemplo.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var pagedResponse = new { Content = items, TotalElements = totalCount, PageNumber = pageNumber, PageSize = pageSize, TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize) };

            return Ok(pagedResponse);
        }
    }
}
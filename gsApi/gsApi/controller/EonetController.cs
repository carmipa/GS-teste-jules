// File: gsApi/controller/EonetController.cs
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeuProjetoNET.DTOs.Request;
using SeuProjetoNET.DTOs.Response; // Para EonetResponseDto e os DTOs da NASA
using SeuProjetoNET.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SeuProjetoNET.Controller
{
    [ApiController]
    [Route("api/eonet")]
    public class EonetController : ControllerBase
    {
        private readonly ILogger<EonetController> _logger;

        // Em uma aplicação completa, você injetaria IEonetService e INasaEonetClient aqui
        public EonetController(ILogger<EonetController> logger)
        {
            _logger = logger;
        }

        // GET: api/eonet (Lista eventos EONET locais)
        [HttpGet]
        [ProducesResponseType(typeof(List<EonetResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ListarTodosEventosEonetLocalmente(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            // No Java era Sort.Direction.DESC para data. Aqui sortBy pode ser "data" e a direção controlada.
            [FromQuery] string sortBy = "data",
            [FromQuery] string sortDirection = "desc")
        {
            _logger.LogInformation("Endpoint GET /api/eonet (listar locais) chamado.");
            // TODO: Implementar listagem local (camada de serviço).
            var eventosExemplo = new List<EonetResponseDto>
            {
                new EonetResponseDto { IdEonet = 1, EonetIdApi = "NASA_EONET_123", Data = DateTimeOffset.Now.AddDays(-1), Json = "{ \"title\": \"Evento Exemplo 1\" }" },
                new EonetResponseDto { IdEonet = 2, EonetIdApi = "NASA_EONET_456", Data = DateTimeOffset.Now.AddDays(-2), Json = "{ \"title\": \"Evento Exemplo 2\" }" }
            };
            var totalCount = eventosExemplo.Count;
            var items = eventosExemplo.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var pagedResponse = new { Content = items, TotalElements = totalCount, PageNumber = pageNumber, PageSize = pageSize, TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize) };

            return Ok(pagedResponse);
        }

        // GET: api/eonet/{idInterno} (Busca evento local por ID interno)
        [HttpGet("{idInterno}")]
        [ProducesResponseType(typeof(EonetResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BuscarEventoLocalPorIdInterno(long idInterno)
        {
            _logger.LogInformation("Endpoint GET /api/eonet/{IdInterno} chamado.", idInterno);
            // TODO: Implementar busca local por ID (camada de serviço).
            if (idInterno == 1) // Simulação
            {
                return Ok(new EonetResponseDto { IdEonet = 1, EonetIdApi = "NASA_EONET_123", Data = DateTimeOffset.Now.AddDays(-1) });
            }
            return NotFound(new { message = $"Evento EONET local com ID interno {idInterno} não encontrado." });
        }

        // GET: api/eonet/api-id/{eonetApiId} (Busca evento local por ID da API NASA)
        [HttpGet("api-id/{eonetApiId}")]
        [ProducesResponseType(typeof(EonetResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BuscarEventoLocalPorEonetApiId(string eonetApiId)
        {
            _logger.LogInformation("Endpoint GET /api/eonet/api-id/{EonetApiId} chamado.", eonetApiId);
            // TODO: Implementar busca local por EonetApiId (camada de serviço).
            if (eonetApiId == "NASA_EONET_123") // Simulação
            {
                return Ok(new EonetResponseDto { IdEonet = 1, EonetIdApi = "NASA_EONET_123", Data = DateTimeOffset.Now.AddDays(-1) });
            }
            return NotFound(new { message = $"Evento EONET local com ID da API {eonetApiId} não encontrado." });
        }

        // POST: api/eonet (Salva manualmente um evento EONET local)
        [HttpPost]
        [ProducesResponseType(typeof(EonetResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SalvarEventoEonetManualmente([FromBody] EonetRequestDto eonetRequestDto)
        {
            _logger.LogInformation("Endpoint POST /api/eonet (salvar manual) chamado.");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // TODO: Implementar salvamento manual (camada de serviço).
            var eventoSalvo = new EonetResponseDto
            {
                IdEonet = new Random().Next(1, 100),
                EonetIdApi = eonetRequestDto.EonetIdApi,
                Data = eonetRequestDto.Data,
                Json = eonetRequestDto.Json
            };
            return CreatedAtAction(nameof(BuscarEventoLocalPorIdInterno), new { idInterno = eventoSalvo.IdEonet }, eventoSalvo);
        }

        // PUT: api/eonet/{idInterno} (Atualiza manualmente um evento EONET local)
        [HttpPut("{idInterno}")]
        [ProducesResponseType(typeof(EonetResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarEventoEonetManualmente(long idInterno, [FromBody] EonetRequestDto eonetRequestDto)
        {
            _logger.LogInformation("Endpoint PUT /api/eonet/{IdInterno} (atualizar manual) chamado.", idInterno);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // TODO: Implementar atualização manual (camada de serviço).
            // Simulação
            if (idInterno != 1) return NotFound(new { message = "Evento não encontrado para atualização." });
            var eventoAtualizado = new EonetResponseDto { IdEonet = idInterno, EonetIdApi = eonetRequestDto.EonetIdApi, Data = eonetRequestDto.Data, Json = eonetRequestDto.Json };
            return Ok(eventoAtualizado);
        }

        // DELETE: api/eonet/{idInterno} (Deleta um evento EONET local)
        [HttpDelete("{idInterno}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletarEventoEonetLocal(long idInterno)
        {
            _logger.LogInformation("Endpoint DELETE /api/eonet/{IdInterno} chamado.", idInterno);
            // TODO: Implementar deleção (camada de serviço).
            // Simulação
            if (idInterno != 1) return NotFound(new { message = "Evento não encontrado para deleção." });
            return NoContent();
        }

        // GET: api/eonet/por-data (Busca eventos EONET locais por intervalo de data)
        [HttpGet("por-data")]
        [ProducesResponseType(typeof(List<EonetResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BuscarEventosLocaisPorIntervaloDeData(
            [FromQuery, Required] DateTimeOffset dataInicial,
            [FromQuery, Required] DateTimeOffset dataFinal)
        {
            _logger.LogInformation("Endpoint GET /api/eonet/por-data chamado.");
            if (dataInicial > dataFinal)
            {
                return BadRequest(new { message = "Data inicial não pode ser posterior à data final." });
            }
            // TODO: Implementar busca local por data (camada de serviço).
            return Ok(new List<EonetResponseDto>());
        }

        // POST: api/eonet/nasa/sincronizar
        [HttpPost("nasa/sincronizar")]
        [ProducesResponseType(typeof(List<EonetResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<IActionResult> SincronizarEventosDaNasa(
        [FromQuery] int? limit,
        [FromQuery] int? days,
        [FromQuery] string? source,
        [FromQuery] string? status = "open") // Parâmetro opcional movido para o final
        {
            _logger.LogInformation("Endpoint POST /api/eonet/nasa/sincronizar chamado.");
            // TODO: Implementar sincronização (chamar cliente NASA, salvar com serviço).
            // Simulação de resposta:
            return Ok(new List<EonetResponseDto> { new EonetResponseDto { IdEonet = 100, EonetIdApi = "SYNCED_EVENT_01", Data = DateTimeOffset.UtcNow } });
        }

        // GET: api/eonet/nasa/proximos
        [HttpGet("nasa/proximos")]
        [ProducesResponseType(typeof(List<NasaEonetEventDto>), StatusCodes.Status200OK)] // Usa DTO externo da NASA
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BuscarEventosDaNasa(
            [FromQuery] double? latitude,
            [FromQuery] double? longitude,
            [FromQuery] double? raioKm,
            [FromQuery] string? startDate, // Formato YYYY-MM-DD
            [FromQuery] string? endDate,   // Formato YYYY-MM-DD
            [FromQuery] int? limit,
            [FromQuery] int? days,
            [FromQuery] string? status, // "open", "closed" ou vazio/null para todos
            [FromQuery] string? source)
        {
            _logger.LogInformation("Endpoint GET /api/eonet/nasa/proximos chamado.");
            // TODO: Implementar busca na API da NASA (camada de cliente HTTP).
            // Simulação de resposta:
            var eventos = new List<NasaEonetEventDto>();
            // Adicionar eventos de exemplo se necessário para teste de contrato.
            // Ex: eventos.Add(new NasaEonetEventDto { Id = "NASA_API_EVT_1", Title = "Exemplo Evento NASA"});

            if (!eventos.Any()) return NoContent();
            return Ok(eventos);
        }
    }
}
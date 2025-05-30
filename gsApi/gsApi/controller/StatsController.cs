// File: gsApi/controller/StatsController.cs
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeuProjetoNET.DTOs.Response; // Para CategoryCountDto
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SeuProjetoNET.Controller
{
    [ApiController]
    [Route("api/stats")]
    public class StatsController : ControllerBase
    {
        private readonly ILogger<StatsController> _logger;

        // Em uma aplicação completa, você injetaria um serviço que calcula as estatísticas (provavelmente usando EonetService ou um StatsService dedicado)
        public StatsController(ILogger<StatsController> logger)
        {
            _logger = logger;
        }

        // GET: api/stats/eonet/count-by-category
        [HttpGet("eonet/count-by-category")]
        [ProducesResponseType(typeof(List<CategoryCountDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetEonetCountByCategory(
            [FromQuery, Required, Range(1, int.MaxValue)] int days = 365) // Parâmetro 'days' do Java
        {
            _logger.LogInformation("Endpoint GET /api/stats/eonet/count-by-category chamado com days: {Days}", days);
            if (days <= 0)
            {
                return BadRequest(new { message = "O parâmetro 'days' deve ser um número positivo." });
            }
            // TODO: Implementar lógica para buscar e calcular estatísticas (camada de serviço).
            // Simulação de resposta:
            var statsExemplo = new List<CategoryCountDto>
            {
                new CategoryCountDto("Incêndios Florestais", 15),
                new CategoryCountDto("Tempestades Severas", 25),
                new CategoryCountDto("Inundações", 10)
            };
            return Ok(statsExemplo);
        }
    }
}
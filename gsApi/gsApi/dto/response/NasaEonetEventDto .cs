// File: gsApi/dto/response/NasaEonetEventDto.cs
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SeuProjetoNET.DTOs.Response
{
    public class NasaEonetEventDto
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; } // Permanece anulável

        [JsonPropertyName("link")]
        public required string Link { get; set; }

        [JsonPropertyName("categories")]
        public required List<NasaEonetCategoryDto> Categories { get; set; }

        [JsonPropertyName("sources")]
        public List<NasaEonetSourceDto>? Sources { get; set; } // Permanece anulável

        [JsonPropertyName("geometry")]
        public required List<NasaEonetGeometryDto> Geometry { get; set; }
    }
}
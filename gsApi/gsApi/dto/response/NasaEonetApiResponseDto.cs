// File: SeuProjetoNET/DTOs/Response/NasaEonetApiResponseDto.cs
using System.Text.Json.Serialization;

namespace SeuProjetoNET.DTOs.Response
{
    public class NasaEonetApiResponseDto
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("events")]
        public List<NasaEonetEventDto> Events { get; set; }
    }
}
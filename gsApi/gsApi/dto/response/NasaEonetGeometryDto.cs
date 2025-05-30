// File: SeuProjetoNET/DTOs/Response/NasaEonetGeometryDto.cs
using System.Text.Json.Serialization;

namespace SeuProjetoNET.DTOs.Response
{
    public class NasaEonetGeometryDto
    {
        [JsonPropertyName("date")]
        public DateTimeOffset Date { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("coordinates")]
        public object Coordinates { get; set; }
    }
}
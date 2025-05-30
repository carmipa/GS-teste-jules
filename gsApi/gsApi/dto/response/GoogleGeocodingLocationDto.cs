// File: SeuProjetoNET/DTOs/Response/GoogleGeocodingLocationDto.cs
using System.Text.Json.Serialization;

namespace SeuProjetoNET.DTOs.Response
{
    public class GoogleGeocodingLocationDto
    {
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lng")]
        public double Longitude { get; set; }
    }
}
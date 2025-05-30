// File: SeuProjetoNET/DTOs/Response/GoogleGeocodingGeometryDto.cs
using System.Text.Json.Serialization;

namespace SeuProjetoNET.DTOs.Response
{
    public class GoogleGeocodingGeometryDto
    {
        [JsonPropertyName("location")]
        public GoogleGeocodingLocationDto Location { get; set; }
    }
}
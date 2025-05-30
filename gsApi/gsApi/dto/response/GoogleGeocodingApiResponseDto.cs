// File: SeuProjetoNET/DTOs/Response/GoogleGeocodingApiResponseDto.cs
using System.Text.Json.Serialization;

namespace SeuProjetoNET.DTOs.Response
{
    public class GoogleGeocodingApiResponseDto // Esta é a classe que estava dando o erro na imagem
    {
        [JsonPropertyName("results")]
        public List<GoogleGeocodingResultDto> Results { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("error_message")]
        public string? ErrorMessage { get; set; }
    }
}
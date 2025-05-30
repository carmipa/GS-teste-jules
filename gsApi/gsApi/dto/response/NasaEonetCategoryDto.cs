// File: SeuProjetoNET/DTOs/Response/NasaEonetCategoryDto.cs
using System.Text.Json.Serialization;

namespace SeuProjetoNET.DTOs.Response
{
    public class NasaEonetCategoryDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }
    }
}
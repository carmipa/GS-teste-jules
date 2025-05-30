// File: SeuProjetoNET/DTOs/Response/GoogleGeocodingResultDto.cs
using System.Text.Json.Serialization;

namespace SeuProjetoNET.DTOs.Response
{
    public class GoogleGeocodingResultDto
    {
        [JsonPropertyName("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonPropertyName("geometry")]
        public GoogleGeocodingGeometryDto Geometry { get; set; }

        [JsonPropertyName("place_id")]
        public string PlaceId { get; set; }

        [JsonPropertyName("types")]
        public List<string> Types { get; set; }
    }
}
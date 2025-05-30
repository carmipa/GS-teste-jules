// File: SeuProjetoNET/DTOs/Response/EonetResponseDto.cs
namespace SeuProjetoNET.DTOs.Response
{
    public class EonetResponseDto
    {
        public long IdEonet { get; set; }
        public string? Json { get; set; }
        public DateTimeOffset? Data { get; set; }
        public string EonetIdApi { get; set; }
    }
}
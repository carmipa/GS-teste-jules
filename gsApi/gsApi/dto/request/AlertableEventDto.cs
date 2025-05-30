// File: gsApi/dto/request/AlertableEventDto.cs
namespace SeuProjetoNET.DTOs.Request
{
    public class AlertableEventDto
    {
        public string? EventId { get; set; }
        public string? Title { get; set; }
        public string? EventDate { get; set; }
        public string? Link { get; set; }
        public string? Description { get; set; }
    }
}
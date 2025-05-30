// File: gsApi/dto/response/ContatoResponseDto.cs
namespace SeuProjetoNET.DTOs.Response
{
    public class ContatoResponseDto
    {
        public long IdContato { get; set; }
        public required string Ddd { get; set; }
        public required string Telefone { get; set; }
        public required string Celular { get; set; }
        public required string Whatsapp { get; set; }
        public required string Email { get; set; }
        public required string TipoContato { get; set; }
    }
}
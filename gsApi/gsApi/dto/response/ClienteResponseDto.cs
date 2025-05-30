// File: gsApi/dto/response/ClienteResponseDto.cs
using System.Collections.Generic;

namespace SeuProjetoNET.DTOs.Response
{
    public class ClienteResponseDto
    {
        public long IdCliente { get; set; }
        public required string Nome { get; set; }
        public required string Sobrenome { get; set; }
        public required string DataNascimento { get; set; }
        public required string Documento { get; set; }
        public ICollection<ContatoResponseDto> Contatos { get; set; } = new List<ContatoResponseDto>();
        public ICollection<EnderecoResponseDto> Enderecos { get; set; } = new List<EnderecoResponseDto>();
    }
}
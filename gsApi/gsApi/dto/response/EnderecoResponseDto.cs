// File: SeuProjetoNET/DTOs/Response/EnderecoResponseDto.cs
namespace SeuProjetoNET.DTOs.Response
{
    public class EnderecoResponseDto
    {
        public long IdEndereco { get; set; }
        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Complemento { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
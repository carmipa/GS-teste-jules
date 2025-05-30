// File: gsApi/dto/request/ContatoRequestDto.cs
using System.ComponentModel.DataAnnotations;

namespace SeuProjetoNET.DTOs.Request
{
    public class ContatoRequestDto
    {
        [Required(ErrorMessage = "O DDD não pode estar em branco.")]
        [StringLength(3, MinimumLength = 2, ErrorMessage = "O DDD deve ter entre 2 e 3 caracteres.")]
        public required string Ddd { get; set; }

        [Required(ErrorMessage = "O telefone não pode estar em branco.")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "O telefone deve ter entre 8 e 15 caracteres.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "O telefone deve conter apenas números.")]
        public required string Telefone { get; set; }

        [Required(ErrorMessage = "O celular não pode estar em branco.")] // [Required] já estava implícito no Java para String
        [StringLength(15, MinimumLength = 9, ErrorMessage = "O celular deve ter entre 9 e 15 caracteres.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "O celular deve conter apenas números.")]
        public required string Celular { get; set; }

        [Required(ErrorMessage = "O WhatsApp não pode estar em branco.")]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "O WhatsApp deve ter entre 9 e 15 caracteres.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "O WhatsApp deve conter apenas números.")]
        public required string Whatsapp { get; set; }

        [Required(ErrorMessage = "O e-mail não pode estar em branco.")]
        [EmailAddress(ErrorMessage = "O e-mail deve ser válido.")]
        [StringLength(255, ErrorMessage = "O e-mail não pode exceder 255 caracteres.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O tipo de contato não pode estar em branco.")]
        [StringLength(50, ErrorMessage = "O tipo de contato não pode exceder 50 caracteres.")]
        public required string TipoContato { get; set; }
    }
}
// File: gsApi/dto/request/UserAlertRequestDto.cs
using System.ComponentModel.DataAnnotations;

namespace SeuProjetoNET.DTOs.Request
{
    public class UserAlertRequestDto
    {
        [Required(ErrorMessage = "O ID do usuário não pode ser nulo.")]
        public long UserId { get; set; } // Tipos de valor não precisam de 'required' para nulidade

        [Required(ErrorMessage = "Os detalhes do evento não podem ser nulos.")]
        public required AlertableEventDto EventDetails { get; set; }
    }
}
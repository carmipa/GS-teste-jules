// File: gsApi/model/Contato.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; // Adicionado para ICollection
using System.Text.Json.Serialization;

namespace SeuProjetoNET.Models // Certifique-se que o namespace corresponde ao seu projeto
{
    [Table("tb_contato3")]
    public class Contato
    {
        [Key]
        [Column("id_contato")]
        public long IdContato { get; set; }

        [Required(ErrorMessage = "O DDD não pode estar em branco.")]
        [StringLength(3)]
        [Column("ddd")]
        public required string Ddd { get; set; }

        [Required(ErrorMessage = "O telefone não pode estar em branco.")]
        [StringLength(15)]
        [Column("telefone")]
        public required string Telefone { get; set; }

        [Required(ErrorMessage = "O celular não pode estar em branco.")]
        [StringLength(15)]
        [Column("celular")]
        public required string Celular { get; set; }

        [Required(ErrorMessage = "O WhatsApp não pode estar em branco.")]
        [StringLength(15)]
        [Column("whatsapp")]
        public required string Whatsapp { get; set; }

        [Required(ErrorMessage = "O e-mail não pode estar em branco.")]
        [StringLength(255)]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [Column("email")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O tipo de contato não pode estar em branco.")]
        [StringLength(50)]
        [Column("tipo_contato")]
        public required string TipoContato { get; set; }

        [JsonIgnore]
        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
    }
}
// File: gsApi/model/EonetEvent.cs
using System; // Para DateTimeOffset
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; // Para ICollection
using System.Text.Json.Serialization;

namespace SeuProjetoNET.Models // Certifique-se que o namespace corresponde ao seu projeto
{
    [Table("tb_eonet3")]
    public class EonetEvent
    {
        [Key]
        [Column("id_eonet")]
        public long IdEonet { get; set; }

        [Column("json", TypeName = "CLOB")]
        public string? Json { get; set; } // Pode ser nulo conforme DDL

        [Column("data")]
        public DateTimeOffset? Data { get; set; } // Pode ser nulo conforme DDL (TIMESTAMP WITH LOCAL TIME ZONE)

        [Required(ErrorMessage = "O ID da API EONET é obrigatório.")]
        [StringLength(50)]
        [Column("eonet_id")]
        public required string EonetIdApi { get; set; }

        [JsonIgnore]
        public virtual ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
    }
}
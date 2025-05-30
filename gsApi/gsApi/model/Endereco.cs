// File: gsApi/model/Endereco.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; // Adicionado para ICollection
using System.Text.Json.Serialization;

namespace SeuProjetoNET.Models // Certifique-se que o namespace corresponde ao seu projeto
{
    [Table("tb_endereco3")]
    public class Endereco
    {
        [Key]
        [Column("id_endereco")]
        public long IdEndereco { get; set; }

        [Required(ErrorMessage = "O CEP não pode estar em branco.")]
        [StringLength(9)] // Ex: "XXXXX-XXX" ou "XXXXXXXX"
        [Column("cep")]
        public required string Cep { get; set; }

        [Required(ErrorMessage = "O número do endereço é obrigatório.")]
        [Column("numero")] // DDL: NUMBER(5)
        public int Numero { get; set; }

        [Required(ErrorMessage = "O logradouro não pode estar em branco.")]
        [StringLength(255)]
        [Column("logradouro")]
        public required string Logradouro { get; set; }

        [Required(ErrorMessage = "O bairro não pode estar em branco.")]
        [StringLength(255)]
        [Column("bairro")]
        public required string Bairro { get; set; }

        [Required(ErrorMessage = "A localidade (cidade) não pode estar em branco.")]
        [StringLength(100)]
        [Column("localidade")]
        public required string Localidade { get; set; }

        [Required(ErrorMessage = "A UF não pode estar em branco.")]
        [StringLength(2)]
        [Column("uf")]
        public required string Uf { get; set; }

        [Required(ErrorMessage = "O complemento é obrigatório.")] // DDL é NOT NULL
        [StringLength(255)]
        [Column("complemento")]
        public required string Complemento { get; set; }

        [Required(ErrorMessage = "Latitude é obrigatória.")]
        [Column("latitude", TypeName = "NUMBER(10,7)")]
        public double Latitude { get; set; }

        [Required(ErrorMessage = "Longitude é obrigatória.")]
        [Column("longitude", TypeName = "NUMBER(10,7)")]
        public double Longitude { get; set; }

        [JsonIgnore]
        public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
        public virtual ICollection<EonetEvent> EventosEonet { get; set; } = new List<EonetEvent>();
    }
}
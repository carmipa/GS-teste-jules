// File: gsApi/model/Cliente.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic; // Adicionado para ICollection

namespace SeuProjetoNET.Models // Certifique-se que o namespace corresponde ao seu projeto
{
    [Table("tb_cliente3")]
    public class Cliente
    {
        [Key]
        [Column("id_cliente")]
        public long IdCliente { get; set; }

        [Required(ErrorMessage = "O nome não pode estar em branco.")]
        [StringLength(100)]
        [Column("nome")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "O sobrenome não pode estar em branco.")]
        [StringLength(100)]
        [Column("sobrenome")]
        public required string Sobrenome { get; set; }

        [Required(ErrorMessage = "A data de nascimento não pode estar em branco.")]
        [StringLength(10)] // Formato YYYY-MM-DD ou DD/MM/YYYY
        [Column("data_nascimento")]
        public required string DataNascimento { get; set; }

        [Required(ErrorMessage = "O documento não pode estar em branco.")]
        [StringLength(18)] // Ajuste o tamanho se necessário para CPF/CNPJ formatados
        [Column("documento")]
        public required string Documento { get; set; }

        public virtual ICollection<Contato> Contatos { get; set; } = new List<Contato>();
        public virtual ICollection<Endereco> Enderecos { get; set; } = new List<Endereco>();
    }
}
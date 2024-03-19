using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Entities
{
    [Table("Produtos")]
    public class Product : Notifies
    {
        [Key]
        public int ProductId { get; set; } // Código do produto (sequencial e não nulo)

        [Required]
        public string Description { get; set; } // Descrição do produto (não nulo)

        public bool Active { get; set; } // Situação do produto (Ativo ou Inativo)

        [Column(TypeName = "Date")]
        public DateTime ManufactureDate { get; set; } // Data de fabricação

        [Column(TypeName = "Date")]
        public DateTime ExpiryDate { get; set; } // Data de validade

        public int SupplierId { get; set; } // Código do fornecedor

        public string SupplierDescription { get; set; } // Descrição do fornecedor

        public string SupplierCNPJ { get; set; } // CNPJ do fornecedor]

    }
}

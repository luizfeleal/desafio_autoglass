using System;

namespace ManagerAPI.Models
{
    public class ProductViewModel
    {
            public int ProductId { get; set; } // Código do produto (sequencial e não nulo)

            public string Description { get; set; } // Descrição do produto (não nulo)

            public bool Active { get; set; } // Situação do produto (Ativo ou Inativo)

            public DateTime ManufactureDate { get; set; } // Data de fabricação

            public DateTime ExpiryDate { get; set; } // Data de validade

            public int SupplierId { get; set; } // Código do fornecedor

            public string SupplierDescription { get; set; } // Descrição do fornecedor

            public string SupplierCNPJ { get; set; } // CNPJ do fornecedor
    }
}

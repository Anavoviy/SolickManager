using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SolickManagerV3_4.DTO;

public partial class Assembly
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int Idmasterconfiguration { get; set; }

    public int Idmasterassembler { get; set; }

    public DateOnly Data { get; set; }

    public decimal Cost { get; set; }

    public string? Description { get; set; }

    public string? Notes { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<Applicationassembly> Applicationassemblies { get; } = new List<Applicationassembly>();

    public virtual ICollection<Assemblyproduct> Assemblyproducts { get; } = new List<Assemblyproduct>();

    public virtual Worker IdmasterassemblerNavigation { get; set; } = null!;

    public virtual Worker IdmasterconfigurationNavigation { get; set; } = null!;

    [NotMapped]
    public string CostView { get
        {
            return Cost.ToString() + " руб.";
        } }

    [NotMapped]
    public string MastersView { get
        {
            return DB.Instance.Workers.FirstOrDefault(s => s.Id == this.Idmasterconfiguration).FIO + ", " + DB.Instance.Workers.FirstOrDefault(s => s.Id == this.Idmasterassembler).FIO;
        } }

    [NotMapped]
    public string DataView { get
        {
            return Data.ToString();
        } }

    [NotMapped]
    public List<Product> Products
    {
        get
        {
            List<Product> products = new List<Product>();

            var result = DB.Instance.Assemblyproducts.Include(s => s.IdproductNavigation).Where(s => s.Idassembly == this.Id).ToList();
            if (result.Count > 0)
            {
                foreach (var assemblyProd in result)
                {
                    products.Add(assemblyProd.IdproductNavigation);
                }
            }

            return products;
        }
    }

    [NotMapped]
    public string GroupProductsView
    {
        get
        {
            return Title;
        }
    }
}

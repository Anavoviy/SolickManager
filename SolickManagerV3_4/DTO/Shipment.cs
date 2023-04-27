using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolickManagerV3_4.DTO;

public partial class Shipment
{
    public int Id { get; set; }

    public DateOnly Data { get; set; }

    public int Idprovider { get; set; }

    public int? Numberproducts { get; set; }

    public decimal? Amount { get; set; }

    public string? Notes { get; set; }

    public bool Deleted { get; set; }

    public virtual Provider IdproviderNavigation { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();

    [NotMapped]
    public string DataView { get
        {
            return Data.ToString("d");
        } }
}

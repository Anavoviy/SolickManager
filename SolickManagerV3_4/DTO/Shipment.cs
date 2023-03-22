using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Shipment
{
    public int Id { get; set; }

    public DateOnly Data { get; set; }

    public int Idprovider { get; set; }

    public int Numberproducts { get; set; }

    public decimal Amount { get; set; }

    public string Notes { get; set; } = null!;

    public bool Deleted { get; set; }

    public virtual Provider IdproviderNavigation { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}

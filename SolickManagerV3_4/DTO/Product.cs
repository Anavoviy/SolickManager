﻿using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Product
{
    public int Id { get; set; }

    public int Idcategory { get; set; }

    public string Model { get; set; } = null!;

    public string? Description { get; set; }

    public int Idshipment { get; set; }

    public decimal Cost { get; set; }

    public virtual Category IdcategoryNavigation { get; set; } = null!;

    public virtual Shipment IdshipmentNavigation { get; set; } = null!;

    public virtual ICollection<Productpricechange> Productpricechanges { get; } = new List<Productpricechange>();
}

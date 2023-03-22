using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Productcharacteristic
{
    public int Idproduct { get; set; }

    public int Idcharacteristic { get; set; }

    public string Meaning { get; set; } = null!;

    public bool Deleted { get; set; }

    public virtual Characteristic IdcharacteristicNavigation { get; set; } = null!;

    public virtual Product IdproductNavigation { get; set; } = null!;
}

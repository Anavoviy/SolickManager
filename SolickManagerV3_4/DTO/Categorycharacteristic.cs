using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Categorycharacteristic
{
    public int Idcategory { get; set; }

    public int Idcharacteristic { get; set; }

    public bool Deleted { get; set; }

    public virtual Category IdcategoryNavigation { get; set; } = null!;

    public virtual Characteristic IdcharacteristicNavigation { get; set; } = null!;
}

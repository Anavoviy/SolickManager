using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Characteristic
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public bool Deleted { get; set; }

    public virtual ICollection<Categorycharacteristic> Categorycharacteristics { get; } = new List<Categorycharacteristic>();

    public virtual ICollection<Productcharacteristic> Productcharacteristics { get; } = new List<Productcharacteristic>();

    public override string ToString()
    {
        return Title;
    }
}

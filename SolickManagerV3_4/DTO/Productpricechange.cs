using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Productpricechange
{
    public int Id { get; set; }

    public int Idproduct { get; set; }

    public decimal Ratio { get; set; }

    public decimal Newcost { get; set; }

    public bool Deleted { get; set; }

    public virtual Product IdproductNavigation { get; set; } = null!;
}

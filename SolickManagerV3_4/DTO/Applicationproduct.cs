using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Applicationproduct
{
    public int Idapplication { get; set; }

    public int Idproduct { get; set; }

    public bool Deleted { get; set; }

    public virtual Application IdapplicationNavigation { get; set; } = null!;

    public virtual Product IdproductNavigation { get; set; } = null!;
}

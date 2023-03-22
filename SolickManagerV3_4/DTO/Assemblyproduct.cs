using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Assemblyproduct
{
    public int Idassembly { get; set; }

    public int Idproduct { get; set; }

    public int Count { get; set; }

    public bool Deleted { get; set; }

    public virtual Assembly IdassemblyNavigation { get; set; } = null!;

    public virtual Product IdproductNavigation { get; set; } = null!;
}

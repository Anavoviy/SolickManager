using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Applicationassembly
{
    public int Idapplication { get; set; }

    public int Idassemby { get; set; }

    public bool Deleted { get; set; }

    public virtual Application IdapplicationNavigation { get; set; } = null!;

    public virtual Assembly IdassembyNavigation { get; set; } = null!;
}

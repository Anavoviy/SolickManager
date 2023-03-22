using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Workingshift
{
    public int Id { get; set; }

    public DateOnly Data { get; set; }

    public int Idworker { get; set; }

    public int Spendunits { get; set; }

    public bool Deleted { get; set; }

    public virtual Worker IdworkerNavigation { get; set; } = null!;
}

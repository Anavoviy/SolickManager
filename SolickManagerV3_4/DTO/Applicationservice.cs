using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Applicationservice
{
    public int Idapplication { get; set; }

    public int Idservice { get; set; }

    public int Idworker { get; set; }

    public bool Deleted { get; set; }

    public string? Notes { get; set; }

    public virtual Application IdapplicationNavigation { get; set; } = null!;

    public virtual Service IdserviceNavigation { get; set; } = null!;

    public virtual Worker IdworkerNavigation { get; set; } = null!;
}

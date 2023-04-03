using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Application
{
    public int Id { get; set; }

    public DateOnly Data { get; set; }

    public int Idclient { get; set; }

    public int Idmanager { get; set; }

    public string Problem { get; set; } = null!;

    public string Diagnostics { get; set; } = null!;

    public string Status { get; set; } = null!;

    public bool Deleted { get; set; }

    public string? Notes { get; set; }

    public int? Iddevice { get; set; }

    public virtual ICollection<Applicationassembly> Applicationassemblies { get; } = new List<Applicationassembly>();

    public virtual ICollection<Applicationproduct> Applicationproducts { get; } = new List<Applicationproduct>();

    public virtual ICollection<Applicationservice> Applicationservices { get; } = new List<Applicationservice>();

    public virtual Client IdclientNavigation { get; set; } = null!;

    public virtual Clientsdevice? IddeviceNavigation { get; set; }

    public virtual Worker IdmanagerNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Clientsdevice
{
    public int Id { get; set; }

    public int Idclient { get; set; }

    public string? Model { get; set; }

    public string? Description { get; set; }

    public decimal? Cost { get; set; }

    public string? Notes { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<Application> Applications { get; } = new List<Application>();

    public virtual Client IdclientNavigation { get; set; } = null!;
}

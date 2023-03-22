using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Provider
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string DirectorManager { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Requisites { get; set; }

    public string? Notes { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<Shipment> Shipments { get; } = new List<Shipment>();
}

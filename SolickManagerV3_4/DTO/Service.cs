using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Service
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public decimal Cost { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<Applicationservice> Applicationservices { get; } = new List<Applicationservice>();
}

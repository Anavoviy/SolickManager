using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Assembly
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int Idmasterconfiguration { get; set; }

    public int Idmasterassembler { get; set; }

    public DateOnly Data { get; set; }

    public decimal Cost { get; set; }

    public string? Description { get; set; }

    public string? Notes { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<Applicationassembly> Applicationassemblies { get; } = new List<Applicationassembly>();

    public virtual ICollection<Assemblyproduct> Assemblyproducts { get; } = new List<Assemblyproduct>();

    public virtual Worker IdmasterassemblerNavigation { get; set; } = null!;

    public virtual Worker IdmasterconfigurationNavigation { get; set; } = null!;
}

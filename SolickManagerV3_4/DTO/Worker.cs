using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

public partial class Worker
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateOnly Birthday { get; set; }

    public int Idpost { get; set; }

    public string? Graphic { get; set; }

    public int? Idplan { get; set; }

    public string Passport { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Requisites { get; set; }

    public bool Deleted { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Application> Applications { get; } = new List<Application>();

    public virtual ICollection<Applicationservice> Applicationservices { get; } = new List<Applicationservice>();

    public virtual ICollection<Assembly> AssemblyIdmasterassemblerNavigations { get; } = new List<Assembly>();

    public virtual ICollection<Assembly> AssemblyIdmasterconfigurationNavigations { get; } = new List<Assembly>();

    public virtual Plan? IdplanNavigation { get; set; }

    public virtual Post IdpostNavigation { get; set; } = null!;

    public virtual ICollection<Workingshift> Workingshifts { get; } = new List<Workingshift>();

    public override string ToString()
    {
        return Firstname + " " + Surname[0] + ".";
    }

    public string FIO
    {
        get
        {
            return this.Surname + " " + this.Firstname + " " + this.Patronymic;
        }
    }
}

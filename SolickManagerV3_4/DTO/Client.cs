using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolickManagerV3_4.DTO;

public partial class Client
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string Secondname { get; set; } = null!;

    public string? Patronymic { get; set; }

    public DateOnly? Birthday { get; set; }

    public string? Passport { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Notes { get; set; }

    public bool Deleted { get; set; }

    public virtual ICollection<Application> Applications { get; } = new List<Application>();

    public virtual ICollection<Clientsdevice> Clientsdevices { get; } = new List<Clientsdevice>();


    [NotMapped]
    public string FIO
    {
        get
        {
            return Secondname + " " + Firstname + " " + Patronymic;
        }
    }
}

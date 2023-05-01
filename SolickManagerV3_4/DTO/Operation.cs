using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolickManagerV3_4.DTO;

public partial class Operation
{
    public int Id { get; set; }

    public DateOnly Dataopen { get; set; }

    public DateOnly? Dataclose { get; set; }

    public int? Debet { get; set; }

    public int? Credit { get; set; }

    public string? Object { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public string? Notes { get; set; }

    public bool Deleted { get; set; }

    public virtual Bankaccount? CreditNavigation { get; set; }

    public virtual Bankaccount? DebetNavigation { get; set; }

    [NotMapped]
    public string DataOpenView { get
        {
            return Dataopen.ToString("d");
        } }
    [NotMapped]
    public string DataCloseView
    {
        get
        {
            return Dataclose.ToString();
        }
    }
}

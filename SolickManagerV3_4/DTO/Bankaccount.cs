using System;
using System.Collections.Generic;

namespace SolickManagerV3_4.DTO;

/// <summary>
/// Таблица содержит счета (внутренние счета) компании (организации)
/// </summary>
public partial class Bankaccount
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public decimal? Balance { get; set; }

    public bool? Deleted { get; set; }

    public virtual ICollection<Operation> OperationCreditNavigations { get; } = new List<Operation>();

    public virtual ICollection<Operation> OperationDebetNavigations { get; } = new List<Operation>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SolickManagerV3_4.DTO;

public partial class Plan
{
    public int Id { get; set; }

    public int Idhowpay { get; set; }

    public decimal Costofone { get; set; }

    public bool Deleted { get; set; }

    public virtual Howpay IdhowpayNavigation { get; set; } = null!;

    public virtual ICollection<Worker> Workers { get; } = new List<Worker>();

    [NotMapped]
    public string HowPayTitle { get
        {
            return DB.Instance.Howpays.FirstOrDefault(s => s.Id == this.Idhowpay).Title;
        } }
}

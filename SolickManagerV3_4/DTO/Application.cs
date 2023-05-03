using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Windows.Media;

namespace SolickManagerV3_4.DTO;

public partial class Application
{
    public int Id { get; set; }

    public DateOnly Data { get; set; }

    public int Idclient { get; set; }

    public int Idmanager { get; set; }

    public string? Problem { get; set; }

    public string? Diagnostics { get; set; }

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

    [NotMapped]
    public string PriceOfAllService
    {
        get
        {
            double sum = 0;

            var ApplicationServices = DB.Instance.Applicationservices.Include(s => s.IdserviceNavigation).Where(s => s.Idapplication == this.Id && s.Deleted == false);

            foreach (var applicationService in ApplicationServices)
            {
                sum += (double)applicationService.IdserviceNavigation.Cost;
            }

            return sum.ToString() + " руб.";
        }
    }

    [NotMapped]
    public string DateView
    {
        get
        {
            return Data.ToString("d");
        }
    }

    [NotMapped]
    public List<Applicationservice> ListService
    {
        get
        {
            return DB.Instance.Applicationservices.Include(s => s.IdserviceNavigation).Where(s => s.Idapplication == this.Id).ToList();
        }
    }


    [NotMapped]
    public decimal PriceOfAllproducts
    { 
        get
            {
                decimal sum = 0;

                return sum;
            } 
    }

    [NotMapped]
    public List<Product> Products { get
        {
            return DB.Instance.Applicationproducts.Include(s => s.IdproductNavigation).Where(s => s.Idapplication == this.Id).Select(s => s.IdproductNavigation).ToList();
        } }


}

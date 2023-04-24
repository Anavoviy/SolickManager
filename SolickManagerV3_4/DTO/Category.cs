using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows.Automation.Peers;

namespace SolickManagerV3_4.DTO;

public partial class Category
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public bool Deleted { get; set; }

    public virtual ICollection<Categorycharacteristic> Categorycharacteristics { get; } = new List<Categorycharacteristic>();

    public virtual ICollection<Product> Products { get; } = new List<Product>();

    public string CharacteristicsView { 
        get 
        {
            string Characteristics = "";

            List<Categorycharacteristic> categorycharacteristics = DB.Instance.Categorycharacteristics
                .Include(s => s.IdcharacteristicNavigation)
                .Where(s => s.Idcategory == this.Id && s.Deleted == false)
                .OrderBy(s => s.Idcharacteristic).ToList();

            if (categorycharacteristics.Count > 0)
            {
                foreach (Categorycharacteristic charcat in categorycharacteristics)
                {
                    if (charcat == categorycharacteristics.Last())
                        Characteristics += charcat.IdcharacteristicNavigation.Title;
                    else
                        Characteristics += charcat.IdcharacteristicNavigation.Title + ", ";
                }
            }

            return Characteristics;
        } }

    [NotMapped]
    public List<Characteristic> Characteristics { 
        get 
        {
            return DB.Instance.Categorycharacteristics
                .Include(s => s.IdcharacteristicNavigation)
                .Where(s => s.Idcategory == this.Id && s.Deleted == false)
                .Select(s => s.IdcharacteristicNavigation)
                .OrderBy(s => s.Id).ToList();
        } }
}

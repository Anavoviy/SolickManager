using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SolickManagerV3_4.DTO;

public partial class Product
{
    public int Id { get; set; }

    public int Idcategory { get; set; }

    public string Model { get; set; } = null!;

    public string? Description { get; set; }

    public int Idshipment { get; set; }

    public decimal Cost { get; set; }

    public int Amount { get; set; }

    public virtual ICollection<Applicationproduct> Applicationproducts { get; } = new List<Applicationproduct>();

    public virtual ICollection<Assemblyproduct> Assemblyproducts { get; } = new List<Assemblyproduct>();

    public virtual Category IdcategoryNavigation { get; set; } = null!;

    public virtual Shipment IdshipmentNavigation { get; set; } = null!;

    public virtual ICollection<Productcharacteristic> Productcharacteristics { get; } = new List<Productcharacteristic>();

    public virtual ICollection<Productpricechange> Productpricechanges { get; } = new List<Productpricechange>();

    [NotMapped]
    public Provider Provider { 
        get 
        {
            return DB.Instance.Providers.FirstOrDefault(s => s.Id == DB.Instance.Shipments.FirstOrDefault(s => s.Id == this.Idshipment).Idprovider);
        } }

    [NotMapped]
    public string CostView { get
        {
            if(DB.Instance.Productpricechanges.FirstOrDefault(s => s.Idproduct == this.Id) != null)
                return DB.Instance.Productpricechanges.Where(s => s.Idproduct == this.Id).OrderBy(s => s.Id).Last().Newcost.ToString();
            else
                return this.Cost.ToString();
        } }

    [NotMapped]
    public decimal CostAll { get
        {
            return (Amount * decimal.Parse(this.CostView));
        } }

    [NotMapped]
    public string ProductView { get
        {
            return Model + " " + CostView + "руб.";
        } }
}

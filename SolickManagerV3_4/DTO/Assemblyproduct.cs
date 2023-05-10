using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows;

namespace SolickManagerV3_4.DTO;

public partial class Assemblyproduct
{
    [NotMapped]
    private int addCount = 0;

    public int Idassembly { get; set; }

    public int Idproduct { get; set; }

    public int Count { get; set; }

    public bool Deleted { get; set; }

    public virtual Assembly IdassemblyNavigation { get; set; } = null!;

    public virtual Product IdproductNavigation { get; set; } = null!;

    [NotMapped]
    public int AddCount 
    { get => addCount; 
        set 
        {
            if (value <= DB.Instance.Products.FirstOrDefault(s => s.Id == Idproduct).Amount)
                addCount = value;

            else
                MessageBox.Show("На складе нет столько товаров!");
        } 
    }
}

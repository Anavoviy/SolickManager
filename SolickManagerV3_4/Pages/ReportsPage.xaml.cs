using SolickManagerV3_4.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace SolickManagerV3_4.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void Signal([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Работник за приложением
        public Worker Worker { get; set; }

        // Название кнопки для склада: WireHouseMenuItem  Название Grid: WireHouseGrid
        public ISeries[] DiagramCategoriesPiece { get; set; }
        public ISeries[] DiagramCategoriesMoney { get; set; }
        public ISeries[] DiagramAssembliesOnProductsPiece { get; set; }
        public ISeries[] DiagramAssembliesOnProductsMoney { get; set; }


        // Название кнопки для продаж: SaleMenuItem
        public ISeries[] DiagramSales { get; set; }

        // Название кнопки для заявок: ApplicationMenuItem
        public ISeries[] DiagramApplications { get; set; }



        public ReportsPage(DTO.Worker worker)
        {
            InitializeComponent();

            WireHouseClick(WireHouseMenuItem, new RoutedEventArgs());

            Worker = worker;
        }

        private void WireHouseClick(object sender, RoutedEventArgs e)
        {
            SaleMenuItem.BorderThickness = new Thickness(0,0,0,0);
            ApplicationMenuItem.BorderThickness = new Thickness(0, 0, 0, 0);

            WireHouseMenuItem.BorderThickness = new Thickness(0, 0, 0, 1);

            List<Category> Categories = DB.Instance.Categories.Include(s => s.Products).Where(s => s.Deleted == false && s.Products.Count() > 0).Distinct().ToList();
            DiagramCategoriesPiece = new ISeries[Categories.Count()];
            DiagramCategoriesMoney = new ISeries[Categories.Count()];

            for(int i = 0; i < Categories.Count(); i++) 
            {
                DiagramCategoriesPiece[i] = new PieSeries<int> 
                { 
                    Name = Categories[i].Title, 
                    Values = new[] { DB.Instance.Products.Where(s => s.Idcategory == Categories[i].Id).Count() },
                    DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                    DataLabelsSize = 16,
                    DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer,
                    DataLabelsFormatter = point => point.PrimaryValue.ToString() + " шт."
                };

                List<Product> products = DB.Instance.Products.Where(s => s.Idcategory == Categories[i].Id).ToList();
                decimal costAll = products.Select(s => s.CostAll).Sum();
                if(costAll > 0) 
                    DiagramCategoriesMoney[i] = new PieSeries<decimal> 
                    { 
                        Name = Categories[i].Title, 
                        Values = new[] { costAll },
                        DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                        DataLabelsSize = 16,
                        DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer,
                        DataLabelsFormatter = point => point.PrimaryValue.ToString("N2") + " ₽"
                    };
                else
                    DiagramCategoriesMoney[i] = new PieSeries<decimal>
                    {
                        Name = Categories[i].Title,
                        Values = new[] { costAll },
                    };

            }


            DiagramAssembliesOnProductsPiece = new ISeries[2]
            {
                new PieSeries<int>
                {
                    Name = "Сборки",
                    Values= new int[] { DB.Instance.Assemblies.Select(s => s.Cost).Count()},
                    DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                    DataLabelsSize = 16,
                    DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                    DataLabelsFormatter = point => point.PrimaryValue.ToString() + " шт."
                },
                new PieSeries<int>
                {
                    Name = "Товары",
                    Values = new int[]
                    {
                        DB.Instance.Products.Sum(s => s.Amount)
                    },
                    DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                    DataLabelsSize = 16,
                    DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                    DataLabelsFormatter = point => point.PrimaryValue.ToString() + " шт."
                }
            };

            List<Product> products2 = DB.Instance.Products.ToList();
            decimal costAll2 = products2.Select(s => s.CostAll).Sum();
            DiagramAssembliesOnProductsMoney = new ISeries[2]
            {
                new PieSeries<decimal>
                {
                    Name = "Сборки",
                    Values = new decimal[] {DB.Instance.Assemblies.Sum(s => s.Cost)},
                    DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                    DataLabelsSize = 16,
                    DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                    DataLabelsFormatter = point => point.PrimaryValue.ToString("N2") + " ₽"
                },
                new PieSeries<decimal>
                {
                    Name = "Товары",
                    Values = new decimal[] {costAll2},
                    DataLabelsPaint = new SolidColorPaint(SKColors.Black),
                    DataLabelsSize = 16,
                    DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Middle,
                    DataLabelsFormatter = point => point.PrimaryValue.ToString("N2") + " ₽"
                }
            };

            AssemblyOnProductMoneyDiagram.Series = DiagramAssembliesOnProductsMoney;
            AssemblyOnProductPieceDiagram.Series = DiagramAssembliesOnProductsPiece;
            CategoriesPieceDiagram.Series = DiagramCategoriesPiece;
            CategoriesMoneyDiagram.Series = DiagramCategoriesMoney;
        }
        private void SaleClick(object sender, RoutedEventArgs e)
        {
            ApplicationMenuItem.BorderThickness = new Thickness(0, 0, 0, 0);
            WireHouseMenuItem.BorderThickness = new Thickness(0, 0, 0, 0);

            SaleMenuItem.BorderThickness = new Thickness(0, 0, 0, 1);
        }
        private void ApplicationClick(object sender, RoutedEventArgs e)
        {
            SaleMenuItem.BorderThickness = new Thickness(0, 0, 0, 0);
            WireHouseMenuItem.BorderThickness = new Thickness(0, 0, 0, 0);

            ApplicationMenuItem.BorderThickness = new Thickness(0, 0, 0, 1);
        }
    }
}

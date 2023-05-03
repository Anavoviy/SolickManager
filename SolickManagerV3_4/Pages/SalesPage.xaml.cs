﻿using Microsoft.EntityFrameworkCore;
using SolickManagerV3_4.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

namespace SolickManagerV3_4.Pages
{
    /// <summary>
    /// Логика взаимодействия для SalesPage.xaml
    /// </summary>
    public partial class SalesPage : Page, INotifyPropertyChanged
    {
        private DTO.Application selectedSale;

        public event PropertyChangedEventHandler? PropertyChanged;
        void Signal([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        // Работник за приложением
        public Worker Worker { get; set; }

        // Основные данные
        public List<DTO.Application> Sales { get; set; }
        public DTO.Application SelectedSale { get => selectedSale; set { selectedSale = value; } }

        // Поиск
        public string DataStart { get; set; }
        public string DataEnd { get; set; }
        public string SearchText { get; set; }
        public List<string> StatusesList { get; set; } = new List<string>();
        public int StatusIndex { get; set; } = 0;

        // Быстрое редактирование
        public List<string> EditStatusesList { get; set; } = new List<string>() { "Создана", "Принята", "Собрана", "Оплачена", "Завершена" };
        public int EditStatusIndex { get; set; }
        public List<Product> SelectedSaleProducts { get; set; }
        public Product SelectedSaleSelectedProduct { get; set; }
        public List<Assembly> SelectedSaleGroupsProducts { get; set; }
        public Assembly SelectedSaleSelectedGroupProducts { get; set; }

        public SalesPage(Worker worker)
        {
            InitializeComponent();

            Worker = worker;
            Search();
            FillStatusesList();

            DataContext = this;
        }

        private void FillStatusesList()
        {
            List<string> statuses = new List<string>();

            if (Sales.Count > 0)
                statuses = Sales.Select(s => s.Status).Distinct().ToList();

            StatusesList.Add("Все статусы");
            StatusesList.Add("Незавершённые");
            if (statuses.Count() > 0)
                StatusesList.AddRange(statuses);

            Signal(nameof(StatusesList));
        }
        private void Search()
        {
            var result = DB.Instance.Applications.Include(s => s.Applicationproducts).Include(s => s.Applicationassemblies).Where(s => (s.Applicationassemblies.Count() > 0 || s.Applicationproducts.Count() > 0) &&
                                                                                                                                       s.Deleted == false);

            if (StatusIndex == 0)
                Sales = result.OrderBy(s => s.Id).ToList();
            else if (StatusIndex == 1)
                Sales = result.Where(s => s.Status != "Завершена").OrderBy(s => s.Id).ToList();
            else
                Sales = result.Where(s => s.Status == StatusesList[StatusIndex]).OrderBy(s => s.Id).ToList();

            DateOnly startData, endData;
            if (DateOnly.TryParse(DataStart, out startData) && !DateOnly.TryParse(DataEnd, out endData))
                Sales = Sales.Where(s => s.Data > startData).OrderBy(s => s.Id).ToList();
            else if (!DateOnly.TryParse(DataStart, out startData) && DateOnly.TryParse(DataEnd, out endData))
                Sales = Sales.Where(s => s.Data < endData).OrderBy(s => s.Id).ToList();
            else if (DateOnly.TryParse(DataStart, out startData) && DateOnly.TryParse(DataEnd, out endData))
                Sales = Sales.Where(s => s.Data > startData && s.Data < endData).OrderBy(s => s.Id).ToList();

            if (SelectedSale != null)
                SelectedSale = Sales.FirstOrDefault(s => s.Id == SelectedSale.Id);

            Signal(nameof(Sales));
            Signal(nameof(SelectedSale));
        }


        private void AddNewSale(object sender, RoutedEventArgs e)
        {

        }
        private void EditSelectedSale(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteSelectedSale(object sender, RoutedEventArgs e)
        {
            if(SelectedSale != null)
            {
                SelectedSale.Deleted = true;

                DB.Instance.Applications.Update(SelectedSale);

                Search();
            }
        }

        private void SaveEditSelectedSale(object sender, RoutedEventArgs e)
        {

        }

        private void AddCrossSaleProduct(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteCrossSaleProduct(object sender, RoutedEventArgs e)
        {

        }
        private void AddCrossSaleGroupProducts(object sender, RoutedEventArgs e)
        {

        }
        private void DeleteCrossSaleGroupProducts(object sender, RoutedEventArgs e)
        {

        }
    }
}

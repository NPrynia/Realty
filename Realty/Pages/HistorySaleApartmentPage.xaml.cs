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

namespace Realty.Pages
{
    /// <summary>
    /// Логика взаимодействия для HistorySaleApartmentPage.xaml
    /// </summary>
    public partial class HistorySaleApartmentPage : Page
    {
        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По Цене",
            "По Адресу",
            "По дате "
           ,
        };
        public HistorySaleApartmentPage()
        {
            InitializeComponent();
            lvHistory.ItemsSource = ClassHelper.AppData.Context.SaleApartment.ToList();
            cbSort.ItemsSource = listSort;
        }

        private void filter()
        {
            List<EF.SaleApartment> saleApartments = new List<EF.SaleApartment>();
            saleApartments = ClassHelper.AppData.Context.SaleApartment.ToList();
            saleApartments = saleApartments.Where
                (i => i.Apartment.adressApartment.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 i.Client.FIO.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 Convert.ToString(i.Apartment.Cost).Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {

                case 0:
                    saleApartments = saleApartments.OrderBy(i => i.IDApartment).ToList();
                    lvHistory.ItemsSource = saleApartments;
                    break;
                case 1:
                    saleApartments = saleApartments.OrderBy(i => i.Apartment.Cost).ToList();
                    lvHistory.ItemsSource = saleApartments;
                    break;
                case 2:
                    saleApartments = saleApartments.OrderBy(i => i.Apartment.adressApartment).ToList();
                    lvHistory.ItemsSource = saleApartments;
                    break;
                case 3:
                    saleApartments = saleApartments.OrderBy(i => i.Date).ToList();
                    lvHistory.ItemsSource = saleApartments;
                    break;
            }
            lvHistory.ItemsSource = saleApartments;

        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter();
        }
    }
}

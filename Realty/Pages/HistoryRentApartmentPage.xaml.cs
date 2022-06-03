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
    /// Логика взаимодействия для HistoryRentApartmentPage.xaml
    /// </summary>
    public partial class HistoryRentApartmentPage : Page
    {

        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По Цене",
            "По Адресу",
            "По дате начала",
            "По дате конца"
           ,
        };
        public HistoryRentApartmentPage()
        {
            InitializeComponent();
            lvHistory.ItemsSource = ClassHelper.AppData.Context.RentApartment.ToList();
            cbSort.ItemsSource = listSort;
        }

        private void filter()
        {
            List<EF.RentApartment> rentApartments = new List<EF.RentApartment>();
            rentApartments = ClassHelper.AppData.Context.RentApartment.ToList();
            rentApartments = rentApartments.Where
                (i => i.Apartment.adressApartment.ToLower().Contains(tbSearch.Text.ToLower()) ||
                i.Client.FIO.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 Convert.ToString(i.FinalCost).Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {

                case 0:
                    rentApartments = rentApartments.OrderBy(i => i.IDApartment).ToList();
                    lvHistory.ItemsSource = rentApartments;
                    break;
                case 1:
                    rentApartments = rentApartments.OrderBy(i => i.FinalCost).ToList();
                    lvHistory.ItemsSource = rentApartments;
                    break;
                case 2:
                    rentApartments = rentApartments.OrderBy(i => i.Apartment.adressApartment).ToList();
                    lvHistory.ItemsSource = rentApartments;
                    break;
                case 3:
                    rentApartments = rentApartments.OrderBy(i => i.DateStartRent).ToList();
                    lvHistory.ItemsSource = rentApartments;
                    break;
                case 4:
                    rentApartments = rentApartments.OrderBy(i => i.DateEndRent).ToList();
                    lvHistory.ItemsSource = rentApartments;
                    break;
            }
            lvHistory.ItemsSource = rentApartments;

        }


        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter();
        }
    }
}

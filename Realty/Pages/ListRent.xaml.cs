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
    /// Логика взаимодействия для ListRent.xaml
    /// </summary>
    public partial class ListRent : Page
    {
        List<EF.RentApartment> rentApartments = new List<EF.RentApartment>();
        List<EF.RentHouse> rentHouses = new List<EF.RentHouse>();
        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По Цене",
            "По Адресу",
            "По дате начала",
            "По дате конца"
           ,
        };
        public ListRent()
        {
            InitializeComponent();
            rentApartments = ClassHelper.AppData.Context.RentApartment.Where(i => i.IsDelete == false).ToList();
            lvRentApart.ItemsSource = rentApartments;
            rentHouses = ClassHelper.AppData.Context.RentHouse.Where(i => i.IsDelete == false).ToList();
            lvRentHouse.ItemsSource = rentHouses;
        }

        private void btnExtendRent_Click(object sender, RoutedEventArgs e)
        {
            if (lvRentApart.SelectedItem is EF.RentApartment)
            {
                var rent = lvRentApart.SelectedItem as EF.RentApartment;
                Windows.extendTimeRentWindow extendTimeRentWindow = new Windows.extendTimeRentWindow(rent);
                extendTimeRentWindow.ShowDialog();
                filter();
            }
            else
            {
                MessageBox.Show("Выберите аренду");
            }

            if (lvRentApart.SelectedItem is EF.RentHouse)
            {
                var rent = lvRentApart.SelectedItem as EF.RentHouse;
                Windows.extendTimeRentWindow extendTimeRentWindow = new Windows.extendTimeRentWindow(rent);
                extendTimeRentWindow.ShowDialog();
                filter();
            }
            else
            {
                MessageBox.Show("Выберите аренду");
            }

        }


        private void lvRentApart_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvRentApart.SelectedItem is EF.RentApartment)
            {
                var resClick = MessageBox.Show("Удалить аренду  ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                var rentapart = lvRentApart.SelectedItem as EF.RentApartment;
                rentapart.IsDelete = true;
                ClassHelper.AppData.Context.SaveChanges();
                filter();
                MessageBox.Show("Аренда удалена");

            }
        }

        private void lvRentHouse_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvRentHouse.SelectedItem is EF.RentHouse)
            {
                var resClick = MessageBox.Show("Удалить аренду  ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                var rentHouse = lvRentHouse.SelectedItem as EF.RentHouse;
                rentHouse.IsDelete = true;
                ClassHelper.AppData.Context.SaveChanges();
                filter();
                MessageBox.Show("Аренда удалена");

            }
            filter();
        }

        private void filter()
        {

            List<EF.RentApartment> rentApartments  = new List<EF.RentApartment>();
            List<EF.RentHouse> rentHouses = new List<EF.RentHouse>();
            rentApartments = ClassHelper.AppData.Context.RentApartment.Where(i => i.IsDelete == false ).ToList();
            rentApartments = rentApartments.Where
                (i => i.Client.FIO.ToLower().Contains(tbSearch.Text.ToLower()) ||
                i.Apartment.adressApartment.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 Convert.ToString(i.FinalCost).Contains(tbSearch.Text.ToLower())).ToList();

            rentHouses = ClassHelper.AppData.Context.RentHouse.Where(i => i.IsDelete == false).ToList();
            rentHouses = rentHouses.Where
                (i => i.Client.FIO.ToLower().Contains(tbSearch.Text.ToLower()) ||
                i.House.adressHouse.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 Convert.ToString(i.FinalCost).Contains(tbSearch.Text.ToLower())).ToList();



            switch (cbSort.SelectedIndex)
            {

                case 0:
                    rentApartments = rentApartments.OrderBy(i => i.IDApartment).ToList();
                    rentHouses = rentHouses.OrderBy(i => i.IDHouse).ToList();
                    lvRentApart.ItemsSource = rentApartments;
                    lvRentHouse.ItemsSource = rentHouses;
                    break;
                case 1:
                    rentApartments = rentApartments.OrderBy(i => i.FinalCost).ToList();
                    rentHouses = rentHouses.OrderBy(i => i.FinalCost).ToList();
                    lvRentApart.ItemsSource = rentApartments;
                    lvRentHouse.ItemsSource = rentHouses;
                    break;
                case 2:
                    rentApartments = rentApartments.OrderBy(i => i.Apartment.adressApartment).ToList();
                    rentHouses = rentHouses.OrderBy(i => i.House.adressHouse).ToList();
                    lvRentApart.ItemsSource = rentApartments;
                    lvRentHouse.ItemsSource = rentHouses;
                    break;
                case 3:
                    rentApartments = rentApartments.OrderBy(i => i.DateStartRent).ToList();
                    rentHouses = rentHouses.OrderBy(i => i.DateStartRent).ToList();
                    lvRentApart.ItemsSource = rentApartments;
                    lvRentHouse.ItemsSource = rentHouses;
                    break;
                case 4:
                    rentApartments = rentApartments.OrderBy(i => i.DateEndRent).ToList();
                    rentHouses = rentHouses.OrderBy(i => i.DateEndRent).ToList();
                    lvRentApart.ItemsSource = rentApartments;
                    lvRentHouse.ItemsSource = rentHouses;
                    break;
            }
            lvRentApart.ItemsSource = rentApartments;
            lvRentHouse.ItemsSource = rentHouses;

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

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
    /// Логика взаимодействия для HistoryRentHousePage.xaml
    /// </summary>
    public partial class HistoryRentHousePage : Page
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

        public HistoryRentHousePage()
        {
            InitializeComponent();
            lvHistory.ItemsSource = ClassHelper.AppData.Context.RentHouse.ToList();
            cbSort.ItemsSource = listSort;
        }

        private void filter()
        {
            List<EF.RentHouse> rentHouses = new List<EF.RentHouse>();
            rentHouses = ClassHelper.AppData.Context.RentHouse.ToList();
            rentHouses = rentHouses.Where
                (i => i.House.adressHouse.ToLower().Contains(tbSearch.Text.ToLower()) ||
                i.Client.FIO.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 Convert.ToString(i.FinalCost).Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {

                case 0:
                    rentHouses = rentHouses.OrderBy(i => i.IDHouse).ToList();
                    lvHistory.ItemsSource = rentHouses;
                    break;
                case 1:
                    rentHouses = rentHouses.OrderBy(i => i.FinalCost).ToList();
                    lvHistory.ItemsSource = rentHouses;
                    break;
                case 2:
                    rentHouses = rentHouses.OrderBy(i => i.House.adressHouse).ToList();
                    lvHistory.ItemsSource = rentHouses;
                    break;
                case 3:
                    rentHouses = rentHouses.OrderBy(i => i.DateStartRent).ToList();
                    lvHistory.ItemsSource = rentHouses;
                    break;
                case 4:
                    rentHouses = rentHouses.OrderBy(i => i.DateEndRent).ToList();
                    lvHistory.ItemsSource = rentHouses;
                    break;
            }
            lvHistory.ItemsSource = rentHouses;

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

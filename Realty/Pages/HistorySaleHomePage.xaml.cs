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
    /// Логика взаимодействия для HistorySaleHomePage.xaml
    /// </summary>
    public partial class HistorySaleHomePage : Page
    {

        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По Цене",
            "По Адресу",
            "По дате "
           ,
        };
        public HistorySaleHomePage()
        {
            InitializeComponent();
            lvHistory.ItemsSource = ClassHelper.AppData.Context.SaleHouse.ToList();
            cbSort.ItemsSource = listSort;
        }

        private void filter()
        {
            List<EF.SaleHouse> saleHouses = new List<EF.SaleHouse>();
            saleHouses = ClassHelper.AppData.Context.SaleHouse.ToList();
            saleHouses = saleHouses.Where
                (i => i.House.adressHouse.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 i.Client.FIO.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 Convert.ToString(i.House.Cost).Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {

                case 0:
                    saleHouses = saleHouses.OrderBy(i => i.IDHouse).ToList();
                    lvHistory.ItemsSource = saleHouses;
                    break;
                case 1:
                    saleHouses = saleHouses.OrderBy(i => i.House.Cost).ToList();
                    lvHistory.ItemsSource = saleHouses;
                    break;
                case 2:
                    saleHouses = saleHouses.OrderBy(i => i.House.adressHouse).ToList();
                    lvHistory.ItemsSource = saleHouses;
                    break;
                case 3:
                    saleHouses = saleHouses.OrderBy(i => i.Date).ToList();
                    lvHistory.ItemsSource = saleHouses;
                    break;
            }
            lvHistory.ItemsSource = saleHouses;

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

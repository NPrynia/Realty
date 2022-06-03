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
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Page
    {
        public History()
        {
            InitializeComponent();
            contentFrame.Navigate(new Pages.HistorySaleHomePage());
        }
        private void nv_ItemInvoked(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked == true)
            {
            }
            else if (args.InvokedItemContainer != null)
            {
                var navItemTag = args.InvokedItemContainer.ToolTip.ToString();
                switch (navItemTag)
                {


                    case "История продаж домов":
                        contentFrame.Navigate(new Pages.HistorySaleHomePage());
                        break;
                    case "История продаж квартир":
                        contentFrame.Navigate(new Pages.HistorySaleApartmentPage());
                        break;
                    case "История аренд домов":
                        contentFrame.Navigate(new Pages.HistoryRentHousePage());
                        break;
                    case "История аренд квартир":
                        contentFrame.Navigate(new Pages.HistoryRentApartmentPage());
                        break;

                }

            }
        }

       
    }
}

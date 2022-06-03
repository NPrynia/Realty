
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

namespace Realty
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            contentFrame.Navigate(new Pages.ListRent());


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
                    

                    case "Добавить недвижимость":
                        contentFrame.Navigate(new Pages.AddProperty());
                        borderDrag.VerticalAlignment = VerticalAlignment.Top;
                        break;
                    case "Покупка дома":
                        contentFrame.Navigate(new Pages.BuyHouse());
                        borderDrag.VerticalAlignment = VerticalAlignment.Top;
                        break;
                    case "Покупка квартиры":
                        contentFrame.Navigate(new Pages.BuyApartment());
                        borderDrag.VerticalAlignment = VerticalAlignment.Top;
                        break;
                    case "Аренда дома":
                        contentFrame.Navigate(new Pages.RentHouse());
                        borderDrag.VerticalAlignment = VerticalAlignment.Top;
                        break;
                    case "Аренда квартиры":
                        contentFrame.Navigate(new Pages.RentApartment());
                        borderDrag.VerticalAlignment = VerticalAlignment.Top;
                        break;

                    case "Действующие аренды":
                        contentFrame.Navigate(new Pages.ListRent());
                        borderDrag.VerticalAlignment = VerticalAlignment.Top;
                        break;
                    case "История":
                        contentFrame.Navigate(new Pages.History());
                        borderDrag.VerticalAlignment = VerticalAlignment.Bottom;
                        break;



                    case "Выход":
                        this.Close();
                        break;
                }

            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DragMove();
            }
            catch
            {
            }
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                }

            }
        }


    }
}

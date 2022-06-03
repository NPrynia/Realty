using Gu.Wpf.FlipView;
using Realty.ClassHelper;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для BuyHouse.xaml
    /// </summary>
    public partial class BuyHouse : Page
    {

        List<EF.House> houses = new List<EF.House>();
        int qtyElementl = 0;

        List<string> listSort = new List<string>()
        {
            "По умолчанию",
            "По Цене",
            "По Адресу",
            "По количетсву комнат",
            "По количетсву этажей"
           ,
        };

        public BuyHouse()
        {
            InitializeComponent();

            cbSort.ItemsSource = listSort;
            houses  = ClassHelper.AppData.Context.House.Where(i => i.Isdelete == false && i.ForRent == false).ToList();
            lvBuyHouse.ItemsSource = houses;
        }

        private void Filter()
        {

            qtyElementl = 0;
            List<EF.House> houses = new List<EF.House>();
            houses = ClassHelper.AppData.Context.House.Where(i => i.Isdelete == false && i.ForRent == false).ToList();
            houses = houses.Where
                (i => i.adressHouse.ToLower().Contains(tbSearch.Text.ToLower()) ||
                 Convert.ToString(i.Cost).Contains(tbSearch.Text.ToLower())).ToList();

            switch (cbSort.SelectedIndex)
            {

                case 0:
                    houses = houses.OrderBy(i => i.IDHouse).ToList();
                    lvBuyHouse.ItemsSource = houses;
                    break;
                case 1:
                    houses = houses.OrderBy(i => i.Cost).ToList();
                    lvBuyHouse.ItemsSource = houses;
                    break;
                case 2:
                    houses = houses.OrderBy(i => i.adressHouse).ToList();
                    lvBuyHouse.ItemsSource = houses;
                    break;
                case 3:
                    houses = houses.OrderBy(i => i.QtyFloor).ToList();
                    lvBuyHouse.ItemsSource = houses;
                    break;
                case 4:
                    houses = houses.OrderBy(i => i.QtyRoom).ToList();
                    lvBuyHouse.ItemsSource = houses;
                    break;
            }
            lvBuyHouse.ItemsSource = houses;

        }


        private void lvBuyHouse_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvBuyHouse.SelectedItem is EF.House)
            {
                qtyElementl = 0;
                var house = lvBuyHouse.SelectedItem as EF.House;
                Windows.AddHouseWindow addHouseWindow = new Windows.AddHouseWindow(house);
                addHouseWindow.ShowDialog();
                Filter();
            }

        }
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void lvDel_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvBuyHouse.SelectedItem is EF.House)
            {
                var resClick = MessageBox.Show("Удалить дом  ?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                var house = lvBuyHouse.SelectedItem as EF.House;
                house.Isdelete = true;
                ClassHelper.AppData.Context.SaveChanges();
                MessageBox.Show("Дом удален");
                Filter();

            }

        }
        private void LoadedLv(object sender, RoutedEventArgs e)
        {

            try
            {
                List<Image> images = new List<Image>();

                FlipView fv = new FlipView();


                var idHouse = houses[qtyElementl];
                var idImageHouse = AppData.Context.ImageHouse.ToList().
                       Where(i => i.IDHouse == idHouse.IDHouse).FirstOrDefault();

                if (idImageHouse != null)
                {

                    Image img = new Image();
                    var imageCollection = ClassHelper.AppData.Context.ImageHouse.Where(a => a.IDHouse == idHouse.IDHouse).ToList();
                    var image = from p in imageCollection select new { p.Image }.Image;
                    foreach (byte[] byteImg in image)
                    {

                        using (MemoryStream stream = new MemoryStream(byteImg))
                        {
                            Image i = new Image();

                            BitmapImage bitmapImage = new BitmapImage();
                            bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                            bitmapImage.StreamSource = stream;
                            bitmapImage.EndInit();
                            i.Source = bitmapImage;
                            images.Add(i);
                        }
                    }
                    fv = sender as FlipView;
                    fv.ItemsSource = images;
                }
                else
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("/Res/DefaultApartment.png", UriKind.RelativeOrAbsolute));
                    images.Add(img);
                    fv.ItemsSource = images;
                }
                qtyElementl++;
            }
            catch
            { 
            
            }
           


        }

    }
}

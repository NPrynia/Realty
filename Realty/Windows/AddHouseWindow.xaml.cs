using ModernWpf.Controls;
using Realty.ClassHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Color = System.Drawing.Color;
using Image = System.Windows.Controls.Image;
using MessageBox = System.Windows.MessageBox;

namespace Realty.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddHouseWindow.xaml
    /// </summary>
    public partial class AddHouseWindow : Window
    {
        List<Image> images = new List<Image>();
        EF.House edithouse = new EF.House();
        List<int> listQtyFlour = new List<int>()

        {
            1,
            2,
            3,
            4
           ,
        };
        public string pathPhoto;
        List<string> listQtyRoom = new List<string>();
        List<byte[]> imageInByte = new List<byte[]>();
        bool isChange = false;
        public AddHouseWindow()
        {
            InitializeComponent();

            for (int i=1 ; i < 32; i++)
            {
                listQtyRoom.Add(Convert.ToString(i));
            }
            cbQtyFloor.ItemsSource = listQtyFlour;
            cbQtyRoom.ItemsSource = listQtyRoom;
            cbQtyRoom.MaxDropDownHeight =200;


        }

        public AddHouseWindow(EF.House house)
        {
            InitializeComponent();
            btnOffer.Width = 200;
            List<string> qtyRoom = new List<string>();
            List<string> qtyFloor = new List<string>();
            qtyRoom.Add(Convert.ToString(house.QtyRoom));
            qtyFloor.Add(Convert.ToString(house.QtyFloor));
            btnOffer.Width = 200;
            tbCost.Text = Convert.ToString(house.Cost);
            tbSpaceHome.Text = Convert.ToString(house.Space);
            tbSpaceArea.Text = Convert.ToString(house.SpaceArea);
            cbQtyFloor.ItemsSource = qtyFloor;
            cbQtyRoom.ItemsSource = qtyRoom;
            cbQtyFloor.SelectedIndex = 0;
            cbQtyRoom.SelectedIndex = 0;
            tbCost.Text = Convert.ToString(house.Cost);
            tbNumHome.Text = Convert.ToString(house.NumberHouse);
            tbDesciption.Text = Convert.ToString(house.Description);

            if (house.HasBath)
            {

                tsBath.IsOn = true;
            }
            if (house.HasGas)
            {

                tsGas.IsOn = true;
            }
            if (house.HasElectricity)
            {

                tsElectric.IsOn = true;
            }
            if (house.HasPlumbing)
            {

                tsPlumbing.IsOn = true;
            }
            if (house.HasPool)
            {

                tsPool.IsOn = true;
            }

            if (house.ForRent == true)
            {
                rbRennt.IsChecked = true;
            }
            else
            {
                rbSale.IsChecked = true;
            }


            var imageCollection = ClassHelper.AppData.Context.ImageHouse.Where(a => a.IDHouse == house.IDHouse).ToList();
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
                    imageInByte.Add(byteImg);
                }
            }

            fvImg.ItemsSource = images;
            edithouse = house;
            isChange = true;

            var adressCollection = ClassHelper.AppData.Context.Adress.Where(i => i.IDStreet == edithouse.IDStreet).ToList();
            var adress = from p in adressCollection select new { p.Adress1 }.Adress1;
            asbAdress.ItemsSource = adress;
            asbAdress.Text = adress.ToList()[0];


        }

      

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            if (ClassHelper.Validation.validationNum(tbCost.Text) == false)
            {
                MessageBox.Show("Некорректная цена", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ClassHelper.Validation.validationNum(tbSpaceHome.Text) == false)
            {
                MessageBox.Show("Некорректная площадь дома", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ClassHelper.Validation.validationNum(tbSpaceArea.Text) == false)
            {
                MessageBox.Show("Некорректная площадь", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cbQtyFloor.SelectedIndex <0)
            {
                MessageBox.Show("Выберите количество этажей", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (cbQtyRoom.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите количество комнат", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var adressCollection = ClassHelper.AppData.Context.Adress.ToList();
            var idStreet = from p in adressCollection
                           where p.Adress1 == asbAdress.Text
                           select new { p.IDStreet }.IDStreet;

            if (idStreet.Count() == 0)
            {
                MessageBox.Show("Выберите адресс", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ClassHelper.Validation.validationNum(tbNumHome.Text) == false)
            {
                MessageBox.Show("Некорректый номер дома", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (rbRennt.IsChecked == false && rbSale.IsChecked == false)
            {
                MessageBox.Show("Выберите для аренды или для продажи", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }




            //add
            if (isChange == false)
            {
                EF.House addHouse = new EF.House();
                addHouse.Isdelete = false;
                addHouse.Cost = Convert.ToDecimal(tbCost.Text);
                addHouse.Space = Convert.ToDouble(tbSpaceHome.Text);
                addHouse.SpaceArea = Convert.ToDouble(tbSpaceArea.Text);
                addHouse.QtyFloor = Convert.ToInt32(cbQtyFloor.Text);
                addHouse.QtyRoom = Convert.ToInt32(cbQtyRoom.Text);
                addHouse.IDStreet = idStreet.ToList()[0];
                addHouse.NumberHouse = tbNumHome.Text;
                addHouse.Description = (tbDesciption.Text);
                if (tsPool.IsOn)
                {

                    addHouse.HasPool = true;
                }
                else
                {
                    addHouse.HasPool = false;
                }

                if (tsBath.IsOn)
                {

                    addHouse.HasBath = true;
                }
                else
                {
                    addHouse.HasBath = false;
                }

                if (tsPlumbing.IsOn)
                {

                    addHouse.HasPlumbing = true;
                }
                else
                {
                    addHouse.HasPlumbing = false;
                }

                if (tsGas.IsOn)
                {

                    addHouse.HasGas = true;
                }
                else
                {
                    addHouse.HasGas = false;
                }

                if (tsElectric.IsOn)
                {

                    addHouse.HasElectricity = true;
                }
                else
                {
                    addHouse.HasElectricity = false;
                }

                if (rbRennt.IsChecked == true)
                {

                    addHouse.ForRent = true;
                }
                else
                {
                    addHouse.ForRent = false;
                }

                AppData.Context.House.Add(addHouse);
                AppData.Context.SaveChanges();
                AppData.Context.SaveChangesAsync();

                EF.ImageHouse image = new EF.ImageHouse();
                if (images.Count != 0)
                {
                    foreach (byte[] b in imageInByte)
                    {
                        image.Image = b;
                        image.IDHouse = addHouse.IDHouse;
                        AppData.Context.ImageHouse.Add(image);
                        AppData.Context.SaveChanges();
                    }
                }
                else 
                {
                    var imageDefault = AppData.Context.ImageHouse.Where(i => i.IDImageHouse == 11).ToList().FirstOrDefault();
                    image.Image = imageDefault.Image;
                    image.IDHouse = addHouse.IDHouse;
                    AppData.Context.ImageHouse.Add(image);
                    AppData.Context.SaveChanges();

                }


                MessageBox.Show("Дом добавлен");
                this.Close();
            }


            //change
            else
            {
                edithouse.Cost = Convert.ToDecimal(tbCost.Text);
                edithouse.Space = Convert.ToDouble(tbSpaceHome.Text);
                edithouse.SpaceArea = Convert.ToDouble(tbSpaceArea.Text);
                edithouse.QtyRoom = Convert.ToInt32(cbQtyFloor.Text);
                edithouse.QtyFloor = Convert.ToInt32(cbQtyRoom.Text);
                edithouse.IDStreet = idStreet.ToList()[0];
                edithouse.NumberHouse = tbNumHome.Text;
                edithouse.Description = (tbDesciption.Text);
                if (tsBath.IsOn)
                {

                    edithouse.HasBath = true;
                }
                if (tsPool.IsOn)
                {

                    edithouse.HasPool = true;
                }
                if (tsPlumbing.IsOn == true)
                {

                    edithouse.HasPlumbing = true;
                }
                if (tsGas.IsOn == true)
                {

                    edithouse.HasGas = true;
                }
                if (tsElectric.IsOn == true)
                {

                    edithouse.HasElectricity = true;
                }


                if (rbRennt.IsChecked == true)
                {

                    edithouse.ForRent = true;
                }
                else
                {
                    edithouse.ForRent = false;
                }

                EF.ImageHouse image = new EF.ImageHouse();
                var imgHouse = ClassHelper.AppData.Context.ImageHouse.Where(i => i.IDHouse == edithouse.IDHouse).ToList();
                var imgHouseCopy = ClassHelper.AppData.Context.ImageHouse.Where(i => i.IDHouse == edithouse.IDHouse).ToList();
                foreach (var i in imgHouseCopy)
                {
                    imgHouse.Remove(i);
                    AppData.Context.ImageHouse.Remove(i);
                    AppData.Context.SaveChanges();
                }

                if (images.Count != 0)
                {
                    foreach (byte[] b in imageInByte)
                    {
                        image.Image = b;
                        image.IDHouse = edithouse.IDHouse;
                        AppData.Context.ImageHouse.Add(image);
                        AppData.Context.SaveChanges();
                    }
                }
                else
                {
                    var imageDefault = AppData.Context.ImageHouse.Where(i => i.IDImageHouse == 11).ToList().FirstOrDefault();
                    image.Image = imageDefault.Image;
                    image.IDHouse = edithouse.IDHouse;
                    AppData.Context.ImageHouse.Add(image);
                }


                MessageBox.Show("Квартира cохранена");
                AppData.Context.SaveChanges();
                this.Close();

            }
           

               
           
        }

        private void asbAdress_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {

            if (isChange == false)
            {
                var adressCollection = ClassHelper.AppData.Context.Adress.ToList();
                var adress = from p in adressCollection select new { p.Adress1 }.Adress1;
                if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
                {

                    var suitableItems = new List<string>();
                    var splitText = sender.Text.ToLower().Split(' ');
                    foreach (var i in adress)
                    {
                        var found = splitText.All((key) =>
                        {
                            return i.ToLower().Contains(key);
                        });
                        if (found)
                        {
                            suitableItems.Add(i);
                        }
                    }
                    if (suitableItems.Count == 0)
                    {
                        suitableItems.Add("Нет результатов");
                    }
                    sender.ItemsSource = suitableItems;
                }
            }
            else
            {

            }


        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog(); 
            if (openFile.ShowDialog() == true)
            {
                Image i = new Image();
                i.Source = new BitmapImage(new Uri(openFile.FileName));
                pathPhoto = openFile.FileName;
                images.Add(i);
                imageInByte.Add(File.ReadAllBytes(pathPhoto));
                fvImg.ItemsSource = images;
                
            }

        }

        private void tbNumHouse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbNumHome.Text = "";
        }

        private void btnOffer_Click(object sender, RoutedEventArgs e)
        {
            Windows.OfferWindow offerWindow = new Windows.OfferWindow(edithouse);
            offerWindow.ShowDialog();
        }

        private void btnCleanPhoto_Click(object sender, RoutedEventArgs e)
        {
            if (imageInByte.Count == 0)
            {

                MessageBox.Show("Все фото удалены");
                List<Image> emptyimg = new List<Image>();
                fvImg.ItemsSource = emptyimg;
            }
            else
            {
                images.Remove(images[images.Count - 1]);
                imageInByte.Remove(imageInByte[imageInByte.Count - 1]);

                fvImg.ItemsSource = images;
            }



        }
    }

   
}

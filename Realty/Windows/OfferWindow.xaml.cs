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
using System.Windows.Shapes;

namespace Realty.Windows
{
    /// <summary>
    /// Логика взаимодействия для OfferWindow.xaml
    /// </summary>
    public partial class OfferWindow : Window
    {
        bool isRent = false;
        bool isHouse = false;
        bool hasClient = false;
        decimal CostProperty;

        EF.Client client = new EF.Client();

        EF.Apartment oferApartment = new EF.Apartment();
        EF.House oferHouse = new EF.House();
        public OfferWindow(EF.Apartment apartment)
        {
            InitializeComponent();
            if (apartment.ForRent == true)
            {
                tbTypeDeal.Text = "Аренда";
                isRent = true;
                dpStartRent.IsEnabled = true;
                dpEndRent.IsEnabled = true;
            }
            else
            {
                tbFinalCost.Text = Convert.ToString(apartment.Cost);
            }
            CostProperty = apartment.Cost;
            oferApartment = apartment;
            dpStartRent.DisplayDateStart = DateTime.Now;
        }
        public OfferWindow(EF.House house)
        {
            InitializeComponent();
            if (house.ForRent == true)
            {
                tbTypeDeal.Text = "Аренда";
                isRent = true;
                dpStartRent.IsEnabled = true;
                dpEndRent.IsEnabled = true;
            }
            else
            {
                tbFinalCost.Text = Convert.ToString(house.Cost);
            }
            CostProperty = house.Cost;
            tbTypeProperty.Text = "Дом";
            oferHouse = house;
            isHouse = true;
            dpStartRent.DisplayDateStart = DateTime.Now;
        }

        private void btnOffer_Click(object sender, RoutedEventArgs e)
        {
            //Validation

            if (ClassHelper.Validation.validationText(tbSurname.Text) == false)
            {
                MessageBox.Show("Некорректная фамилия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ClassHelper.Validation.validationText(tbFirstName.Text) == false)
            {
                MessageBox.Show("Некорректное имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ClassHelper.Validation.validationNum(tbPhone.Text) == false)
            {
                MessageBox.Show("Некорректный телефон", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tbPhone.Text.Length < 12)
            {
                MessageBox.Show("Введите телефон полностью", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tbNumPss.Text.Length != 4)
            {
                MessageBox.Show("Номер паспорта должен состоять из 4 цифр", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ClassHelper.Validation.validationNum(tbNumPss.Text) == false)
            {
                MessageBox.Show("Некорректный номер паспорта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (ClassHelper.Validation.validationNum(tbSerialPuss.Text) == false)
            {
                MessageBox.Show("Некорректная серия паспорта", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tbSerialPuss.Text.Length != 6)
            {
                MessageBox.Show("Серия паспорта должен состоять из 6 цифр", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }




            if (isHouse == true)
            {

                if (isRent == false)
                {

                    EF.SaleHouse saleHouse = new EF.SaleHouse();
                    if (hasClient == false)
                    {
                        EF.Client addClient = new EF.Client();

                        addClient.Surname = tbSurname.Text;
                        addClient.FirstName = tbFirstName.Text;
                        addClient.Phone = tbPhone.Text;
                        addClient.NumberPassport = tbNumPss.Text;
                        addClient.SerialPassport = tbSerialPuss.Text;

                        ClassHelper.AppData.Context.Client.Add(addClient);
                        ClassHelper.AppData.Context.SaveChanges();
                        saleHouse.IDHouse = oferHouse.IDHouse;
                        saleHouse.IDClient = addClient.IDClient;
                        saleHouse.Date = DateTime.Now;
                        saleHouse.IsDelete = false;

                        oferHouse.Isdelete = true;
                        ClassHelper.AppData.Context.SaleHouse.Add(saleHouse);
                        ClassHelper.AppData.Context.SaveChanges();
                        MessageBox.Show("Продано!");
                        this.Close();
                    }
                    else
                    {
                        saleHouse.IDHouse = oferHouse.IDHouse;
                        saleHouse.IDClient = client.IDClient;
                        saleHouse.Date = DateTime.Now;
                        saleHouse.IsDelete = false;

                        oferHouse.Isdelete = true;
                        ClassHelper.AppData.Context.SaleHouse.Add(saleHouse);
                        ClassHelper.AppData.Context.SaveChanges();
                        MessageBox.Show("Продано!");
                        this.Close();
                    }

                    
                   
                }

                else
                {
                    if (dpStartRent.SelectedDate == null)
                    {
                        MessageBox.Show("Выберите дату начала аренды", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }


                    if (dpEndRent.SelectedDate == null)
                    {
                        MessageBox.Show("Выберите дату конца аренды", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (dpStartRent.SelectedDate > dpEndRent.SelectedDate)
                    {
                        MessageBox.Show("Дата начала аренды не может быть больше", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    EF.RentHouse RentHouse = new EF.RentHouse();

                    if (hasClient == false)
                    {

                        EF.Client addClient = new EF.Client();
                        addClient.Surname = tbSurname.Text;
                        addClient.FirstName = tbFirstName.Text;
                        addClient.Phone = tbPhone.Text;
                        addClient.NumberPassport = tbNumPss.Text;
                        addClient.SerialPassport = tbSerialPuss.Text;

                        ClassHelper.AppData.Context.Client.Add(addClient);
                        ClassHelper.AppData.Context.SaveChanges();


                        RentHouse.IDHouse = oferHouse.IDHouse;
                        RentHouse.IDClient = addClient.IDClient;
                        RentHouse.DateStartRent = (DateTime)dpStartRent.SelectedDate;
                        RentHouse.DateEndRent = (DateTime)dpEndRent.SelectedDate;
                        RentHouse.IsDelete = false;
                        RentHouse.FinalCost = costRent((DateTime)dpStartRent.SelectedDate, (DateTime)dpEndRent.SelectedDate, oferHouse.Cost);

                        ClassHelper.AppData.Context.RentHouse.Add(RentHouse);
                        ClassHelper.AppData.Context.SaveChanges();
                        MessageBox.Show("Арендовано!");
                        this.Close();
                    }
                    else 
                    {
                        RentHouse.IDHouse = oferHouse.IDHouse;
                        RentHouse.IDClient = client.IDClient;
                        RentHouse.DateStartRent = (DateTime)dpStartRent.SelectedDate;
                        RentHouse.DateEndRent = (DateTime)dpEndRent.SelectedDate;
                        RentHouse.IsDelete = false;
                        RentHouse.FinalCost = costRent((DateTime)dpStartRent.SelectedDate, (DateTime)dpEndRent.SelectedDate, oferHouse.Cost);

                        ClassHelper.AppData.Context.RentHouse.Add(RentHouse);
                        ClassHelper.AppData.Context.SaveChanges();
                        MessageBox.Show("Арендовано!");
                        this.Close();
                    }
                       
                   
                    

                }
            }




            else
            {
                if (isRent == false)
                {


                    EF.SaleApartment saleApartment = new EF.SaleApartment();
                    if (hasClient == false)
                    {
                        EF.Client addClient = new EF.Client();

                        addClient.Surname = tbSurname.Text;
                        addClient.FirstName = tbFirstName.Text;
                        addClient.Phone = tbPhone.Text;
                        addClient.NumberPassport = tbNumPss.Text;
                        addClient.SerialPassport = tbSerialPuss.Text;

                        ClassHelper.AppData.Context.Client.Add(addClient);
                        ClassHelper.AppData.Context.SaveChanges();

                        saleApartment.IDApartment = oferApartment.IDApartment;
                        saleApartment.IDCLient = addClient.IDClient;
                        saleApartment.Date = DateTime.Now;
                        saleApartment.IsDelete = false;
                        oferApartment.IsDelete = true;

                        ClassHelper.AppData.Context.SaleApartment.Add(saleApartment);
                        ClassHelper.AppData.Context.SaveChanges();

                        MessageBox.Show("Продано!");
                        this.Close();
                    }
                    else 
                    {
                        saleApartment.IDApartment = oferApartment.IDApartment;
                        saleApartment.IDCLient = client.IDClient;
                        saleApartment.Date = DateTime.Now;
                        saleApartment.IsDelete = false;
                        oferApartment.IsDelete = true;

                        ClassHelper.AppData.Context.SaleApartment.Add(saleApartment);
                        ClassHelper.AppData.Context.SaveChanges();

                        MessageBox.Show("Продано!");
                        this.Close();
                    }
                       

                    

                    
                }

                else
                {

                    EF.RentApartment rentApartment = new EF.RentApartment();
                    if (hasClient == false)
                    {
                        EF.Client addClient = new EF.Client();

                        addClient.Surname = tbSurname.Text;
                        addClient.FirstName = tbFirstName.Text;
                        addClient.Phone = tbPhone.Text;
                        addClient.NumberPassport = tbNumPss.Text;
                        addClient.SerialPassport = tbSerialPuss.Text;

                        ClassHelper.AppData.Context.Client.Add(addClient);
                        ClassHelper.AppData.Context.SaveChanges();

                        rentApartment.IDApartment = oferApartment.IDApartment;
                        rentApartment.IDClient = addClient.IDClient;
                        rentApartment.DateStartRent = (DateTime)dpStartRent.SelectedDate;
                        rentApartment.DateEndRent = (DateTime)dpEndRent.SelectedDate;
                        rentApartment.IsDelete = false;
                        rentApartment.FinalCost = costRent((DateTime)dpStartRent.SelectedDate, (DateTime)dpEndRent.SelectedDate, oferApartment.Cost);
                        ClassHelper.AppData.Context.RentApartment.Add(rentApartment);
                        ClassHelper.AppData.Context.SaveChanges();

                        MessageBox.Show("Арендовано!");
                        this.Close();
                    }
                    else 
                    {
                        rentApartment.IDApartment = oferApartment.IDApartment;
                        rentApartment.IDClient = client.IDClient;
                        rentApartment.DateStartRent = (DateTime)dpStartRent.SelectedDate;
                        rentApartment.DateEndRent = (DateTime)dpEndRent.SelectedDate;
                        rentApartment.IsDelete = false;
                        rentApartment.FinalCost = costRent((DateTime)dpStartRent.SelectedDate, (DateTime)dpEndRent.SelectedDate, oferApartment.Cost);
                        ClassHelper.AppData.Context.RentApartment.Add(rentApartment);
                        ClassHelper.AppData.Context.SaveChanges();

                        MessageBox.Show("Арендовано!");
                        this.Close();
                    }
                      
                   
                  
                }
            }
        }


        public static decimal costRent(DateTime dateStart, DateTime dateEnd, decimal cost)
        {
            try
            {

                double qtyDay = (dateEnd - dateStart).TotalDays;
                if (qtyDay == 0)
                {
                    qtyDay = 1;
                }
                return Math.Round((decimal)(cost/30)) * Convert.ToInt32(qtyDay);
            }
            catch
            {
                return 0;
            }


        }

        private void DpStartRent_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
                tbFinalCost.Text = Convert.ToString(costRent((DateTime)dpStartRent.SelectedDate, (DateTime)dpEndRent.SelectedDate, CostProperty));
            }
            catch
            {
            }
            
        }

        private void btnClient_Click(object sender, RoutedEventArgs e)
        {
            ListClient listClient = new ListClient();
            if (listClient.ShowDialog().Value == true)
            {
                hasClient = true;
                client = ClassHelper.Client.client;
                tbSurname.Text = client.Surname;
                tbFirstName.Text = client.FirstName;
                tbPhone.Text = client.Phone;
                tbSerialPuss.Text = client.SerialPassport;
                tbNumPss.Text = client.NumberPassport;
            }
        }
    }
}

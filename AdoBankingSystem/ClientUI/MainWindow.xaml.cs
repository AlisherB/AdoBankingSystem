using AdoBankingSystem.BLL.Services;
using AdoBankingSystem.Shared.DTOs;
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

namespace ClientUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RabbitMqBusService _rabbitMqBusService;

        public MainWindow()
        {
            _rabbitMqBusService = new RabbitMqBusService();
            InitializeComponent();
        }
        private void CountButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string firstName = firstNameTextBox.Text;
            string lastName = lastNameTextBox.Text;
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;
            string passwordConfirm = passwordConfirmTextBox.Text;

            BankClientDto bankClientDto = new BankClientDto()
            {
                FirstName = firstName,
                LastName = lastName,
                ApplicationClientType = ApplicationClientType.BankClient,
                CreatedTime = DateTime.Now,
                Email = firstName,
                EntityStatus = EntityStatusType.IsActive,
                PasswordHash = "12345"
            };

            _rabbitMqBusService.PublishMessageToQueue<BankClientDto>("bank_client_registration_queue", bankClientDto);
        }
        
    }
}

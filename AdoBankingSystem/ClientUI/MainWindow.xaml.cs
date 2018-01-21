using AdoBankingSystem.BLL.Services;
using AdoBankingSystem.Shared.DTOs;
using AdoBankingSystem.Shared.Utils;
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
        private OfflineStorageService _offlineDataPublisher;
        private RabbitMqBusService _onlineDataPublisher;
        private ApplicationClientService _applicationClientService;
        public MainWindow()
        {
            _onlineDataPublisher = new RabbitMqBusService();
            InitializeComponent();
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
                Email = firstName,
                PasswordHash = "12345",
                ApplicationClientType = ApplicationClientType.BankClient,
                CreatedTime = DateTime.Now,
                EntityStatus = EntityStatusType.IsActive
            };

            if (ConnectionManagerUtil.IsConnectionAvailable())
            {
                var pastData = _offlineDataPublisher.GetAllPastData<BankClientDto>();
                if (pastData.Count > 0)
                {
                    var sortedData = pastData.OrderBy(p => p.CreatedTime);
                    foreach (var item in sortedData)
                    {
                        _onlineDataPublisher.PublishMessageToStorage(item);
                    }
                }

                _onlineDataPublisher.PublishMessageToStorage(bankClientDto);
            }
            else _offlineDataPublisher.PublishMessageToStorage(bankClientDto);

            //_onlineDataPublisher.PublishMessageToQueue<BankClientDto>("bank_client_registration_queue", bankClientDto);
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

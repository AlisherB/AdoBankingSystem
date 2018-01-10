using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.BLL.Services
{
    public class OfflineStorageService : IDataPublisher
    {
        private readonly BankClientOfflineDao dao;
        public void PublishMessageToStorage<T>(string storeName, T data) where T : class
        {

        }
        public OfflineStorageService()
        {
            dao = new BankClientOfflineDao();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.BLL.Services
{
    public interface IDataPublisher
    {
        void PublishMessageToStorage<T>(string storeName, T data) where T : class;
    }
}

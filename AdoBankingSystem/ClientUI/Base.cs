using AdoBankingSystem.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI
{
    public  class Base
    {
        protected ApplicationClientService _applicationClientService;
        public Base()
        {
            _applicationClientService = new ApplicationClientService();
        }
    }
}

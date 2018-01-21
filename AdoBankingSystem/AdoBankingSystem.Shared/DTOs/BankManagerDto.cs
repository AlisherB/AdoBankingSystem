using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.Shared.DTOs
{
    public class BankManagerDto : ApplicationClientDto
    {
        public BankManagerDto()
        {
        }

        public BankManagerDto(string firstName, string lastName, string email, string passwordHash) : base(firstName, lastName, email, passwordHash)
        {
        }

        public BankManagerDto(string firstName, string lastName, string email, string password, string passwordConfirmation)
        {

        }
    }
}

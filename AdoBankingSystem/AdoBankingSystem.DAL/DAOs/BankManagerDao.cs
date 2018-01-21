using AdoBankingSystem.DAL.Interfaces;
using AdoBankingSystem.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoBankingSystem.DAL.DAOs
{
    public class BankManagerDao : IDAO<BankManagerDto>
    {
        private SqlConnection sqlConnection = null;
        private BankManagerDto bankManagerDTOToReturn;

        public string Create(BankManagerDto record)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                SqlParameter idParameter = new SqlParameter("@Id", SqlDbType.VarChar);
                SqlParameter firstNameParameter = new SqlParameter("@FirstName", SqlDbType.VarChar);
                SqlParameter lastNameParameter = new SqlParameter("@LastName", SqlDbType.VarChar);
                SqlParameter emailParameter = new SqlParameter("@Email", SqlDbType.VarChar);
                SqlParameter passwordHashParameter = new SqlParameter("@PasswordHash", SqlDbType.VarChar);
                SqlParameter createdTimeParameter = new SqlParameter("@CreatedTime", SqlDbType.DateTime);
                SqlParameter entityStatusParameter = new SqlParameter("@EntityStatus", SqlDbType.Int);

                idParameter.Value = record.Id;
                firstNameParameter.Value = record.FirstName;
                lastNameParameter.Value = record.LastName;
                emailParameter.Value = record.Email;
                passwordHashParameter.Value = record.PasswordHash;
                createdTimeParameter.Value = record.CreatedTime;
                entityStatusParameter.Value = record.EntityStatus;

                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "CreateNewBankManager";
                    sqlCommand.CommandType = CommandType.StoredProcedure;

                    sqlCommand.Parameters.Add(idParameter);
                    sqlCommand.Parameters.Add(firstNameParameter);
                    sqlCommand.Parameters.Add(lastNameParameter);
                    sqlCommand.Parameters.Add(emailParameter);
                    sqlCommand.Parameters.Add(passwordHashParameter);
                    sqlCommand.Parameters.Add(createdTimeParameter);
                    sqlCommand.Parameters.Add(entityStatusParameter);

                    sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                    return record.Id;
                }
            }
        }

        public BankManagerDto Read(string id)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    string baseSelectQuery = @"SELECT * FROM [AdoBankingSystem].[dbo].[BankManager] WHERE [Id = {0}]";
                    string realSelectQuery = String.Format(baseSelectQuery, id.ToString());

                    sqlCommand.CommandText = realSelectQuery;
                    sqlCommand.CommandType = CommandType.Text;

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        bankManagerDTOToReturn = new BankManagerDto()
                        {
                            Id = reader["Id"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            CreatedTime = DateTime.Parse(reader["CreatedTime"].ToString()),
                            EntityStatus = (EntityStatusType)Int32.Parse(reader["EntityStatus"].ToString())
                        };
                    }
                }
                sqlConnection.Close();
            }
            return bankManagerDTOToReturn;
        }

        public ICollection<BankManagerDto> Read()
        {
            ICollection<BankManagerDto> users = new List<BankManagerDto>();
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                string realQuery = "SELECT * FROM dbo.BankManagers";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = realQuery;

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(new BankManagerDto()
                        {
                            Id = reader["Id"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PasswordHash = reader["PasswordHash"].ToString(),
                            CreatedTime = DateTime.Parse(reader["CreatedTime"].ToString()),
                            EntityStatus = (EntityStatusType)Int32.Parse(reader["EntityStatus"].ToString())
                        });
                    }
                }
                sqlConnection.Close();
            }
            return users;
        }

        public void Remove(string id)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                string baseQuery = "DELETE FROM [AdoBankingSystem].[dbo].[BankManager] WHERE Id = '{0}'";
                string realQuery = String.Format(baseQuery, id);

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(realQuery, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
                sqlConnection.Close();
            }
        }

        public string Update(BankManagerDto record)
        {
            using (sqlConnection = DatabaseConnectionFactory.GetConnection())
            {
                string baseQuery = "UPDATE [AdoBankingSystem].[dbo].[BankManager] SET Id = '{0}', Email = '{1}', FirstName = '{2}', LastName = {3}, PasswordHash = {4}, CreatedTime = {5}, EntityStatus = {6}";
                string realQuery = String.Format(baseQuery, record.Id, record.Email, record.FirstName, record.LastName, record.PasswordHash, record.CreatedTime, record.EntityStatus);

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(realQuery, sqlConnection))
                {
                    sqlConnection.Close();
                    sqlCommand.ExecuteNonQuery().ToString();
                }
                return record.ToString();
            }
        }
    }
}

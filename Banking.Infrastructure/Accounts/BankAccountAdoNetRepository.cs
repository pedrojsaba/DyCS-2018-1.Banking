using Banking.Domain.Accounts.Entity;
using Banking.Domain.Accounts.Repository;
using Banking.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Banking.Infrastructure.Accounts
{
    public class BankAccountAdoNetRepository : IBankAccountRepository
    {
        public bool AccountEnabled(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public BankAccount FindById(int bankAccountId)
        {
            throw new NotImplementedException();
        }

        public BankAccount FindByNumber(string accountNumber)
        {

            var bankAccount = new BankAccount();
            const string query = "SELECT [Bank_Account_Id],[number], [balance],[Customer_Id] FROM Bank_Account WHERE number = @accountNumber";

            using (var conn = new SqlConnection(Functions.GetConnectionString()))
            {

                var command = new SqlCommand(query, conn) { CommandType = CommandType.Text };
                command.Parameters.AddRange(new[]
                {
                    new SqlParameter
                    {
                        SqlDbType = SqlDbType.VarChar,
                        ParameterName = "@accountNumber",
                        Value = accountNumber
                    }
                });

                conn.Open();

                using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        bankAccount.setId(reader.GetInt64(reader.GetOrdinal("Bank_Account_Id")));
                        bankAccount.setNumber(reader.GetString(reader.GetOrdinal("number")));
                        bankAccount.setBalance (reader.GetDecimal(reader.GetOrdinal("balance")));
                    }
                }

            }

            return bankAccount;

        }

        public BankAccount FindByNumberLocked(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public List<BankAccount> GetByCustomerId(int customerId)
        {
            throw new NotImplementedException();
        }

        public List<BankAccount> GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public bool InsufficientBalance(string accountNumber, decimal amount)
        {
            throw new NotImplementedException();
        }

        public bool OwnAccount(string username, string accountNumber)
        {
            throw new NotImplementedException();
        }

        public void Update(BankAccount accountNumber)
        {


                SqlConnection conn = null;
                String sqlBank_AccountUpdate;
                SqlCommand cmdBank_AccountUpdate;
                SqlParameter prmUBank_Account_Id;
                SqlParameter prmUbalance;

                try
                {
                    conn = new SqlConnection(Functions.GetConnectionString());
                    sqlBank_AccountUpdate = "UpdateBank_Account";

                    cmdBank_AccountUpdate = new SqlCommand(sqlBank_AccountUpdate, conn);
                    cmdBank_AccountUpdate.CommandType = CommandType.StoredProcedure;

                    prmUBank_Account_Id = new SqlParameter();
                    prmUBank_Account_Id.ParameterName = "@Bank_Account_Id";
                    prmUBank_Account_Id.SqlDbType = SqlDbType.BigInt;
                    prmUBank_Account_Id.Value = accountNumber.getId();
                    cmdBank_AccountUpdate.Parameters.Add(prmUBank_Account_Id);

                    prmUbalance = new SqlParameter();
                    prmUbalance.ParameterName = "@balance";
                    prmUbalance.SqlDbType = SqlDbType.Decimal;
                    prmUbalance.Value = accountNumber.getBalance();
                    cmdBank_AccountUpdate.Parameters.Add(prmUbalance);

                    cmdBank_AccountUpdate.Connection.Open();
                    cmdBank_AccountUpdate.ExecuteNonQuery();

                    cmdBank_AccountUpdate.Connection.Close();
                    conn.Dispose();

                }
                catch (Exception ex)
                {
                    conn.Dispose();
                    throw ex;
                }

        }
    }
}

using Banking.Domain.Customers.Entity;
using Banking.Domain.Customers.Repository;
using Banking.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Banking.Infrastructure.Customers
{
    public class CustomerAdoNetRepository : ICustomerRepository
    {


        public List<CustomerDto> getCustomersDto(int skip, int pageSize)
        {

            SqlConnection conn = null;
            SqlDataReader drCustomers;
            String strSqlCustomer;
            SqlCommand cmdCustomer;
            SqlParameter prmskip = null;
            SqlParameter prmpageSize = null;

            try
            {
                CustomerDto objCustomer;
                List<CustomerDto> lstCustomers;

                conn = new SqlConnection(Functions.GetConnectionString());

                strSqlCustomer = "GetAllCustomer";

                cmdCustomer = new SqlCommand(strSqlCustomer, conn);
                cmdCustomer.CommandType = CommandType.StoredProcedure;

                prmskip = new SqlParameter();
                prmskip.ParameterName = "@skip";
                prmskip.SqlDbType = SqlDbType.Int;
                prmskip.Value = skip;
                cmdCustomer.Parameters.Add(prmskip);

                prmpageSize = new SqlParameter();
                prmpageSize.ParameterName = "@pageSize";
                prmpageSize.SqlDbType = SqlDbType.Int;
                prmpageSize.Value = pageSize;
                cmdCustomer.Parameters.Add(prmpageSize);

                cmdCustomer.Connection.Open();
                drCustomers = cmdCustomer.ExecuteReader();

                lstCustomers = new List<CustomerDto>();

                while (drCustomers.Read())
                {
                    objCustomer = new CustomerDto();
                    objCustomer.id = drCustomers.GetInt64(drCustomers.GetOrdinal("Customer_Id"));
                    objCustomer.name = drCustomers.GetString(drCustomers.GetOrdinal("First_Name"));
                    objCustomer.status ="Active";
                    lstCustomers.Add(objCustomer);
                }

                cmdCustomer.Connection.Close();
                conn.Dispose();



                return lstCustomers;

            }
            catch (Exception ex)
            {
                conn.Dispose();
                throw ex;
            }
        }


    }
}

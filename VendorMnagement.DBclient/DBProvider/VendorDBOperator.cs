using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.DBclient.DBProvider
{
    public class VendorDBOperator : IVendorDbOperator
    {
        private IConfiguration _configuration { get; }
        private SqlConnection connection { get; set; }

        public SqlCommand command { get; set; }

        public VendorDBOperator(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        private SqlConnection GetSqlConnection()
        {
            try
            {
                string connectionString = _configuration["ConnectionStrings:VendorMgmtConnectionString"];
                connection = new SqlConnection(connectionString);
                connection.Open();
            }
            catch(Exception ex)
            {
                if(connection != null && connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return connection;
        }

        private SqlCommand ConstructSqlCommand(string query, CommandType commandType, List<SqlParameter> parameters, int timeOut)
        {
            try
            {
                var connection = GetSqlConnection();
                if (connection == null)
                    return null;

                if (connection.State == ConnectionState.Open)
                {
                    command = connection.CreateCommand();
                    command.CommandType = commandType;
                    command.CommandText = query;
                    if(timeOut == 0)
                      command.CommandTimeout=  timeOut;
                    if(parameters != null)
                        command.Parameters.AddRange(parameters.ToArray<SqlParameter>());    
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return command;
        }

        public void InitializeOperator(string query, CommandType commandType, List<SqlParameter> parameters, int timeOut = 30)
        {
            ConstructSqlCommand(query, commandType, parameters, timeOut); 
        }
    }
}

using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.DBclient.DBProvider
{
    public interface IVendorDbOperator
    {
        public SqlCommand command { get; set; }
        //SqlCommand GetSqlCommand(string query,CommandType commandType, List<SqlParameter> parameters,int timeOut);

        void InitializeOperator(string query, CommandType commandType, List<SqlParameter> parameters, int timeOut = 30);
    }
}

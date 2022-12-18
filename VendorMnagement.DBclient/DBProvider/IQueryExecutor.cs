using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.DBclient.DBProvider
{
    public interface IQueryExecutor
    {
        IDataReader ExecuteReader();

        int ExecuteQuery();

        object ExecuteScalar();
    }
}

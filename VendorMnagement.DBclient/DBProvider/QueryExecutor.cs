using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.DBclient.DBProvider
{
    public class QueryExecutor : IQueryExecutor
    {
        public readonly IVendorDbOperator _vendorDbOperator;

        public QueryExecutor(IVendorDbOperator vendorDbOperator)
        {
            _vendorDbOperator = vendorDbOperator;
        }

        public object ExecuteScalar()
        {
            if (_vendorDbOperator == null)
            {
                throw new Exception("VendorDbOperator cannot be null");
            }
            try
            {
                return _vendorDbOperator.command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                return ex;
            }
        }

        public IDataReader ExecuteReader()
        {
            if(_vendorDbOperator == null )
            {
                throw new Exception("VendorDbOperator cannot be null");
            }
            try
            {
                return _vendorDbOperator.command.ExecuteReader();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public int ExecuteQuery()
        {
            if (_vendorDbOperator == null)
            {
                throw new Exception("VendorDbOperator cannot be null");
            }
            try
            {
                return _vendorDbOperator.command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}

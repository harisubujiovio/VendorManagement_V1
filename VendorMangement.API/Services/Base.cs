using Microsoft.Data.SqlClient;
using System.Data;

namespace VendorMangement.API.Services
{
    public class Base
    {
        public int AgainstInt(object obj)
        {

            if (obj == DBNull.Value)
                return 0;

            if (null == obj)
                return 0;

            if (string.Empty == obj.ToString())
                return 0;

            return Convert.ToInt32(obj);
        }
        public long AgainstLong(object obj)
        {

            if (obj == DBNull.Value)
                return -1;

            if (null == obj)
                return -1;

            if (string.Empty == obj.ToString())
                return -1;

            return Convert.ToInt64(obj);
        }

        public short AgainstShort(object obj)
        {

            if (obj == DBNull.Value)
                return -1;

            if (null == obj)
                return -1;

            if (string.Empty == obj.ToString())
                return -1;

            return Convert.ToInt16(obj);
        }

        public int? AgainstNullableInt(object obj)
        {

            if (obj == DBNull.Value)
                return null;

            if (null == obj)
                return null;

            if (string.Empty == obj.ToString())
                return null;


            return Convert.ToInt32(obj);
        }
        public long? AgainstNullableLong(object obj)
        {

            if (obj == DBNull.Value)
                return null;

            if (null == obj)
                return null;

            if (string.Empty == obj.ToString())
                return null;


            return Convert.ToInt64(obj);
        }
        public double AgainstDouble(object obj)
        {

            if (obj == DBNull.Value)
                return -1;

            if (null == obj)
                return -1;

            if (string.Empty == obj.ToString())
                return -1;

            return Convert.ToDouble(obj);
        }
        public double? AgainstNullableDouble(object obj)
        {

            if (obj == DBNull.Value)
                return null;

            if (null == obj)
                return null;

            if (string.Empty == obj.ToString())
                return null;

            return Convert.ToDouble(obj);
        }
        public string AgainstString(object obj)
        {
            if (obj == DBNull.Value)
                return string.Empty;
            if (null == obj)
                return string.Empty;

            return Convert.ToString(obj);
        }
        public Guid AgainstGUID(object obj)
        {
            return new Guid(obj.ToString());
        }
        public string AgainstString(object obj, bool fromExport)
        {
            if (obj == DBNull.Value)
                return string.Empty;
            if (null == obj)
                return string.Empty;

            return Convert.ToString(obj);
        }
        public bool AgainstBit(object obj)
        {
            if (obj == DBNull.Value)
                return false;
            if (null == obj)
                return false;

            if (string.Empty == obj.ToString())
                return false;

            return Convert.ToBoolean(obj);
        }
        public byte[] AgainstByte(object obj)
        {
            byte[] retval = null;
            if (obj == DBNull.Value)
                return retval;
            if (null == obj)
                return retval;

            if (string.Empty == obj.ToString())
                return retval;

            byte[] binaryString = (byte[])obj;

            return binaryString;
        }
        public bool? AgainstNullableBit(object obj)
        {
            if (obj == DBNull.Value)
                return null;
            if (null == obj)
                return null;

            if (string.Empty == obj.ToString())
                return null;

            return Convert.ToBoolean(obj);
        }
        public DateTime? AgainstNullableDatetime(object obj)
        {

            if (obj == DBNull.Value)
                return null;

            if (null == obj)
                return null;

            if (string.Empty == obj.ToString())
                return null;

            return Convert.ToDateTime(obj);
        }
        public DateTime AgainstDatetime(object obj)
        {

            if (obj == DBNull.Value)
                return DateTime.MinValue;

            if (null == obj)
                return DateTime.MinValue;

            if (string.Empty == obj.ToString())
                return DateTime.MinValue;

            return Convert.ToDateTime(obj);
        }
        public TimeSpan? AgainstNullableTimeSpan(object obj)
        {

            if (obj == DBNull.Value)
                return null;

            if (null == obj)
                return null;

            if (string.Empty == obj.ToString())
                return null;

            return TimeSpan.Parse(obj.ToString());
        }
        public TimeSpan AgainstTimeSpan(object obj)
        {

            if (obj == DBNull.Value)
                return new TimeSpan();

            if (null == obj)
                return new TimeSpan();

            if (string.Empty == obj.ToString())
                return new TimeSpan();

            return TimeSpan.Parse(obj.ToString());
        }
        public List<SqlParameter> GetPaginationParameters(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@PageNo";
            sqlParameter.SqlDbType = SqlDbType.Int;
            sqlParameter.Value = pageNo + 1;
            parameters.Add(sqlParameter);

            sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@PageSize";
            sqlParameter.SqlDbType = SqlDbType.Int;
            sqlParameter.Value = pageSize;
            parameters.Add(sqlParameter);

            if(!string.IsNullOrEmpty(sortCol))
            {
                sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@SortingCol";
                sqlParameter.SqlDbType = SqlDbType.VarChar;
                sqlParameter.Value = sortCol;
                parameters.Add(sqlParameter);
            }
            if (!string.IsNullOrEmpty(sortType))
            {
                sqlParameter = new SqlParameter();
                sqlParameter.ParameterName = "@SortType";
                sqlParameter.SqlDbType = SqlDbType.VarChar;
                sqlParameter.Value = sortType;
                parameters.Add(sqlParameter);
            }
            return parameters;
        }
    }
}

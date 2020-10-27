using System;
using System.Data.SqlClient;

namespace ConnectionGateway
{
    public interface IDBContext
    {
        SqlConnection GetConn();
        SqlCommand GetCommand();
        string GetAccessKey();
    }
}

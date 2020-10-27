using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ConnectionGateway
{
    public class DBContext : IDBContext
    {
        private IConfiguration _config;
        private string _connectionString;
        private string _accessKey;
        private string _serverIP;
        private SqlConnection _connection;
        private SqlCommand _cmd;
        //private ICRUD _iCRUD;
        public DBContext(IConfiguration configuration)
        {
            _config = configuration;
            _connectionString = _config.GetConnectionString("DefaultConnection");
            _serverIP = _config.GetConnectionString("ServerIP");
            _accessKey = _config.GetConnectionString("AccessKey");
        }
        public SqlConnection GetConn()
        {
            _connection = new SqlConnection(_connectionString);
            return _connection;
        }
        public SqlCommand GetCommand()
        {
            _cmd = _connection.CreateCommand();
            return _cmd;
        }
        public string GetAccessKey()
        {
            return _accessKey;
        }
        public string GetServerIP()
        {
            return _serverIP;
        }

    }
}

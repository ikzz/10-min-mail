using Devart.Data.PostgreSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Npgsql;


namespace diplomv3
{
    public enum Status
{
    ConnectionOpen,
    ConnectionClosed,
    ConnectionNotInitialized,
    EmptyResult
}

public class dbconnection
{
    static private Status connectionstatus = Status.ConnectionNotInitialized;
    static public Status ConnectionStatus
    {
        get { return connectionstatus; }
        set { connectionstatus = value; }
    }
    static private NpgsqlConnection Connection;

    public static void OpenConnection()
    {
        if ((ConnectionStatus == Status.ConnectionNotInitialized) || (ConnectionStatus == Status.ConnectionClosed))
        {
            string connstring = "Server=localhost;User Id=hmail;Password=22;Port=5432;Database=hmailserverdb;Timeout=300;CommandTimeout = 300";
            Connection = new NpgsqlConnection(connstring);
            Connection.Open();
            ConnectionStatus = Status.ConnectionOpen;
        }
    }
    public static void CloseConnection()
    {
        if (ConnectionStatus == Status.ConnectionOpen)
        {
            Connection.Close();
            ConnectionStatus = Status.ConnectionClosed;
        }
    }
    public static DataTable Request(string sql)
    {
        if (ConnectionStatus != Status.ConnectionOpen)
        {
            dbconnection.OpenConnection();
        }
        NpgsqlCommand command = new NpgsqlCommand(sql, Connection);
        var adapter = new NpgsqlDataAdapter(command);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        return dt;
    }

    public static void Execute(string sql)
    {
        if (ConnectionStatus != Status.ConnectionOpen)
        {
            dbconnection.OpenConnection();
        }
        NpgsqlCommand command = new NpgsqlCommand(sql, Connection);
        command.ExecuteNonQuery();
    }
}
}

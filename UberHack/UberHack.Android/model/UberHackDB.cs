using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;

namespace UberHack.Model
{
    public class problema
    {
        public problema(int tipo_id, double lat, double longi, bool resolvido)
        {
            problema_tipo_id = tipo_id;
            latirude = lat;
            longitude = longi;
            data_insercao = DateTime.Now;
            resolvido = false;
        }

        public int problema_tipo_id;
        public double latirude;
        public double longitude;
        public DateTime data_insercao;
        public bool resolvido;
    }
    public class Connect
    {
        //Feito na pressa infinita, nunca que a connection string iria ficar assim
        static string connectionString = @"Data Source=laurasan.database.windows.net;Initial Catalog=Uber_Hack_DB;Persist Security Info=True;User ID=lauragabrielasan;Password=wololo@123456";

        public static void CriaUsuario(string nome, string senha)
        {
            
            string selectQuery = "insert into usuario values ('Laura','123456')";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //open connection
                    connection.Open();

                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    command.Connection = connection;
                    command.CommandText = selectQuery;
                    var result = command.ExecuteReader();
                    //check if account exists
                    var exists = result.HasRows;
                }
            }
            catch (Exception exception)
            {
            }
        }

        public static List<problema> GetAllProblemas(string nome, string senha)
        {
            string connectionString = @"Data Source=laurasan.database.windows.net;Initial Catalog=Uber_Hack_DB;Persist Security Info=True;User ID=lauragabrielasan;Password=wololo@123456";
            string selectQuery = "select * from problema";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //open connection
                    connection.Open();

                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    command.Connection = connection;
                    command.CommandText = selectQuery;
                    var result = command.ExecuteReader();
                    //check if account exists
                    var exists = result.HasRows;

                    DataTable table = new DataTable();
                    table.Load(result);

                    List<problema> problemas = new List<problema>();

                    foreach (DataRow row in table.Rows)
                        problemas.Add(new problema(int.Parse(row[0].ToString()), double.Parse(row[0].ToString()), double.Parse(row[0].ToString()), bool.Parse(row[0].ToString())));

                    return problemas;
                }
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public static void InserirProblema(problema problem)
        {

            string resolvido = "0";

            if (problem.resolvido)
                resolvido = "1";
                       
            string selectQuery = "insert into problema values(" + problem.problema_tipo_id.ToString() + ", " + problem.latirude.ToString() + ", " + problem.longitude.ToString() + ", '" + problem.data_insercao.ToString("yyyy-MM-dd hh:mm:ss") + "', " + resolvido + ")";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //open connection
                    connection.Open();

                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    command.Connection = connection;
                    command.CommandText = selectQuery;
                    var result = command.ExecuteReader();
                   
                }
            }
            catch (Exception exception)
            {
            }
        }

        public static bool VerificaUsuario(string nome, string senha)
        {
            string selectQuery = "select * from usuario where nome = '" + nome + "' and senha = '" + senha + "'";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //open connection
                    connection.Open();

                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    command.Connection = connection;
                    command.CommandText = selectQuery;
                    var result = command.ExecuteReader();
                    //check if account exists
                    var exists = result.HasRows;
                    return true;
                }
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
}
using PW_7_8.MyEntity;
using MySql.Data.MySqlClient;
using System;

namespace PW_7_8.Database
{
    public class Datebase
    {
        private static Datebase _instance = null;
        private static MySqlConnection _connection = null;

        private readonly string server = "localhost";
        private readonly string user = "root";
        private readonly string password = "test12345";
        private readonly string database = "phone";

        private Datebase()
        { 

            try
            {
                _connection = new MySqlConnection($"server={server};user={user};password={password};database={database};");
                _connection.Open();
            }
            catch
            {
                Console.WriteLine("Ошибка при подключение к БД!");
            }
            finally
            {
                _connection.Close();
            }

            LoadingTable();
        }


        public static Datebase GetInstance()
        {
            if (_instance == null)
                _instance = new Datebase();
            return _instance;
        }

        public static MySqlConnection GetConnection() => _connection;

        private void LoadingTable()
        {
            new DirectorDAO();
            new CompanyDAO();
            new PhoneDAO();
            new NetworkDAO();
            User.CreateTable();
            Role.CreateTable();
        }


    }

}


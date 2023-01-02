using PW_7_8.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace PW_7_8.MyEntity
{
    public class User
    {
        private int Id;
        private string Login;
        private string Password;
        private Role _role;

        public User() { }
        public User(int id, string login, string password, string role)
        {
            Id = id;
            Login = login;
            Password = password;
            _role = new Role(role, id);
        }

        public string GetRole() => _role.Name;

        public static void CreateTable()
        {
            string sql = "CREATE TABLE IF NOT EXISTS User"
                + "(User_id INT NOT NULL AUTO_INCREMENT, "
                + "Login VARCHAR(50) NOT NULL, "
                + "Password VARCHAR(50) NOT NULL, "
                + "PRIMARY KEY(User_id)) "
                + "COLLATE='utf8_general_ci' ENGINE=InnoDB;";

            using (MySqlConnection connection = Datebase.GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class Role
    {
        public string Name;
        public int UserId;

        public Role(string name, int userid)
        {
            Name = name;
            UserId = userid;
        }

        public static void CreateTable()
        {
            string sql = "CREATE TABLE IF NOT EXISTS Role"
                + "(Role_id INT NOT NULL AUTO_INCREMENT,"
                + "Role VARCHAR(30) NOT NULL, "
                + "User_id INT NOT NULL, "
                + "PRIMARY KEY(Role_id), "
                + "CONSTRAINT fk_Role_User "
                + "FOREIGN KEY (User_id) REFERENCES User (User_id) "
                + "ON DELETE CASCADE "
                + "ON UPDATE CASCADE) "
                + "COLLATE='utf8_general_ci' ENGINE=InnoDB;";

            using (MySqlConnection connection = Datebase.GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class Auth
    {
        public static void Register()
        {
            Console.Clear();
            Console.Write("Введите id: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Введите логин: ");
            string login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            Console.Write("Выберите роль(user - 1; admin - 2): ");
            string role = Console.ReadLine();

            if (role == "2")
                role = "Admin";
            else
                role = "User";

            string sql = "INSERT INTO User (User_id, Login, Password) " +
                "VALUES (@user_id, @login, @password)";

            using (MySqlConnection connection = Datebase.GetConnection())
            {
                connection.Open();

                try
                {
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@user_id", id);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);

                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("\n[Ошибка!]");
                    Console.WriteLine(ex.Message);
                }
            }


            sql = "INSERT INTO Role (Role_id, Role, User_id) " +
                "VALUES (@role_id, @role, @user_id)";

            using (MySqlConnection connection = Datebase.GetConnection())
            {
                connection.Open();

                try
                {
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@role_id", id);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@user_id", id);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\n[Ошибка!]");
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Регистрация прошла успешна!");
            Console.ReadKey();
        }
        public static User Entry()
        {
            Console.Clear();
            Console.WriteLine("[Вход]\n");

            Console.Write("Введите логин: ");
            string Login = Console.ReadLine();

            Console.Write("Введите пароль: ");
            string Password = Console.ReadLine();

            //Запрос на получения данных таблицы "Company"
            string sql = "SELECT * FROM User, Role WHERE (User.User_id = Role.User_id)";

            using (MySqlConnection connection = Datebase.GetConnection())
            {
                connection.Open();

                MySqlCommand cmd = new MySqlCommand(sql, connection);
                MySqlDataReader reader = cmd.ExecuteReader();


                if (reader.HasRows) // Если есть данные
                {

                    while (reader.Read()) // Построчно считываем данные
                    {
                        int id = reader.GetInt32(0);
                        string login = reader.GetString(1);
                        string password = reader.GetString(2);
                        string role = reader.GetString(4);

                        if (Login == login && Password == password)
                        {
                            Console.WriteLine("Выполнен вход!");
                            Console.ReadKey();
                            return new User(id, login, password, role);
                        }
                    }
                }

                Console.WriteLine("Логин или пароль не правильный!");
                Console.ReadKey();
                return null;
            }
        }
    }

}

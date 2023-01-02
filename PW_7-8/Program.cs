using PW_7_8.Database;
using PW_7_8.MyEntity;
using System;

namespace PW_7_8
{   
    class Program
    {
        static void Main(string[] args)
        {
            string select;
            Datebase.GetInstance();
            User user = null;
            Entity entity = new Entity();
            EntityFactory factory = new EntityFactory();


            do
            {
                Console.WriteLine("[Меню]");
                Console.WriteLine("1) Регистрация");
                Console.WriteLine("2) Вход");
                Console.Write("Выберит номер: ");
                select = Console.ReadLine();

                switch (select)
                {
                    case "1":
                        Auth.Register();
                        break;

                    case "2":
                        user = Auth.Entry();
                        break;

                    default:
                        Console.WriteLine($"Номера({select}) не существует!");
                        break;
                }
                Console.Clear();
            } while (user == null);

            do
            {

                Console.WriteLine("[Программа по сохранению и управлению данными мобильных телефонов]\n");

                Console.WriteLine("1) Директора");
                Console.WriteLine("2) Компании");
                Console.WriteLine("3) Телефоны");
                Console.WriteLine("4) Сети");

                if (entity.CheckObserver(factory))
                    Console.WriteLine("5) Включить(+) оповещения");
                else
                    Console.WriteLine("5) Выключить(-) оповещения");

                Console.WriteLine("0) Выйти");
                Console.Write("Выберит номер: ");
                select = Console.ReadLine();

                entity.SetEntityDAO(factory.CreateEntity(select));

                if (entity.GetEntityDAO() != null)
                    MenuTable(entity, user);

                // Добавление/Удаление подписки
                else if (select == "5")
                    if (entity.CheckObserver(factory))
                        entity.AddObserver(factory);
                    else
                        entity.RemoveObserver(factory);

                Console.Clear();

            } while (select != "0");
        }

        public static void MenuTable(Entity entity, User user)
        {
            string select;
            string table = entity.GetEntityDAO().GetType().Name.Replace("DAO", "");
            ProxyEntityDAO edao = new ProxyEntityDAO(entity.GetEntityDAO(), user);

            do
            {
                Console.Clear();
                Console.WriteLine("[Программа по сохранению и управлению данными мобильных телефонов]\n");

                Console.WriteLine($"[{table}]");
                Console.WriteLine("1) Добавить");
                Console.WriteLine("2) Изменить(id)");
                Console.WriteLine("3) Удалить(id)");
                Console.WriteLine("4) Найти(id)");
                Console.WriteLine("5) Получить список");
                Console.WriteLine("0) Вернуться");
                Console.Write("Выберит номер: ");
                select = Console.ReadLine();

                Console.Clear();


                switch (select)
                {
                    case "1":
                        edao.Add();
                        entity.Operations("Add");
                        break;

                    case "2":
                        edao.Change();
                        entity.Operations("Change");
                        break;

                    case "3":
                        edao.Remove();
                        entity.Operations("Remove");
                        break;

                    case "4":
                        edao.Find();
                        break;

                    case "5":
                        edao.GetList();
                        break;

                    case "0":
                        break;

                    default:
                        Console.WriteLine($"Номера({select}) не существует!");
                        break;
                }

                Console.ReadKey();
            } while (select != "0");


        }

    }

}


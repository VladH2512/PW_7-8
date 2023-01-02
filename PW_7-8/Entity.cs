using PW_7_8.Database;
using PW_7_8.MyEntity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace PW_7_8
{
    public struct EntityList
    {
        public Dictionary<int, Director> directors;
        public Dictionary<int, Company> companies;
        public Dictionary<int, Phone> phones;
        public Dictionary<int, Network> networks;
    }

    public class Entity : IObservable
    {
        protected static EntityList list; // Список данных таблиц из БД
        private static List<IObserver> _observers = new List<IObserver>();
        private EntityDAO _edao;
        public string Operation;

        public void AddObserver(IObserver o)
        {
            _observers.Add(o);
        }
        public void RemoveObserver(IObserver o)
        {
            _observers.Remove(o);
        }
        public void NotifyObservers()
        {
            foreach (IObserver o in _observers)
                o.Update(this);
        }
        public void Operations(string operation)
        {
            Operation = operation;
            NotifyObservers();
        }

        public EntityDAO SetEntityDAO(EntityDAO edao) => _edao = edao;
        public EntityDAO GetEntityDAO() { return _edao; }

        // Проверка списка entity на пустоту в DAO
        protected bool IsEmptyList<N,T>(Dictionary<N, T> list)
        {
            if (list == null)
                return true;

            if (list.Count == 0)
                return true;

            return false;
        }
        // Лог возможных Ошибок
        protected void LogException(ExceptionDispatchInfo edi)
        {
            try
            {
                edi.Throw();
            }
            catch (FormatException ex)
            {
                Console.WriteLine("\n[Неккоректный ввод!]");
                Console.WriteLine(ex.Message);
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("\n[Ошибка в базе данных!]");
                Console.WriteLine(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine("\n[Ошибка в вводе Id!]");
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n[Ошибка!]");
                Console.WriteLine(ex.Message);
            }
        }
        // Проверка наличия подписки
        public bool CheckObserver(IObserver o)
        {
            if (_observers.Contains(o))
                return false;
            return true;
        }

    }
}

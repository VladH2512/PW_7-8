using PW_7_8.Database;
using System;

namespace PW_7_8
{

    public class EntityFactory : IObserver
    {
        public EntityDAO CreateEntity(string select)
        {
            return select switch
            {
                "1" => new DirectorDAO(),
                "2" => new CompanyDAO(),
                "3" => new PhoneDAO(),
                "4" => new NetworkDAO(),
                _ => null,
            };
        }

        public void Update(Entity observable)
        {
            Console.WriteLine($"\nВыполнена операция {observable.Operation} для Entity: {observable.GetType().Name.Replace("DAO", "")}");
        }
    }

}

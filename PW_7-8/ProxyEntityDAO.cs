using PW_7_8.Database;
using PW_7_8.MyEntity;
using System;

namespace PW_7_8
{
    public class ProxyEntityDAO : EntityDAO
    {
        private EntityDAO _edao;
        private User _user;

        public ProxyEntityDAO(EntityDAO edao, User user)
        {
            _edao = edao;
            _user = user;
        }

        public override void Add()
        {
            if (_user.GetRole() == "User" || _user.GetRole() == "Admin")
            {
                _edao.Add();
            }
            else
                Console.WriteLine("У вас нету прав доступа к этой команде!");
        }

        public override void Change()
        {
            if (_user.GetRole() == "Admin")
            {
                _edao.Change();
            }
            else
                Console.WriteLine("У вас нету прав доступа к этой команде!");
        }

        public override void Find()
        {
            if (_user.GetRole() == "User" || _user.GetRole() == "Admin")
            {
                _edao.Find();
            }
            else
                Console.WriteLine("У вас нету прав доступа к этой команде!");
        }

        public override void GetList()
        {
            if (_user.GetRole() == "User" || _user.GetRole() == "Admin")
            {
                _edao.GetList();
            }
            else
                Console.WriteLine("У вас нету прав доступа к этой команде!");
        }

        public override void Remove()
        {
            if (_user.GetRole() == "Admin")
            {
                _edao.Remove();
            }
            else
                Console.WriteLine("У вас нету прав доступа к этой команде!");
        }

        protected sealed override void Update<T>(string operation, T entity, int Id) {}
    }
}

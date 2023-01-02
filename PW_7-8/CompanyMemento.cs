using PW_7_8.MyEntity;
using System;
using System.Collections.Generic;

namespace PW_7_8
{
    public class CompanyMemento
    {
        public int Id;
        public int Director_id;
        public string Name;
        public string Address;
        public DateTime Date_Creation;
        public Director Director;
        public ChangeHistory ChangeHistory;

        public CompanyMemento(int company_id, int director_id, string companyname,
            string address, DateTime date_creation, Director director)
        {
            Id = company_id;
            Director_id = director_id;
            Name = companyname;
            Address = address;
            Date_Creation = date_creation;
            Director = director;
        }
    }

    public class ChangeHistory
    {
        public Stack<CompanyMemento> History { get; private set; }

        public ChangeHistory()
        {
            History = new Stack<CompanyMemento>();
        }
    }
}

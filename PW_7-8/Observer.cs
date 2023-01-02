namespace PW_7_8
{
    public interface IObserver
    {
        void Update(Entity observable);
    }

    public interface IObservable
    {
        void AddObserver(IObserver o);
        void RemoveObserver(IObserver o);
        void NotifyObservers();
        void Operations(string operation);
    }


}

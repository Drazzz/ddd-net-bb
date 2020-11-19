namespace DDDNETBB.Core.Abstractions
{
    public interface IMementoProvider
    {
        object CreateMemento();
        void SetMemento(object memento);        
    }


    public interface IMementoProvider<TMemento> : IMementoProvider
    {
        void SetMemento(TMemento memento);
        new TMemento CreateMemento();
    }
}
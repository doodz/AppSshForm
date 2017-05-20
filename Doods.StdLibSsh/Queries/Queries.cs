namespace Doods.LibSsh.Queries
{
    public abstract class Queries<T> where T:new()
    {
        public T Run() { return new T(); }
         
    }
}

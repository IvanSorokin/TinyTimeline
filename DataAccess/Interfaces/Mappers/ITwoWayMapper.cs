namespace DataAccess.Interfaces.Mappers
{
    public interface ITwoWayMapper<T1, T2>
    {
        T1 Map(T2 obj);
        T2 Map(T1 obj);
    }
}
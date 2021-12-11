namespace Utils.Pool
{
    public interface IPool<T>
    {
        void Prewarm(int num);
        T Get();
        void Return(T member);
    }
}

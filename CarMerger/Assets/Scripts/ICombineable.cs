namespace CarMerger
{
    public interface ICombineable<T>
    {
        void Combine(T other);
    }
}
namespace CarMerger
{
    public interface ICombineable<T>
    {
        bool Combine(T other);
    }
}
namespace GameStore.BLL.Filtering.Factory
{
    interface IFilter<T>
    {
        T Execute(T input);
    }
}

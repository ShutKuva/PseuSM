namespace BLL.Abstractions.Tools
{
    public interface IParamsResolver<T, TParams>
    {
        Task<T> CreateParamsAsync(TParams parameters);
    }
}

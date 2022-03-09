namespace Benchmarking.ApiDefinitions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Refit;
    using RestEase;

    public interface IRefitCatsApi
    {
        [Refit.Get("/v1/images/search")]
        Task<ApiResponse<IEnumerable<SearchResult>>> Search([Refit.Query("q")] string breedIdentifier);
    }

    public interface IRestEaseCatsApi
    {
        [RestEase.Get("/v1/images/search")]
        Task<Response<IEnumerable<SearchResult>>> Search([RestEase.Query("q")] string breedIdentifier);
    }
}

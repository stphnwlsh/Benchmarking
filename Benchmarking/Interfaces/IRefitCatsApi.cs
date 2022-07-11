namespace Benchmarking.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Cat;
using Refit;

public interface IRefitCatsApi
{
    [Get("/v1/images/search")]
    Task<ApiResponse<IEnumerable<SearchResult>>> Search([Query("q")] string breedIdentifier);
}

namespace Benchmarking.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;
using Models.Cat;
using RestEase;

public interface IRestEaseCatsApi
{
    [Get("/v1/images/search")]
    Task<Response<IEnumerable<SearchResult>>> Search([Query("q")] string breedIdentifier);
}


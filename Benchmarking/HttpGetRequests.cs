namespace Benchmarking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.Json;
    using System.Threading.Tasks;
    using ApiDefinitions;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using Flurl;
    using Flurl.Http;
    using Refit;

    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob(RuntimeMoniker.Net60)]
    public class HttpGetRequests
    {
        [Params(10)]
        public int N;

        [Benchmark]
        public async Task HttpClientMethod()
        {
            // Setup HttpClient
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://api.thecatapi.com")

            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            for (var x = 0; x < this.N; x++)
            {
                var response = await httpClient.GetAsync("/v1/images/search?q=bengal");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<IEnumerable<SearchResult>>(json);

                    Console.WriteLine($"Result: {result?.First().id} & Response: {json}");
                }
                else
                {
                    throw new InvalidOperationException($"StatusCode: {response.StatusCode} - HttpClient Error");
                }

                x++;
            }
        }

        [Benchmark]
        public async Task RefitClientMethod()
        {
            // Setup Refit Client
            var refitClient = RestService.For<IRefitCatsApi>("https://api.thecatapi.com");

            for (var x = 0; x < this.N; x++)
            {
                var response = await refitClient.Search("bengal");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = JsonSerializer.Serialize(response.Content);

                    Console.WriteLine($"Result: {response.Content?.First().id} & Response: {json}");
                }
                else
                {
                    throw new InvalidOperationException($"StatusCode: {response.StatusCode} - Refit Error");
                }

                x++;
            }
        }

        [Benchmark]
        public async Task DalSoftClientMethod()
        {
            // Setup Dalsoft Client
            dynamic dalSoftClient = new DalSoft.RestClient.RestClient("https://api.thecatapi.com");

            for (var x = 0; x < this.N; x++)
            {
                var response = await dalSoftClient.V1.Images.Search.Query(new { q = "bengal" }).Get();

                if (((HttpResponseMessage)response).StatusCode == HttpStatusCode.OK)
                {
                    var result = (List<SearchResult>)response;

                    Console.WriteLine($"Result: {result.First().id} & Response: {response}");
                }
                else
                {
                    throw new InvalidOperationException($"StatusCode: {((HttpResponseMessage)response).StatusCode} - DalSoft Error");
                }

                x++;
            }
        }

        [Benchmark]
        public async Task FlurlClientMethod()
        {

            for (var x = 0; x < this.N; x++)
            {
                // Setup Flurl Client
                var response = await "https://api.thecatapi.com"
                    .AppendPathSegment("/v1/images/search")
                    .SetQueryParams(new { q = "bengal" })
                    .GetAsync();

                if (response.StatusCode == (int)HttpStatusCode.OK)
                {
                    var json = await response.ResponseMessage.Content.ReadAsStringAsync();
                    var result = JsonSerializer.Deserialize<IEnumerable<SearchResult>>(json);

                    Console.WriteLine($"Result: {result?.First().id} & Response: {json}");
                }
                else
                {
                    throw new InvalidOperationException($"StatusCode: {response.StatusCode} - Flurl Error");
                }

                x++;
            }
        }

        [Benchmark]
        public async Task RestEaseClientMethod()
        {
            // Setup Refit Client
            var restEaseClient = RestEase.RestClient.For<IRestEaseCatsApi>("https://api.thecatapi.com");

            for (var x = 0; x < this.N; x++)
            {
                var response = await restEaseClient.Search("bengal");

                if (response.ResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.GetContent();

                    Console.WriteLine($"Result: {result?.First().id} & Response: {response.StringContent}");
                }
                else
                {
                    throw new InvalidOperationException($"StatusCode: {response.ResponseMessage.StatusCode} - Refit Error");
                }

                x++;
            }
        }
    }
}

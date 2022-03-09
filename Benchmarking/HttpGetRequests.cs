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
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Jobs;
    using Benchmarking.ApiDefinitions;
    using DalSoft.RestClient;
    using Refit;

    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [SimpleJob(RuntimeMoniker.Net60)]
    public class HttpGetRequests
    {
        [Params(10, 50)]
        public int N;

        [Benchmark]
        public async Task HttpClientMethod()
        {
            // Setup HttpClient
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://api.thecatapi.com")

            };
            httpClient.DefaultRequestHeaders.Add("x-api-key", "b95bfb30-55bc-4327-bb8b-35d740f70051");
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
                    throw new InvalidOperationException($"StatusCode: {response.StatusCode} - Error");
                }

                x++;
            }
        }

        [Benchmark]
        public async Task RefitClientMethod()
        {
            // Setup Refit Client
            var refitClient = RestService.For<ITheCatsAPI>("https://api.thecatapi.com");

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
                    throw new InvalidOperationException($"StatusCode: {response.StatusCode} - Error");
                }

                x++;
            }
        }

        [Benchmark]
        public async Task DalSoftClientMethod()
        {
            // Setup Dalsoft Client
            dynamic dalSoftClient = new RestClient("https://api.thecatapi.com");

            for (var x = 0; x < this.N; x++)
            {
                var response = await dalSoftClient.V1.Images.Search.Query(new { q = "bengal" }).Get();
                var status = (HttpResponseMessage)response;

                if (((HttpResponseMessage)response).StatusCode == HttpStatusCode.OK)
                {
                    var result = (List<SearchResult>) response;

                    Console.WriteLine($"Result: {result.First().id} & Response: {response}");
                }
                else
                {
                    throw new InvalidOperationException($"StatusCode: {response.StatusCode} - Error");
                }

                x++;
            }
        }
    }
}

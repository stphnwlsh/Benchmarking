namespace Benchmarking
{
    using System;
    using System.Collections.Generic;
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
        [Params(10)]
        public int N;

        private HttpClient httpClient;
        private ITheCatsAPI refitClient;
        private dynamic dalSoftClient;

        [GlobalSetup]
        public void GlobalSetup()
        {
            Console.WriteLine("Start GlobalSetup");

            // Setup HttpClient
            this.httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://api.thecatapi.com")

            };
            this.httpClient.DefaultRequestHeaders.Add("x-api-key", "b95bfb30-55bc-4327-bb8b-35d740f70051");
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Setup Refit Client
            this.refitClient = RestService.For<ITheCatsAPI>("https://api.thecatapi.com");

            // Setup Dalsoft Client
            this.dalSoftClient = new RestClient("https://api.thecatapi.com");

            Console.WriteLine("Finish GlobalSetup");
        }

        #region For

        [Benchmark]
        public async Task HttpClientMethod()
        {
            for (var x = 0; x < this.N; x++)
            {
                var response = await this.httpClient.GetAsync("/v1/images/search?q=bengal");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine(JsonSerializer.Deserialize<IEnumerable<SearchResult>>(await response.Content.ReadAsStringAsync()).ToString());
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
            for (var x = 0; x < this.N; x++)
            {
                var response = await this.refitClient.Search("bengal");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine(response.Content.ToString());
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
            for (var x = 0; x < this.N; x++)
            {
                var response = await this.dalSoftClient.V1.Images.Search.Query(new { q = "bengal" }).Get();

                if (response.HttpResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    Console.WriteLine(response.Content.ToString());
                }
                else
                {
                    throw new InvalidOperationException($"StatusCode: {response.StatusCode} - Error");
                }

                x++;
            }
        }

        #endregion For
    }
}

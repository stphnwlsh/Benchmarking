namespace Benchmarking
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.Json;
    using System.Threading.Tasks;
    using ApiDefinitions;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Diagnosers;
    using Flurl;
    using Flurl.Http;
    using Refit;

    [RankColumn]
    [MemoryDiagnoser]
    [MarkdownExporterAttribute.Default]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class IteratingHttpClientsGet : IDisposable
    {
        [Params(50)]
        public int N;

        private HttpClient httpClient;
        private IRefitCatsApi refitClient;
        private Url flurlClient;
        private IRestEaseCatsApi restEaseClient;
        private dynamic dalSoftClient;


        [GlobalSetup]
        public void SetupAsync()
        {
            // Setup Http Client
            this.httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://api.thecatapi.com")
            };
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Setup Refit Client
            this.refitClient = RestService.For<IRefitCatsApi>("https://api.thecatapi.com");

            // Setup Dalsoft Client
            this.dalSoftClient = new DalSoft.RestClient.RestClient("https://api.thecatapi.com");

            // Setup Flurl Client
            this.flurlClient = "https://api.thecatapi.com"
                .AppendPathSegment("/v1/images/search")
                .SetQueryParams(new { q = "bengal" });

            // Setup Refit Client
            this.restEaseClient = RestEase.RestClient.For<IRestEaseCatsApi>("https://api.thecatapi.com");
        }

        [Benchmark]
        public async Task HttpClientMethod()
        {
            for (var x = 0; x < this.N; x++)
            {
                var response = await this.httpClient.GetAsync("/v1/images/search?q=bengal");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    _ = JsonSerializer.Deserialize<IEnumerable<SearchResult>>(json);
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
            for (var x = 0; x < this.N; x++)
            {
                var response = await this.refitClient.Search("bengal");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    _ = JsonSerializer.Serialize(response.Content);
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
            for (var x = 0; x < this.N; x++)
            {
                var response = await this.dalSoftClient.V1.Images.Search.Query(new { q = "bengal" }).Get();

                if (((HttpResponseMessage)response).StatusCode == HttpStatusCode.OK)
                {
                    _ = (List<SearchResult>)response;
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
                var response = await this.flurlClient.GetAsync();

                if (response.StatusCode == (int)HttpStatusCode.OK)
                {
                    var json = await response.ResponseMessage.Content.ReadAsStringAsync();
                    _ = JsonSerializer.Deserialize<IEnumerable<SearchResult>>(json);
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
            for (var x = 0; x < this.N; x++)
            {
                var response = await this.restEaseClient.Search("bengal");

                if (response.ResponseMessage.StatusCode == HttpStatusCode.OK)
                {
                    _ = response.GetContent();
                }
                else
                {
                    throw new InvalidOperationException($"StatusCode: {response.ResponseMessage.StatusCode} - Refit Error");
                }

                x++;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

using FinanceNewTicker.Models;
using Newtonsoft.Json;

namespace FinanceNewTicker.Services
{
    public class NewsService : INewsService
    {
        private readonly IConfiguration _configuration;

        public NewsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public FinanceNews GetFinanceNews(int offset)
        {
            string apiKey = _configuration.GetValue<string>("API_KEY");
            string baseURL = _configuration.GetValue<string>("API_URL");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseURL);

                var parameter = string.Format("?apikey={0}&offset={1}&date={2}&sort={3}", apiKey, offset, "today","desc");

                HttpResponseMessage response = client.GetAsync(parameter).Result;

                if(response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<FinanceNews>(result);

                } else
                {
                    return new FinanceNews()
                    {
                        Data = new List<NewsArticle>(),
                        Pagination = new Pagination()
                    };
                }

            }
        }
    }
}

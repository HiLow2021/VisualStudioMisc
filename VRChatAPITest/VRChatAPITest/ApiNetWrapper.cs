using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VRChatAPI.Extentions.DependencyInjection;
using VRChatAPI.Implementations;

namespace VRChatAPITest
{
    public class ApiNetWrapper
    {
        private readonly string _apiBaseUrl = "https://vrchat.com/api/1/";
        private readonly string _apiKey = "JlE5Jldo5Jibnk5O5hTx6XVqsJu4WJ26";
        private readonly APIHttpClient _client;
        private CancellationTokenSource _tokenSource;
        private CancellationToken _token;

        public ApiNetWrapper()
        {
            var options = Options.Create(new VRCAPIOptions()
            {
                APIEndpointBaseAddress = _apiBaseUrl
            });

            var factory = LoggerFactory.Create(builder => { });
            var logger = new Logger<APIHttpClient>(factory);
            var clientHandler = new HttpClientHandler();

            _client = new APIHttpClient(options, logger, clientHandler);
            _client.AddCookie(new Cookie("apiKey", _apiKey) { Domain = new Uri(_apiBaseUrl).Host });
        }

        public async Task<string> GetWorldAsync(string worldId)
        {
            if (_client == null)
            {
                return null;
            }

            try
            {
                _tokenSource = new CancellationTokenSource();
                _token = _tokenSource.Token;

                var response = await _client.Get($"worlds/{worldId}", _token);
                var result = await response.Content.ReadAsStringAsync();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return e.Message;
            }
            finally
            {
                Console.WriteLine("api finished.");
            }
        }
    }
}

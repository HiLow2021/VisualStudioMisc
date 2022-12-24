using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRChat.API.Api;
using VRChat.API.Client;

namespace VRChatAPITest
{
    public class ApiWrapper
    {
        private static readonly string _errorMessage = "ワールド情報の取得失敗。" +
                                                       "調査結果から VRChat.API はワールド情報取得時、Json変換に失敗している。" +
                                                       "数字のstring型をInt型にパースするときに、Int型の範囲を越えているための模様。";

        private readonly AuthenticationApi _authApi;
        private readonly WorldsApi _worldsApi;

        public ApiWrapper(string username, string password)
        {
            var Config = new Configuration
            {
                Username = username,
                Password = password
            };

            _authApi = new AuthenticationApi(Config);
            _worldsApi = new WorldsApi(Config);
        }

        public async Task<string> GetWorldAsync(string worldId)
        {
            try
            {
                // GetCurrentUserAsync を呼ぶとログインします。
                await _authApi.GetCurrentUserAsync();

                var world = await _worldsApi.GetWorldAsync(worldId);
                if (world == null)
                {
                    return _errorMessage;
                }

                var line = $"{1},{world.AssetUrl},{world.Name},{world.ThumbnailImageUrl}";

                return line;
            }
            catch (ApiException e)
            {
                Console.WriteLine($"Exception when calling API: {e.Message}");
                Console.WriteLine($"Status Code: {e.ErrorCode}");
                Console.WriteLine(e.ToString());

                return e.ToString();
            }
            finally
            {
                Console.WriteLine("api finished.");
            }
        }
    }
}

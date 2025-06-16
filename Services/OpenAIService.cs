using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AutismEducationPlatform.Web.Services
{
    public class OpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAIService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _apiKey = configuration["OpenAI:ApiKey"];
        }

        public async Task<string> GetChatResponseAsync(string userMessage)
        {
            var messages = new List<object>
            {
                new { role = "system", content = "Sen otizmli çocukların ebeveynlerine yardımcı olan bir uzmansın. Sorulara açıklayıcı, empatik ve yönlendirici şekilde cevap ver. Eğitim, gelişim, bakım, uyku düzeni, oyun önerileri, iletişim ve davranışlarla ilgili sorulara detaylı yanıtlar ver." },
                new { role = "user", content = "Çocuğum geceleri çok geç uyuyor, ne yapabilirim?" },
                new { role = "assistant", content = "Gece geç uyuma otizmli çocuklarda yaygın bir durumdur. Öncelikle yatmadan önce ekran süresini sınırlamanızı öneririm. Ayrıca her gün aynı saatte yatma rutini oluşturmak faydalı olur." },
                new { role = "user", content = userMessage }
            };

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                temperature = 0.7,
                max_tokens = 800,
                messages = messages
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return "Üzgünüm, şu anda yanıt veremiyorum.";
            }

            using var doc = JsonDocument.Parse(responseString);
            var reply = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return reply.Trim();
        }
    }
}

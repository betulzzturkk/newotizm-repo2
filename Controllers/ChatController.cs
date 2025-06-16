using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace NewOtizm.Controllers
{
    public class ChatRequest
    {
        public string Message { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly Dictionary<string, string[]> _keywordResponses;

        public ChatController()
        {
            _keywordResponses = new Dictionary<string, string[]>
            {
                ["selamlaşma"] = new[] {
                    "Merhaba! Size nasıl yardımcı olabilirim?",
                    "Hoş geldiniz! Hangi konuda bilgi almak istersiniz?"
                },
                ["otizm"] = new[] {
                    "Otizm, sosyal etkileşim, iletişim ve davranış alanlarında farklılıklar gösteren bir gelişimsel durumdur.",
                    "Otizmli çocuklar benzersizdir ve uygun destekle gelişebilirler."
                }
                // Diğer keyword'ler...
            };
        }

        [HttpPost]
        public IActionResult GetReply([FromBody] ChatRequest request)
        {
            if (string.IsNullOrEmpty(request.Message))
                return new JsonResult(new { reply = "Lütfen bir mesaj yazın." });

            var reply = GetSimpleReply(request.Message);
            return new JsonResult(new { reply = reply });
        }

        private string GetSimpleReply(string message)
        {
            message = message.ToLower()
                             .Replace("ı", "i")
                             .Replace("ğ", "g")
                             .Replace("ü", "u")
                             .Replace("ş", "s")
                             .Replace("ö", "o")
                             .Replace("ç", "c");

            foreach (var keyword in _keywordResponses.Keys)
            {
                if (message.Contains(keyword))
                {
                    var responses = _keywordResponses[keyword];
                    return responses[new Random().Next(responses.Length)];
                }
            }

            return "Üzgünüm, bu konuda size yardımcı olamıyorum.";
        }
    }
}
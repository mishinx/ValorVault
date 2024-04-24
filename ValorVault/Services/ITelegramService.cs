using System;

namespace ValorVault.Services
{
    public interface ITelegramService
    {
        string GetTelegramAccountUrl();
    }

    public class TelegramService : ITelegramService
    {
        private readonly string _telegramAccountUrl = "https://t.me/BT_Git_Support";

        public string GetTelegramAccountUrl()
        {
            return _telegramAccountUrl;
        }
    }
}

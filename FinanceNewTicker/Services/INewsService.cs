using FinanceNewTicker.Models;

namespace FinanceNewTicker.Services
{
    public interface INewsService
    {
        FinanceNews GetFinanceNews(int offset);
    }
}

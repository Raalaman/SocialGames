using System.Collections.Generic;
using System.Threading.Tasks;
using SocialGames.TechnicalTest.Games.Model;

namespace SocialGames.TechnicalTest.Games.Games.Services.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<CharIndex>> GetCharIndexUntilChar(string gameId,char charLimit);
    }
}
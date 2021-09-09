using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialGames.TechnicalTest.Games.Exceptions;
using SocialGames.TechnicalTest.Games.Games.Services.Interfaces;
using SocialGames.TechnicalTest.Games.Model;

namespace SocialGames.TechnicalTest.Games.Games.Services
{
    public class GameService : IGameService
    {
        public async Task<IEnumerable<CharIndex>> GetCharIndexUntilChar(string gameId, char charLimit)
        {
            if (gameId == null)
                throw new GamesException($"Arguments are not valid {gameId}");

            await Task.Delay(500);

            char[] charArray = gameId.ToCharArray();

            int limitPosition = GetPositionCharLimit(charArray, charLimit);

            if (limitPosition == int.MinValue)
                throw new GamesException($"Limit char {charLimit} not found in {gameId}");

            List<CharIndex> result = new List<CharIndex>();
            for (int i = 0; i < limitPosition; i++)
            {
                result.Add(new CharIndex { Char = charArray[i], Index = i });
            }

            return result;
        }

        private static int GetPositionCharLimit(char[] charArray, char charLimit)
        {
            int result = int.MinValue;
            Parallel.For(0, charArray.Length, (i, state) =>
            {
                if (charArray[i] == charLimit)
                {
                    result = i;
                    state.Break();
                }
            });
            return result;
        }
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore;
using Microsoft.AspNetCore.Mvc;
using SocialGames.TechnicalTest.Api.Validators;
using SocialGames.TechnicalTest.Games.Games.Services.Interfaces;

namespace SocialGames.TechnicalTest.Api.Controllers
{
    [ApiController]
    public class GamesControllers : ControllerBase
    {
        private readonly IGameService gameService;

        public GamesControllers(IGameService gameService)
        {
            this.gameService = gameService;

        }

        [Route("api/games/{gameId}/play")]
        [GameIdValidator]
        [HttpGet]
        public async Task<IActionResult> Play([FromRoute] string gameId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.Values.SelectMany(x => x.Errors));
                }
                return Ok(await gameService.GetCharIndexUntilChar(gameId, Constants.LIMIT_CHAR));

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}

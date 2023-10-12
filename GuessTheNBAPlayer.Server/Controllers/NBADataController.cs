using GuessTheNBAPlayer.Model.Entities.Internal;
using GuessTheNBAPlayer.Model.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GuessTheNBAPlayer.Server.Controllers
{
    [ApiController, Route("[controller]/[action]")]
    public class NBADataController : Controller
    {
        private readonly IDataService _dataService;

        public NBADataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public ServerResult<Player> GetRandomPlayer() => _dataService.GetRandomPlayer();

        [HttpPost]
        public ServerResult<List<Team>> GetTeams([FromBody]GetTeamsRequest request) => _dataService.GetTeams(request);
    }
}
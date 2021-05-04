using Microsoft.AspNetCore.Mvc;
using MythicTeamOptimiser.ExternalDataHandling;
using MythicTeamOptimiser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MythicTeamOptimiser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        [HttpGet("{dungeonName}/{characterClass}/{characterSpec}")]
        public async Task<FinalResultModel> Get(string dungeonName, string characterClass, string characterSpec)
        {
            PlayerRequestHandler prh = new PlayerRequestHandler(dungeonName, characterClass, characterSpec);
            FinalResultModel result = await prh.GetBestTeamComp();
            return result;
        }

        // POST api/<TeamsController>
        [HttpPost]
        public async Task<FinalResultModel> Post([FromBody] UserInputModel value)
        {
            PlayerRequestHandler prh = new PlayerRequestHandler(value.DungeonName, value.CharacterClass, value.CharacterSpec);
            FinalResultModel result = await prh.GetBestTeamComp();
            return result;
        }
    }
}

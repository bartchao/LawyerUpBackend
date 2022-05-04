using LawyerUpBackend.Application.Exceptions;
using LawyerUpBackend.Application.Models.Case;
using LawyerUpBackend.Application.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LawyerUpBackend.API.Controllers
{
    public class SearchController : ApiController
    {
        private readonly IPredictionModelService predictionModelService;
        private readonly ICaseService caseService;
        public SearchController(IPredictionModelService predictionModelService, ICaseService caseService)
        {
            this.predictionModelService = predictionModelService;
            this.caseService = caseService;

        }
        // GET api/<SearchController>/5
        [HttpPost]
        public async Task<IActionResult> GetAsync([FromBody] CaseSearchQueryModel query)
        {
            try
            {
                var searchResult = await caseService.SearchCaseListAsync(query);
                return Ok(searchResult);
            }
            catch (SearchNotFoundException e)
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var searchResult = await caseService.GetByIdAsync(id);
            return Ok(searchResult);
        }

    }
}

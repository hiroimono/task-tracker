using Microsoft.AspNetCore.Mvc;
using task_tracker.Interfaces;
using task_tracker.Models;

namespace task_tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuccessesController : Controller
    {
        private readonly ILogger<SuccessesController> _logger;
        private readonly ISuccessesFacade _successesFacade;

        public SuccessesController(
            ILogger<SuccessesController> logger,
            ISuccessesFacade successesFacade)
        {
            _logger = logger;
            _successesFacade = successesFacade;
        }

        [HttpPost]
        [Route("Hub")]
        public string GetAllSuccessesWithHub()
        {
            Success[] successes = _successesFacade.GetAllSuccesses();

            return "Successes sent successfully to all users!";
        }

        // GET api/successes
        [HttpGet]
        public async Task<IActionResult> GetAllSuccesses()
        {
            try
            {
                var successes = await _successesFacade.GetAllSuccessesAsync();

                if (successes == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no Succes!");
                }

                return Ok(successes);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, $"Successes could not get. Database failure!");
            }
        }

        // GET api/successes/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSuccessById(int id)
        {
            try
            {
                var success = await _successesFacade.GetSuccessByIdAsync(id);

                if (success == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Success with id:{id} could not found!");
                }

                return Ok(success);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, $"Success with id:{id} could not found!");
            }
        }

        // PUT api/successes/:taskId/:userId { Success }
        [HttpPut("{taskId}/{userId}")]
        public async Task<IActionResult> AddSuccess(int taskId, int userId)
        {

            try
            {
                var newSuccess = await _successesFacade.AddSuccessAsync(taskId, userId);
                return CreatedAtAction(nameof(AddSuccess), newSuccess);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, $"Success could not be added!");
            }
        }

        // PUT api/successes { Success }
        [HttpPut]
        public async Task<IActionResult> EditSuccess(Success success)
        {
            try
            {
                var editedSuccess = await _successesFacade.EditSuccessAsync(success);
                return Ok(editedSuccess);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, $"Success could not be edited!");
            }
        }

        // DELETE api/successes { Success }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuccessById(int id)
        {
            try
            {
                var isDeleted = await _successesFacade.DeleteSuccessAsync(id);
                return Ok(isDeleted);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, $"Success could not be deleted!");
            }
        }
    }
}

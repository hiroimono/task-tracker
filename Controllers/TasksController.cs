using Microsoft.AspNetCore.Mvc;
using task_tracker.Interfaces;
using Task = task_tracker.Models.Task;

namespace task_tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITasksFacade _tasksfacade;

        public TasksController(ILogger<TasksController> logger, ITasksFacade tasksFacade)
        {
            _logger = logger;
            _tasksfacade = tasksFacade;
        }

        #region GET Requests
        // GET api/tasks
        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var tasks = await _tasksfacade.GetAllTasksAsync();

                if (tasks == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no Task!");
                }

                return Ok(tasks);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, $"Tasks could not get. Database failure!");
            }
        }

        // GET api/tasks/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            try
            {
                var task = await _tasksfacade.GetTaskByIdAsync(id);

                if (task == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Task with id:{id} could not found!");
                }

                return Ok(task);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, $"Task with id:{id} could not found!");
            }
        }

        // GET api/tasks/:id/duration
        [HttpGet("{id}/duration")]
        public async Task<IActionResult> GetTaskDuration(int id)
        {
            try
            {
                var task = await _tasksfacade.GetTaskByIdAsync(id);

                if (task == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Task with id:{id} could not found!");
                }

                return Ok(task.Duration);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, $"Task with id:{id} could not found!");
            }
        }

        // GET api/tasks/:id/is-active
        [HttpGet("{id}/is-active")]
        public async Task<IActionResult> IsTaskActive(int id)
        {
            try
            {
                var task = await _tasksfacade.GetTaskByIdAsync(id);

                if (task == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Task with id:{id} could not found!");
                }

                return Ok(task.IsActiv);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, $"Task with id:{id} could not found!");
            }
        }

        // GET api/tasks/:id/is-break
        [HttpGet("{id}/is-break")]
        public async Task<IActionResult> IsTaskBreak(int id)
        {
            try
            {
                var task = await _tasksfacade.GetTaskByIdAsync(id);

                if (task == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"Task with id:{id} could not found!");
                }

                return Ok(task.IsBreak);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, $"Task with id:{id} could not found!");
            }
        }
        #endregion

        #region POST Requests
        // POST api/tasks { Task }
        [HttpPost]
        public async Task<IActionResult> AddTask(Task task)
        {

            try
            {
                var newTask = await _tasksfacade.AddTaskAsync(task);
                return CreatedAtAction(nameof(AddTask), new { id = task.Id }, task);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, $"Task could not be added!");
            }
        }
        #endregion

        #region PUT Requests
        // PUT api/tasks { Task }
        [HttpPut]
        public async Task<IActionResult> EditTask(Task task)
        {
            try
            {
                var editedTask = await _tasksfacade.EditTaskAsync(task);
                return Ok(editedTask);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, $"Task could not be edited!");
            }
        }
        #endregion

        #region DELETE Requests
        // DELETE api/tasks { Task }
        [HttpDelete("{id}")]
        public IActionResult DeleteTaskById(int id)
        {
            try
            {
                var isDeleted = _tasksfacade.DeleteTask(id);
                return Ok(isDeleted);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, $"Task could not be deleted!");
            }
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using task_tracker.Interfaces;
using task_tracker.Models;

namespace task_tracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersFacade _usersFacade;

        public UsersController(
            ILogger<UsersController> logger,
            IUsersFacade usersFacade)
        {
            _logger = logger;
            _usersFacade = usersFacade;
        }

        #region POST With Hub Requests
        [HttpPost]
        [Route("Hub")]
        public string GetAllUsersWithHub()
        {
            User[] users = _usersFacade.GetAllUsers();

            return "Users sent successfully to all users!";
        }
        #endregion

        #region GET Requests

        // GET api/users
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _usersFacade.GetAllUsersAsync();

                if (users == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"There is no User!");
                }

                return Ok(users);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, $"Users could not get. Database failure!");
            }
        }

        // GET api/users/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _usersFacade.GetUserByIdAsync(id);

                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"User with id:{id} could not found!");
                }

                return Ok(user);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, $"User with id:{id} could not found!");
            }
        }

        // GET api/users/:id/avatar
        [HttpGet("{id}/avatar")]
        public async Task<IActionResult> GetUserAvatar(int id)
        {
            try
            {
                var user = await _usersFacade.GetUserByIdAsync(id);

                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"User Avatar with id:{id} could not found!");
                }

                return Ok(user.Avatar);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, $"User Avatar with id:{id} could not found!");
            }
        }

        // GET api/users/:id/is-admin
        [HttpGet("{id}/is-admin")]
        public async Task<IActionResult> IsUserAdmin(int id)
        {
            try
            {
                var user = await _usersFacade.GetUserByIdAsync(id);

                if (user == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"User Role with id:{id} could not found!");
                }

                return Ok(user.IsAdmin);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status404NotFound, $"User Role with id:{id} could not found!");
            }
        }
        #endregion

        #region POST Requests
        // POST api/users { User }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {

            try
            {
                var newUser = await _usersFacade.AddUserAsync(user);
                return CreatedAtAction(nameof(AddUser), new { id = user.Id }, user);
            }
            catch (Exception Ex)
            {
                _logger.LogError("ERROR: ", Ex.Message);

                if (Ex.InnerException!.Message != null)
                {
                    _logger.LogError("INNER EXCEPTION: ", Ex.InnerException.Message);
                }

                return StatusCode(StatusCodes.Status400BadRequest, $"User could not be added!");
            }
        }
        #endregion

        #region PUT Requests
        // PUT api/users { User }
        [HttpPut]
        public async Task<IActionResult> EditUser(User user)
        {
            try
            {
                var editedUser = await _usersFacade.EditUserAsync(user);
                return Ok(editedUser);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, $"User could not be edited!");
            }
        }
        #endregion

        #region DELETE Requests
        // DELETE api/users { User }
        [HttpDelete("{id}")]
        public IActionResult DeleteUserById(int id)
        {
            try
            {
                var isDeleted = _usersFacade.DeleteUser(id);
                return Ok(isDeleted);
            }
            catch (Exception Ex)
            {
                _logger.LogError(Ex.Message);
                return StatusCode(StatusCodes.Status400BadRequest, $"User could not be deleted!");
            }
        }
        #endregion
    }
}

using App.Domain.Entities;
using App.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;

namespace aspnet_project.Controllers
{
    [Produces("application/json")]
    [Route("users")]

    public class UsersController : Controller
    {
        private IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("create")]
        public JsonResult AddUser([FromBody] Users users)
        {
            try
            {
                _usersService.AddUser(users);

                return Json(new { message = "User added successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { error = "An error occurred while adding the user", message = ex.Message });
            }
        }

        [HttpPut("update")]
        public JsonResult UpdateUser([FromBody] Users users)
        {
            try
            {
                _usersService.UpdateUser(users);

                return Json(new { message = "User updated successfully" });

            }
            catch (Exception ex) 
            {
                return Json(new { error = "An error ocurred while editing the user", message = ex.Message });
            }
        }

        [HttpDelete("delete")]
        public JsonResult DeleteUser([FromHeader] int id) {
            try
            {
                _usersService.DeleteUser(id);

                return Json(new { message = "User deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { error = "An error ocurred while deleting the user", message = ex.Message });
            }
        }

        [HttpGet("getById")]
        public JsonResult GetUserById([FromHeader] int id)
        {
            try
            {
                return Json(_usersService.GetUserById(id));
            }
            catch
            {
                return Json(new { error = "An error ocurred while getting information from this user" });
            }
        }

        [HttpGet("getAllUsers")]
        public JsonResult GetAllUsers()
        {
            try
            {
                return Json(_usersService.GetAllUsers());
            }
            catch
            {
                return Json(new { error = "An error ocurred while getting the users list" });
            }
        }


    }
}

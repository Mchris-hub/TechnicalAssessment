using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Technical_assessment.Data;
using Technical_assessment.models;

namespace Technical_assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserAPIDbcontext dbContext;
        public UserController(UserAPIDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get all users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            return Ok( await dbContext.user.ToListAsync());
        }

        //Get a single user 
        /* uses the unique ApiKey to found a user
         */
        [HttpGet]
        [Route("OneUser/{Api_Key:guid}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid Api_Key)
        {
            var user = await dbContext.user.FindAsync(Api_Key);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        //Add a user
        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserRequest addUserRequest)
        {
            Guid UserAuth = Guid.NewGuid();
            var user = new User()
            {
                UserAuth = UserAuth,
                UserId = addUserRequest.UserId,
                Username = addUserRequest.Username,
                Email = addUserRequest.Email,
                Password = addUserRequest.Password
            };
               await dbContext.user.AddAsync(user);
               await dbContext.SaveChangesAsync();

            return Ok(user);
        }

        //Update a user's info
        /* uses the unique ApiKey to found the user and update that specific user info
         */
        [HttpPut]
        [Route("{Api_Keyy:guid}")]
        public async Task<IActionResult> UpDatetask([FromRoute] Guid Api_Key, UpdateUserRequest updateUserRequest)
        {
            var user = await dbContext.user.FindAsync(Api_Key);

            if(user != null)
            {
                user.UserId = updateUserRequest.UserId;
                user.Username = updateUserRequest.Username;
                user.Email = updateUserRequest.Email;
                user.Password = updateUserRequest.Password;

                await dbContext.SaveChangesAsync();
                return Ok(user);
            }

            return NotFound();
        }

        //Delete a task
        /* delete a user based on the unique ApiKey given
         */
        [HttpDelete]
        [Route("{ApiKey:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid ApiKey)
        {
            var user = await dbContext.user.FindAsync(ApiKey);

            if (user != null)
            {
                dbContext.Remove(user);
                await dbContext.SaveChangesAsync();
            }
            return NotFound();
        }
    }
}

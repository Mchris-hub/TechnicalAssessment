using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Technical_assessment.Data;
using Technical_assessment.models;

namespace Technical_assessment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly UserAPIDbcontext dbContext;

        public TaskController(UserAPIDbcontext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get all tasks 
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            return Ok(await dbContext.task.ToListAsync());
        }

        //Get expired tasks
        /* use dd/mm/yyyy date format on swagger to enter the date and get the desired result 
         * e.g: 02/05/2024, 24/05/2020, 06/07/1997 etc...
         */
        [HttpGet]
        [Route("expired/{Userdate}")]
        public async Task<IActionResult> GetexpiredTasks([FromRoute] string Userdate)
        {
            
            List<Task_> Savedtasks = new List<Task_>();
            Savedtasks = await dbContext.task.ToListAsync();

            List<Task_> FilteredTasks = new List<Task_>();
            string cleanedDate = Userdate.Replace("%2F", "/");
           DateTime UserTask = DateTime.ParseExact(cleanedDate, "dd/MM/yyyy", null);
           
            for(int i = 0; i < Savedtasks.Count; i++)
            {
                DateTime tempTask = DateTime.ParseExact(Savedtasks[i].DueDate, "dd/MM/yyyy",null) ;
                if (tempTask < UserTask)
                {
                    FilteredTasks.Add(Savedtasks[i]);
                }
            }
            return Ok(FilteredTasks);
        }

        //Get Active tasks
        /* use dd/mm/yyyy date format on swagger to enter the date and get the desired result 
         * e.g: 02/05/2024, 24/05/2020, 06/07/1997 etc...
         */
        [HttpGet]
        [Route("Active/{Userdate}")]
        public async Task<IActionResult> GetActiveTasks([FromRoute] string Userdate)
        {

            List<Task_> Savedtasks = new List<Task_>();
            Savedtasks = await dbContext.task.ToListAsync();

            List<Task_> FilteredTasks = new List<Task_>();
            string cleanedDate = Userdate.Replace("%2F", "/");
            DateTime UserTask = DateTime.ParseExact(cleanedDate, "dd/MM/yyyy", null);

            for (int i = 0; i < Savedtasks.Count; i++)
            {
                DateTime tempTask = DateTime.ParseExact(Savedtasks[i].DueDate, "dd/MM/yyyy", null);
                if (tempTask > UserTask)
                {
                    FilteredTasks.Add(Savedtasks[i]);
                }
            }
            return Ok(FilteredTasks);
        }

        //Get tasks from specific date
        /* use dd/mm/yyyy date format on swagger to enter the date and get the desired result 
         * e.g: 02/05/2024, 24/05/2020, 06/07/1997 etc...
         */
        [HttpGet]
        [Route("specific/{Userdate}")]
        public async Task<IActionResult> GetspecificDateTasks([FromRoute] string Userdate)
        {

            List<Task_> Savedtasks = new List<Task_>();
            Savedtasks = await dbContext.task.ToListAsync();

            List<Task_> FilteredTasks = new List<Task_>();
            string cleanedDate = Userdate.Replace("%2F", "/");
            DateTime UserTask = DateTime.ParseExact(cleanedDate, "dd/MM/yyyy", null);

            for (int i = 0; i < Savedtasks.Count; i++)
            {
                DateTime tempTask = DateTime.ParseExact(Savedtasks[i].DueDate, "dd/MM/yyyy", null);
                if (tempTask == UserTask)
                {
                    FilteredTasks.Add(Savedtasks[i]);
                }
            }
            return Ok(FilteredTasks);
        }

        //Add a task
        /* the format for the duedate is dd/mm/yyyy
         * e.g:02/05/2024, 24/05/2020, 06/07/1997 etc...
         */
        [HttpPost]
        public async Task<IActionResult> addtask(AddTaskRequest addTaskRequest)
        {
            var task = new Task_()
            {
                
                Id = addTaskRequest.Id,
                Title = addTaskRequest.Title,
                Description = addTaskRequest.Description,
                Assignee = addTaskRequest.Assignee,
                DueDate = addTaskRequest.DueDate

            };
            await dbContext.task.AddAsync(task);
            await dbContext.SaveChangesAsync();
            return Ok(task);
        }

        //Update a task
        /* use the task's id to update it         
         */
        [HttpPut]
        [Route("{Id}")]
        public async Task<IActionResult> UpdateTask([FromRoute] string Id, UpdateTaskRequest updateTaskRequest)
        {
            var task = await dbContext.task.FindAsync(Id);

            if (task != null)
            {
                task.Id = updateTaskRequest.Id;
                task.Title = updateTaskRequest.Title;
                task.Description = updateTaskRequest.Description;
                task.Assignee = updateTaskRequest.Assignee;
                task.DueDate = updateTaskRequest.DueDate;

                await dbContext.SaveChangesAsync();
                return Ok(task);
            }

            return NotFound();
        }

        //Delete a task
        /* use the task's id to delete it         
        */
        [HttpDelete]
        [Route("{Id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] string Id)
        {
            var task = await dbContext.task.FindAsync(Id);

            if (task != null)
            {
                dbContext.Remove(task);
                await dbContext.SaveChangesAsync();
            }
            return NotFound();
        }
    }
}

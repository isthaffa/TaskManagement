using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.services;

namespace WebApplication1.Controllers
   
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagement : ControllerBase


    {
       
        
        public readonly TaskManager _tasks;

        public TaskManagement(TaskManager tasks_)
        {
            _tasks = tasks_;

        }

        [HttpPost]
        [Route("CreateTasks")]
        public async Task<IActionResult> CreateTasks(CreateTasksRequest request)
        {
            CreateTasksResponse response = null;
            try
            {
                response = await _tasks.CreateTasks(request);
            }catch(Exception ex)
            {

                response.isSuccess = false;
                response.message = ex.Message;

               

            }

            return Ok(response);

        }

        [HttpGet]
        [Route("Tasks")]
        public async Task<IActionResult> getTasks()
        {
            GetTasksResponse response = null;
            try
            {
                response = await _tasks.getTasks();
            }
            catch (Exception ex)
            {

                response.isSuccess = false;
                response.message = ex.Message;



            }

            return Ok(response);

        }

        [HttpGet]
        [Route("Tasks/{id}")]
        public async Task<IActionResult> getTask(int id)
        {
            GetTaskResponse response = null;
            try
            {
                response = await _tasks.getTask(id);
            }
            catch (Exception ex)
            {

                response.isSuccess = false;
                response.message = ex.Message;



            }

            return Ok(response);

        }

        [HttpPut]
        [Route("updateTask/{id}")]
        public async Task<IActionResult> updateTask(int id, CreateTasksRequest request)
        {
            UpdateTaskResponse response = null;
            try
            {
                response = await _tasks.updateTask(id,request);
            }
            catch (Exception ex)
            {

                response.isSuccess = false;
                response.message = ex.Message;



            }

            return Ok(response);

        }

        [HttpDelete]
        [Route("deleteTask/{id}")]
        public async Task<IActionResult> deleteTask(int id)
        {
            DeleteTaskResponse response = null;
            try
            {
                response = await _tasks.deleteTask(id);
            }
            catch (Exception ex)
            {

                response.isSuccess = false;
                response.message = ex.Message;



            }

            return Ok(response);

        }
    }
}

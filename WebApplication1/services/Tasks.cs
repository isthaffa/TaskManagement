using WebApplication1.Models;
using WebApplication1.repositories;

namespace WebApplication1.services
{
    public class Tasks : TaskManager
    {


        public readonly TaskManagmentRepository taskManagmentRepository;

        public Tasks(TaskManagmentRepository _taskManagementRepo)
        {
            taskManagmentRepository = _taskManagementRepo;
        }
        public async Task<CreateTasksResponse> CreateTasks(CreateTasksRequest request)
        {
            return await taskManagmentRepository.CreateTasks(request);
        }

       public async Task<GetTasksResponse> getTasks()
        {
            return await taskManagmentRepository.getTasks();
        }

        public async Task<GetTaskResponse> getTask(int id)
        {
            return await taskManagmentRepository.getTask(id);

        }

        public async Task<UpdateTaskResponse> updateTask(int id, CreateTasksRequest request)
        {
            return await taskManagmentRepository.updateTask(id,request);
        }

       public async Task<DeleteTaskResponse> deleteTask(int id)
        {
            return await taskManagmentRepository.deleteTask(id);
        }
    }
}

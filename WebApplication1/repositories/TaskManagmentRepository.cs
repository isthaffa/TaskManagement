using WebApplication1.Models;

namespace WebApplication1.repositories
{
    public interface TaskManagmentRepository
    {
        public Task<CreateTasksResponse> CreateTasks(CreateTasksRequest request);
        public Task<GetTasksResponse> getTasks( );
        public Task<GetTaskResponse> getTask(int id);
        public Task<UpdateTaskResponse> updateTask(int id, CreateTasksRequest request);
        public Task<DeleteTaskResponse> deleteTask(int id);




    }
}

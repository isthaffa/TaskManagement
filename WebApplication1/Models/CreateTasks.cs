using System.Collections.Generic;

namespace WebApplication1.Models
{

    public class TaskData
    {
        public int id { get; set; }
        public string taskName { get; set; }
        public string taskStatus { get; set; }
        public int active { get; set; }

        public int priority { get; set; }

    }
    public class CreateTasksRequest
    {

        public string taskName { get; set; }
        public string taskStatus { get; set; }
        public int active { get; set; }

        public int priority { get; set; }

    }

    public class CreateTasksResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }

    }

    public class UpdateTaskResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }

    }

    public class DeleteTaskResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }

    }

    public class GetTasksResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }

        public List<TaskData> data { get; set; }

    }

    public class GetTaskResponse
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }

        public TaskData data { get; set; }

    }
}

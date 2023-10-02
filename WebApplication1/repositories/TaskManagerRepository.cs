using System.Data.SqlClient;
using WebApplication1.Models;
using MySqlConnector;


namespace WebApplication1.repositories
{
    public class TaskManagerRepository : TaskManagmentRepository
    {

        public readonly IConfiguration configuration;
        public readonly MySqlConnection _sqlConnection;

    public TaskManagerRepository(IConfiguration con)
        {
            configuration = con;
            _sqlConnection = new MySqlConnection(configuration["ConnectionStrings:DBSettingConnection"]);
        }

    public async Task<CreateTasksResponse> CreateTasks(CreateTasksRequest request)
        {
            CreateTasksResponse response = new CreateTasksResponse();
            response.isSuccess = true;
            response.message = "Success";

            try
            {
                string SqlQuery = "Insert into tasks (taskName , status,active,priority) values (@taskName,@status,@active,@priority)";
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _sqlConnection))
                {
                    _sqlConnection.Open();
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("taskName", request.taskName);
                    sqlCommand.Parameters.AddWithValue("status", request.taskStatus);

                    sqlCommand.Parameters.AddWithValue("active", request.active);
                    sqlCommand.Parameters.AddWithValue("priority", request.priority);


                   int status = await sqlCommand.ExecuteNonQueryAsync();

                  
                    if (status < 0 )
                    {
                        response.isSuccess = false;
                        response.message = "failed";
                    }
                }
            }
            catch(Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;

            }
            finally
            {
                _sqlConnection.Close();
            }

            return response;

        }

        public async Task<GetTasksResponse> getTasks()
        {
            GetTasksResponse response = new GetTasksResponse();
            response.isSuccess = true;
            response.message = "Success";
            response.data = new List<TaskData>(); 


            try
            {
                string SqlQuery = "select * from tasks ";
                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _sqlConnection))
                {
                    _sqlConnection.Open();
                    sqlCommand.CommandTimeout = 180;
                   


                    var reader = await sqlCommand.ExecuteReaderAsync();


                    while (await reader.ReadAsync())
                    {
                        TaskData task = new TaskData
                        {
                            id = reader.GetInt32("taskId"),
                            taskName = reader.GetString("taskName"),
                            taskStatus = reader.GetString("status"),
                            active = reader.GetInt32("active"),
                            priority = reader.GetInt32("priority")
                        };

                        response.data.Add(task); 
                    }
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;

            }
            finally
            {
                _sqlConnection.Close();
            }

            return response;
        }

        public async Task<GetTaskResponse> getTask(int id)
        {
            GetTaskResponse response = new GetTaskResponse();
            response.isSuccess = true;
            response.message = "Success";

            try
            {
                string SqlQuery = "SELECT * FROM tasks WHERE taskId = @id";  // Parameterized SQL query

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@id", id);  // Add the parameter value

                    _sqlConnection.Open();
                    sqlCommand.CommandTimeout = 180;

                    var reader = await sqlCommand.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        TaskData task = new TaskData
                        {
                            id = reader.GetInt32("taskId"),
                            taskName = reader.GetString("taskName"),
                            taskStatus = reader.GetString("status"),
                            active = reader.GetInt32("active"),
                            priority = reader.GetInt32("priority")
                        };

                        response.data = task ; 
                    }
                    else
                    {
                        response.isSuccess = false;
                        response.message = "Task not found";
                    }
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return response;
        }

        public async Task<UpdateTaskResponse> updateTask(int id, CreateTasksRequest request)
        {
            UpdateTaskResponse response = new UpdateTaskResponse();
            response.isSuccess = true;
            response.message = "Success";

            try
            {
                string SqlQuery = @"
            UPDATE tasks 
            SET 
                taskName = @taskName,
                status = @taskStatus,
                active = @active,
                priority = @priority
            WHERE taskId = @id";  

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@id", id);
                    sqlCommand.Parameters.AddWithValue("@taskName", request.taskName);
                    sqlCommand.Parameters.AddWithValue("@taskStatus", request.taskStatus);
                    sqlCommand.Parameters.AddWithValue("@active", request.active);
                    sqlCommand.Parameters.AddWithValue("@priority", request.priority);

                    _sqlConnection.Open();
                    sqlCommand.CommandTimeout = 180;

                    int rowsAffected = await sqlCommand.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        response.isSuccess = false;
                        response.message = "No task was updated. Task may not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return response;
        }

        public async Task<DeleteTaskResponse> deleteTask(int id)
        {
            DeleteTaskResponse response = new DeleteTaskResponse();
            response.isSuccess = true;
            response.message = "Success";

            try
            {
                string SqlQuery = "DELETE FROM tasks WHERE taskId = @id";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    _sqlConnection.Open();
                    sqlCommand.CommandTimeout = 180;

                    int rowsAffected = await sqlCommand.ExecuteNonQueryAsync();

                    if (rowsAffected == 0)
                    {
                        response.isSuccess = false;
                        response.message = "No task was deleted. Task may not exist.";
                    }
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.message = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return response;
        }
    }
}

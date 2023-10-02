using MySqlConnector;
using WebApplication1.repositories;
using WebApplication1.services;

var builder = WebApplication.CreateBuilder(args);
{


    builder.Services.AddCors(c => c.AddPolicy("AllowOrigin", option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
    builder.Services.AddTransient<MySqlConnection>(_ =>
        new MySqlConnection(builder.Configuration.GetConnectionString("DBSettingConnection")));
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            builder =>
            {
                builder.WithOrigins("http://localhost:3000")  
                       .AllowAnyHeader()
                       .AllowAnyMethod();
            });
    });

    builder.Services.AddControllers();


    builder.Services.AddScoped<TaskManager, Tasks>();
    builder.Services.AddScoped<TaskManagmentRepository, TaskManagerRepository>();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

// Add services to the container.


var app = builder.Build();
{

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

    }

    app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    app.UseCors("AllowSpecificOrigin");

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();



    app.Run();
}

// Configure the HTTP request pipeline.


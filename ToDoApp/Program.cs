using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<TodoContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("ToDoAPi"))); using (var scope = builder.Services.BuildServiceProvider())
            {
                var dbContext = scope.GetRequiredService<TodoContext>();
                dbContext.Database.EnsureCreated();
            }


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyAllowedOrigins",
                    policy =>
                    {
                        policy
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();


            app.UseCors("MyAllowedOrigins");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
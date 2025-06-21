
using SkillSystem.Api.ActionHelpers;
using SkillSystem.Api.Configurations;
using SkillSystem.Api.Endpoints;

namespace SkillSystem.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<AppExceptionHandler>();

            // Add services to the container.

            builder.ConfigureDatabase();
            builder.ConfigurationJwtAuth();
            builder.ConfigureServices();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            app.UseExceptionHandler();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();           
            app.MapAuthEndpoints();
            app.MapAdminEndpoints();
            app.MapSkillEndpoints();


            app.Run();
        }
    }
}

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

            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            
            builder.ConfigureDatabase();
            builder.ConfigurationJwtAuth();
            builder.ConfigureServices();

            
            builder.Services.AddProblemDetails();
            builder.Services.AddExceptionHandler<AppExceptionHandler>();

            var app = builder.Build();

            
            app.UseExceptionHandler();

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            
            app.UseAuthentication(); 
            app.UseAuthorization();

           
            app.MapControllers();
            app.MapAuthEndpoints();
            app.MapAdminEndpoints();
            app.MapSkillEndpoints();
            app.MapRoleEndpoints();

            app.Run();
        }
    }
}

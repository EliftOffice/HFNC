namespace HFNC_Coaches
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add CORS support
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Register Dapper and Data Access Services
            builder.Services.AddScoped<HFNC_Coaches.Data.IDbConnectionFactory, HFNC_Coaches.Data.DbConnectionFactory>();

            // Register Coaches Module
            builder.Services.AddScoped<HFNC_Coaches.BLL.Coaches.ICoachesBLL, HFNC_Coaches.BLL.Coaches.CoachesBLL>();
            builder.Services.AddScoped<HFNC_Coaches.DAL.Coaches.ICoachesDAL, HFNC_Coaches.DAL.Coaches.CoachesDAL>();

            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // Enable static file serving for wwwroot
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
            });

            // Use CORS
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.MapControllers();

            // Map default document
            app.MapGet("/", async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(Path.Combine(app.Environment.ContentRootPath, "wwwroot", "index.html"));
            });

            //app.MapGet("/templates/index.html", async context =>
            //{
            //    context.Response.ContentType = "text/html";
            //    await context.Response.SendFileAsync(Path.Combine(app.Environment.ContentRootPath, "wwwroot", "templates", "index.html"));
            //});

            app.Run();
        }
    }
}

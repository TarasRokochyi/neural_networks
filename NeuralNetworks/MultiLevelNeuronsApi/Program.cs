
using MultiLevelNeurons;
using MultiLevelNeuronsApi.Services;

namespace MultiLevelNeuronsApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args);

            //// Add services to the container.

            //builder.Services.AddControllersWithViews();
            //builder.Services.AddRazorPages();

            //builder.Services.AddControllers();
            //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            //var app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //app.UseHttpsRedirection();

            //app.UseBlazorFrameworkFiles();
            //app.UseStaticFiles();

            //app.UseAuthorization();

            //app.MapRazorPages();
            //app.MapControllers();
            //app.MapFallbackToFile("index.html");

            //app.Run();




            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddSingleton<IMedicalDatasetService, MedicalDatasetService>();
            builder.Services.AddSingleton<Network, Network>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();


            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();

        }
    }
}

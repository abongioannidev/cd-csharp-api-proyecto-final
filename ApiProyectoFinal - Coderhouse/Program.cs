using ApiProyectoFinal_Coderhouse.Database;
using ApiProyectoFinal_Coderhouse.Mappers;
using ApiProyectoFinal_Coderhouse.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mime;
using static System.Net.Mime.MediaTypeNames;

namespace ApiProyectoFinal_Coderhouse
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

            //inyectar las dependencias
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<ProductoService>();
            builder.Services.AddScoped<ProductoVendidoService>();
            builder.Services.AddScoped<VentaService>();


            builder.Services.AddScoped<UsuarioMapper>();
            builder.Services.AddScoped<ProductoMapper>();
            builder.Services.AddScoped<ProductoVendidoMapper>();
            builder.Services.AddScoped<VentaMapper>();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                    policy.AllowAnyHeader();
                });
            });


            builder.Services.AddExceptionHandler((options) =>
            {
                options.ExceptionHandler = new RequestDelegate(async (context) =>
                {
                    
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    context.Response.ContentType = Application.Json;

                    await context.Response.WriteAsJsonAsync(new { mensaje = "No se pudo procesar la solicitud correctamente", status = HttpStatusCode.Conflict });

                });
            });



            builder.Services.AddDbContext<CoderContext>(options =>
            {
                options.UseSqlServer("Server=.;Database=coderhouse;Trusted_Connection=True;TrustServerCertificate=Yes;");
            }); //agregar

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseExceptionHandler();
            app.MapControllers();

            app.Run();
        }
    }
}

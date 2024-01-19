using System.Reflection;
using DecimimalToRomanConverterAPI.Converters;
using Microsoft.OpenApi.Models;

namespace DecimimalToRomanConverterAPI
{
    public class Program
    {
        public static void Main ( string [] args )
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers ();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer ();
            
            builder.Services.AddSwaggerGen ( c =>
            {
                c.SwaggerDoc ( "v1" , new OpenApiInfo
                {
                    Title = "Roman number converter API" ,
                    Version = "v1" ,
                    Description = "API to convert numberd" ,
                    // url is fake
                    TermsOfService = new Uri ( "https://app.mycompany.com/terms" ) ,
                    Contact = new OpenApiContact
                    {
                        Name = "Daniel Eros" ,
                        Email = "danieleros@gmail.com" ,
                        Url = new Uri ( "https://www.linkedin.com/in/danieleros/" ) ,
                    } ,
                    License = new OpenApiLicense
                    {
                        Name = "Free to use" ,
                    }
                } );

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments ( xmlPath );
            } );

            builder.Services.AddScoped<IRomanNumberConverter , RomanNumberConverter> ();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if ( app.Environment.IsDevelopment () )
            {
                app.UseSwagger ();
                app.UseSwaggerUI ( c =>
                {
                    c.SwaggerEndpoint ( "/swagger/v1/swagger.json" , "Roman number converter API V1" );
                } );
            }

            app.UseHttpsRedirection ();

            app.UseAuthorization ();


            app.MapControllers ();

            app.Run ();
        }
    }
}
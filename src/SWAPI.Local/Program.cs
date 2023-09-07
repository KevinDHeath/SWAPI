using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Models;

namespace SWAPI.Local;

/// <summary>ASP.NET Core Web API for managing SWAPI resources.</summary>
[EditorBrowsable( EditorBrowsableState.Never )]
public class Program
{
	/// <summary>Main entry point.</summary>
	/// <param name="args">Collection of arguments.</param>
	public static void Main( string[] args )
	{
		var builder = WebApplication.CreateBuilder( args );

		// Add services to the container.
		_ = builder.Services.AddSingleton<Stores.RootStore>();
		_ = builder.Services.AddSingleton<Stores.FilmStore>();
		_ = builder.Services.AddSingleton<Stores.PersonStore>();
		_ = builder.Services.AddSingleton<Stores.PlanetStore>();
		_ = builder.Services.AddSingleton<Stores.SpeciesStore>();
		_ = builder.Services.AddSingleton<Stores.StarshipStore>();
		_ = builder.Services.AddSingleton<Stores.VehicleStore>();

		_ = builder.Services.AddControllers();
		_ = builder.Services.AddEndpointsApiExplorer();
		_ = builder.Services.AddSwaggerGen( options =>
		{
			options.SwaggerDoc( "v1", new OpenApiInfo
			{
				Version = "v1",
				Title = "SWAPI Local",
				Description = "An ASP.NET Core Web API for managing SWAPI resources",
			} );

			var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			options.IncludeXmlComments( Path.Combine( AppContext.BaseDirectory, xmlFilename ) );
		} );

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if( app.Environment.IsDevelopment() )
		{
			_ = app.UseSwagger();
			_ = app.UseSwaggerUI();
		}

		_ = app.UseHttpsRedirection();
		_ = app.UseAuthorization();
		_ = app.MapControllers();

		app.Run();
	}
}
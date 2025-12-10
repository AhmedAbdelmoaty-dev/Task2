using Application.Extensions;
using Infrastructure.Extensions;
using Order.API.Middlewares;

{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddInfrastructureServices(builder.Configuration)
        .AddApplicationServices();

    builder.Services.AddControllers();

    builder.Services.AddSwaggerGen();


    var app = builder.Build();

    app.UseMiddleware<ExceptionHandelingMidlleware>();
    if (app.Environment.IsDevelopment())
    {
        app.MapSwagger();
        app.UseSwaggerUI();

    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

  

    app.Run();
}

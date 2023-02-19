using Library.API.Middlewares;
using Library.Data.EF.Context;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

//builder.Services.AddHttpLogging(opt =>
//{
//    opt.LoggingFields = HttpLoggingFields.RequestQuery | HttpLoggingFields.RequestBody  ;
//});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<LibraryContext>(options =>
    options.UseInMemoryDatabase("LibraryDB"));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




// TODO : Add exception handling middleware
app.UseExceptionHandler(x => x.Run(async context =>
{
    context.Response.StatusCode = 500;
    await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
}));



//app.UseHttpLogging();
app.UseRequestLogger();

app.UseHttpsRedirection();



app.MapControllers();

app.Run();

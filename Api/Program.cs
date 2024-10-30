using API;
using Infra;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(option => option.Filters.Add<NotificationFilter>());
builder.Services.AddControllers(option => option.Filters.Add<AsyncExceptionFilter>());
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddSettings(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddRouting(item => item.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<DbContext2>();
    context.Database.EnsureCreated();
    context.SeedData();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(action => action.AllowAnyOrigin());

app.Run();

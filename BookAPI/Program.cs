using BookCore.Data;
using BookInfrasture.Mappings;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("PersonalDatabase");
builder.Services.AddDbContext<BookContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapperUser));
builder.Services.AddAutoMapper(typeof(AutoMapperBook));
builder.Services.AddAutoMapper(typeof(AutoMapperCategory));
builder.Services.AddAutoMapper(typeof(AutomapperReceiver));
builder.Services.AddAutoMapper(typeof(AutomapperReceiverDetail));
builder.Services.AddAutoMapper(typeof(AutoMapperCart));
builder.Services.AddAutoMapper(typeof(AutoMapperOrder));
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

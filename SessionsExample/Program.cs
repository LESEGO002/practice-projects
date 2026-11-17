using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//registration a in memory that is going to store data until
//session times out
builder.Services.AddDistributedMemoryCache();

//add method that setup how lonng is the session
//timer determines howlong  data must existinside the addDistributed memeoryCache()
builder.Services.AddSession(
    //options method to setup sessions duration
    Options => 
    {//start of options method

        Options.IdleTimeout = TimeSpan.FromMinutes(2);
         
    }//end of options method
    );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//call a method that is going to
//ensure that the session is executed
app.UseSession();

app.MapControllers();

app.Run();

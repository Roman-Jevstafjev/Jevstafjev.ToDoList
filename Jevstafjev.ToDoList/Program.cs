using Jevstafjev.ToDoList.Services;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient("TodoAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri("http://demo2.z-bit.ee/");
    httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, $"Bearer {new AccessTokenService().GetToken()}");
});

builder.Services.AddTransient<AccessTokenService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute("Default", "{controller=Main}/{action=Index}");

app.Run();

using Microsoft.Identity.Web.UI;
using System.Threading.Tasks;

public static class WarmUp
{
    public static void Main()
    {
        Task task = Start(new string[] { "" });
    }


    public Task Start(string[] keyValuePair)
    {
        var builder = WebApplication.CreateBuilder(keyValuePair);

        // Add services to the container.
        builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme);

        builder.Services.AddAuthorization(options =>
        {
        // By default, all incoming requests will be authorized according to the default policy.
        options.FallbackPolicy = options.DefaultPolicy;
        });
        builder.Services.AddRazorPages()
            .AddMicrosoftIdentityUI();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();

        app.Run();

        return Task.CompletedTask;
    }
}
namespace WebMediaController
{
    public class MainPage
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    context.Response.Headers.Add("Content-Type", "text/html");
                    await context.Response.SendFileAsync("Pages/Main.html");
                });

                endpoints.MapPost("/api/values", async context =>
                {
                    var button = context.Request.Form["button"];

                    switch (button)
                    {
                        case "previous-track":
                            MediaController.PreviousTrack();
                            break;
                        case "pause":
                            MediaController.PlayPause();
                            break;
                        case "next-track":
                            MediaController.NextTrack();
                            break;
                        default:
                            break;
                    }

                    context.Response.Redirect("/");
                });
            });
        }

    }
}

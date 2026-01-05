using Microsoft.AspNetCore.HttpsPolicy;
using WebSpark.Bootswatch;
using WebSpark.HttpClientUtility.RequestResult;
using WebSpark.PrismSpark;
using WebSpark.PrismSpark.Highlighting;
using WebSpark.PrismSpark.Demo.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = "PrismSparkDemo.Session";
});

// Configure HTTPS redirection
builder.Services.Configure<HttpsRedirectionOptions>(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 7161; // Match the HTTPS port from launchSettings.json
});

// Use our custom implementation instead of the default one for HTTP requests
builder.Services.AddScoped<IHttpRequestResultService, HttpRequestResultService>();

// Register PrismSpark services
builder.Services.AddSingleton<IHighlighter, HtmlHighlighter>();
builder.Services.AddSingleton<EnhancedHtmlHighlighter>();
builder.Services.AddSingleton<ThemedHtmlHighlighter>();
builder.Services.AddScoped<WebSpark.PrismSpark.Demo.Services.ICodeHighlightingService, WebSpark.PrismSpark.Demo.Services.CodeHighlightingService>();
builder.Services.AddScoped<WebSpark.PrismSpark.Demo.Services.IThemeService, WebSpark.PrismSpark.Demo.Services.ThemeService>();

// Use the extension method to register Bootswatch theme switcher (includes StyleCache)
builder.Services.AddBootswatchThemeSwitcher();
builder.Services.AddLogging();

// Add detailed logging for static files middleware
builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Debug);

// Add HttpContextAccessor service
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Initialize PrismSpark
PrismSpark.Initialize();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure HTTPS redirection with specific port
app.UseHttpsRedirection();
app.UseBootswatchAll();

// Enable session
app.UseSession();

// Initialize default themes for new sessions
app.UseThemeInitialization();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

using WebSpark.Bootswatch;
using WebSpark.HttpClientUtility.RequestResult;
using WebSpark.PrismSpark;
using WebSpark.PrismSpark.Highlighting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Use our custom implementation instead of the default one for HTTP requests
builder.Services.AddScoped<IHttpRequestResultService, HttpRequestResultService>();

// Register PrismSpark services
builder.Services.AddSingleton<IHighlighter, HtmlHighlighter>();
builder.Services.AddSingleton<EnhancedHtmlHighlighter>();
builder.Services.AddSingleton<ThemedHtmlHighlighter>();
builder.Services.AddScoped<WebSpark.PrismSpark.Demo.Services.ICodeHighlightingService, WebSpark.PrismSpark.Demo.Services.CodeHighlightingService>();

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

app.UseHttpsRedirection();
app.UseBootswatchAll();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

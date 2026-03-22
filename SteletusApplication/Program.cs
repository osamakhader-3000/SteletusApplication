using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using SteletusApplicationPresentation.AuthorizationPolicies;
using SteletusApplicationPresentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SteletusApplicationPresentation.Settings.JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddTransient<SteletusApplicationPresentation.Middlewares.ExceptionHandlingMiddleware>();

var authenticationBuilder = builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
});

SteletusApplicationPresentation.Settings.AuthenticationBuilder.ApplySettings(authenticationBuilder, builder.Configuration);


builder.Services.AddAuthorization(options =>
{

    options.AddPolicy("ExternalOnly", policy =>
    {
        policy.AddAuthenticationSchemes("External");
        policy.RequireAuthenticatedUser();
    });

    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

}).AddAuthorization(options =>
{
    options.AddPolicy("VerifiedEmail", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.AddRequirements(new EmailVerifiedRequirement());
    });

    options.AddPolicy("ActiveAccount", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.AddRequirements(new ActiveAccountRequirement());
    });

    options.AddPolicy("AdminLevel", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.AddRequirements(new AdminLevelRequirement());

    });
});




builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IAuthorizationHandler, EmailVerifiedHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ActiveAccountHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, AdminLevelRequirementHandler>();

builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, CustomAuthorizationResultHandler>();

var app = builder.Build();


app.UseRouting();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();


var apiGroup = app.MapGroup("/api");
apiGroup.AddEndpointFilter<SingleFlightSameResponseFilter>();

SteletusApplicationPresentation.APIs.Tasks.Map(apiGroup);


app.Run();

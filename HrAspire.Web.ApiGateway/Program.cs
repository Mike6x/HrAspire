using HrAspire.Business.Common;
using HrAspire.Employees.Data;
using HrAspire.Employees.Data.Models;
using HrAspire.Employees.Web;
using HrAspire.Salaries.Web;
using HrAspire.ServiceDefaults;
using HrAspire.Web.ApiGateway;
using HrAspire.Web.ApiGateway.Endpoints;
using HrAspire.Web.Common;

using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.AddNpgsqlDbContext<EmployeesDbContext>(ResourceNames.EmployeesDb);

builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies(options => options.ApplicationCookie!
        .Configure(cookieOptions =>
        {
            cookieOptions.Cookie.SameSite = SameSiteMode.None;
            cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        }));

builder.Services.AddGrpcClient<Employees.EmployeesClient>(o => o.Address = new Uri($"https://{ResourceNames.EmployeesService}"));
builder.Services.AddGrpcClient<Documents.DocumentsClient>(o => o.Address = new Uri($"https://{ResourceNames.EmployeesService}"));
builder.Services.AddGrpcClient<SalaryRequests.SalaryRequestsClient>(o => o.Address = new Uri($"https://{ResourceNames.SalariesService}"));

builder.Services
    .AddAuthorizationBuilder()
    .AddPolicy(Constants.ManagerAuthPolicyName, p => p.RequireRole(BusinessConstants.ManagerRole))
    .AddPolicy(Constants.HrManagerAuthPolicyName, p => p.RequireRole(BusinessConstants.HrManagerRole))
    .AddPolicy(
        Constants.ManagerOrHrManagerAuthPolicyName,
        p => p.RequireRole(BusinessConstants.ManagerRole, BusinessConstants.HrManagerRole));

builder.Services
    .AddIdentityCore<Employee>(options => options.Password.RequiredLength = AccountConstants.PasswordMinLength)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<EmployeesDbContext>()
    .AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policyBuilder => policyBuilder
    .WithOrigins(app.Configuration[EnvironmentVariableNames.WebFrontEndUrl]!)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.MapAccountEndpoints();
app.MapEmployeesEndpoints();
app.MapDocumentsEndpoints();
app.MapSalaryRequestsEndpoints();

app.MapDefaultEndpoints();

app.Run();

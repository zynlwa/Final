
using AppointmentSystem.Application.Common.Validators.Basket;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Automatic validation & client-side adapters
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Register validators
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateDoctorDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateDoctorDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePatientDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdatePatientDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAppointmentDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateMedicalServiceDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateMedicalServiceDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateAvailabilityDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ForgotPasswordDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ResetPasswordDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePromoCodeDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddBasketItemDtoValidator>();



//  Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

//  Application Layers
builder.Services.AddApplication();
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

//  FluentValidation (modern, deprecated olmayan üsul)
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//  Identity setup
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

//  JWT
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("role");
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("email");

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)),
        ClockSkew = TimeSpan.Zero,
        RoleClaimType = "role"
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception is SecurityTokenExpiredException)
            {
                context.NoResult();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Headers["Token-Expired"] = "true";
                context.Response.ContentType = "application/json";

                var response = Response<string>.Fail("Token expired", 401);
                var json = System.Text.Json.JsonSerializer.Serialize(response);

                return context.Response.WriteAsync(json);
            }
            return Task.CompletedTask;
        },
        OnForbidden = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var response = Response<string>.Fail("You do not have permission to access this resource.", 403);
            var json = System.Text.Json.JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        }
    };
});

builder.Services.AddAuthorization();

//  Build app
var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

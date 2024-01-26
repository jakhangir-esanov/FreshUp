using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace FreshUp.WebApi.Extentions;

public static class ServiceCollection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<CreateCategoryCommand, Category>, CreateCategoryCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteCategoryCommand, bool>, DeleteCategoryCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateCategoryCommand, Category>, UpdateCategoryCommandHandler>();
        services.AddTransient<IRequestHandler<GetCategoryQuery, CategoryResultDto>, GetCategoryQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResultDto>>, GetAllCategoriesQueryHandler>();

        services.AddTransient<IRequestHandler<CreateInventoryCommand, Inventory>, CreateInventoryCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteInventoryCommand, bool>, DeleteInventoryCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateInventoryCommand, Inventory>, UpdateInventoryCommandHandler>();
        services.AddTransient<IRequestHandler<GetInventoryQuery, InventoryResultDto>, GetInventoryQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllInventoriesQuery, IEnumerable<InventoryResultDto>>, GetAllInventoriesQueryHandler>();

        services.AddTransient<IRequestHandler<CreateOrderListCommand, OrderList>, CreateOrderListCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteOrderListCommand, bool>, DeleteOrderListCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateOrderListCommand, OrderList>, UpdateOrderListCommandHandler>();
        services.AddTransient<IRequestHandler<GetOrderListQuery, OrderListResultDto>, GetOrderListQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllOrderListsQuery, IEnumerable<OrderListResultDto>>, GetAllOrderListsQueryHandler>();

        services.AddTransient<IRequestHandler<CreateOrderCommand, Order>, CreateOrderCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteOrderCommand, bool>, DeleteOrderCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateOrderCommand, Order>, UpdateOrderCommandHandler>();
        services.AddTransient<IRequestHandler<GetOrderQuery, OrderResultDto>, GetOrderQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderResultDto>>, GetAllOrdersQueryHandler>();

        services.AddTransient<IRequestHandler<CreateProductCommand, Product>, CreateProductCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteProductCommand, bool>, DeleteProductCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateProductCommand, Product>, UpdateProductCommandHandler>();
        services.AddTransient<IRequestHandler<GetProductQuery, ProductResultDto>, GetProductQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResultDto>>, GetAllProductsQueryHandler>();

        services.AddTransient<IRequestHandler<CreateUserCommand, User>, CreateUserCommandHandler>();
        services.AddTransient<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommandHandler>();
        services.AddTransient<IRequestHandler<UpdateUserCommand, User>, UpdateUserCommandHandler>();
        services.AddTransient<IRequestHandler<GetUserQuery, UserResultDto>, GetUserQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllUsersQuery, IEnumerable<UserResultDto>>, GetAllUsersQueryHandler>();
    }

    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var Key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Key)
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
        {
            // Include 'SecurityScheme' to use JWT Authentication
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
        });
    }
}

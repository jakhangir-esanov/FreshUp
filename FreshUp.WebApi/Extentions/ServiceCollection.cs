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
}

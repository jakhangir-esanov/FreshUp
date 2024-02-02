using FreshUp.Domain.Enums;
using System.Runtime.CompilerServices;

namespace FreshUp.Application.Commands.Orders.UpdateOrder;

public record UpdateOrderCommand : IRequest<Order>
{
    public UpdateOrderCommand(long id, DateTime orderDate, Status status)
    {
        Id = id;
        OrderDate = orderDate;
        Status = status;
    }

    public long Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public Status Status { get; set; }
}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Order>
{
    private readonly IRepository<Order> repository;
    private readonly IRepository<OrderList> orderListRepository;
    private const string filePath = "C:\\Programming\\VisualStudio\\FreshUp\\FreshUp.Application\\Files";
    public UpdateOrderCommandHandler(IRepository<Order> repository, 
        IRepository<OrderList> orderListRepository)
    {
        this.repository = repository;
        this.orderListRepository = orderListRepository;
    }

    public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await this.repository.SelectAsync(x => x.Id.Equals(request.Id))
            ?? throw new NotFoundException("Order was not found!");

        var orderList = this.orderListRepository.SelectAll(x => x.OrderId.Equals(request.Id)).ToList()
            ?? throw new NotFoundException("Order was not found!");

        double totalAmount = 0;
        
        foreach(var item in orderList)
        {
            totalAmount += item.Price;
        }

        order.OrderDate = request.OrderDate;
        order.TotalAmount = totalAmount;
        order.Status = request.Status;

        this.repository.Update(order);
        await this.repository.SaveAsync();

        WriteOrderListToFile(orderList, order.Id.ToString(), totalAmount);

        return order;
    }

    private void WriteOrderListToFile(List<OrderList> orderList, string fileName,double totalAmount)
    {
        string fullPath = Path.Combine(filePath, $"{fileName}-order.txt");
        using (StreamWriter writer = new StreamWriter(fullPath))
        {
            int c = 1;
            writer.WriteLine(@"       CASH RECIEPT");
            writer.WriteLine("||||||||||||||||||||||||||||");
            writer.WriteLine("||||||||||||||||||||||||||||");
            writer.WriteLine("||||||||||||||||||||||||||||");
                          
            writer.WriteLine();
            writer.WriteLine(@"       FreshUp Store");
            writer.WriteLine();
            writer.WriteLine("=========================================================");
            writer.WriteLine($"date:             {DateTime.Now}");
            writer.WriteLine("Cashier:           John Smith");
            writer.WriteLine("=========================================================");
            foreach (var item in orderList)
            {
                if(item.Volume != 0)
                    writer.WriteLine($"{c}. {item.ProductName} ~ {item.Quantity} - {item.Volume} {item.Unit} | {item.Price/item.Quantity} amount: {item.Price}");
                else
                    writer.WriteLine($"{c}. {item.ProductName} ~ {item.Quantity} - {item.Unit} | {item.Price / item.Quantity} amount: {item.Price}");
                c++;
            }
            writer.WriteLine("=========================================================");
            writer.WriteLine($"Price:               {totalAmount}");
            writer.WriteLine($"QQS Tax(12%):             {totalAmount * 0.12}");
            writer.WriteLine();
            writer.WriteLine($"Total Amount: {totalAmount + totalAmount * 0.12}");
        }
    }
}

﻿using Bogus;
using DataAccess.Models;


namespace UnitTest.Mocks;

public class Constants
{
    public static Paper GetPaper(int featureCount = 0, int orderEntryCount = 0)
    {
        return new Faker<Paper>()
            .RuleFor(p => p.Id, f => f.IndexFaker + 1)
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Discontinued, f => f.Random.Bool())
            .RuleFor(p => p.Stock, f => f.Random.Int(0, 1000))
            .RuleFor(p => p.Price, f => double.Parse(f.Commerce.Price(1.0M, 1000.0M)))
            .RuleFor(p => p.OrderEntries, f => GetOrderEntries(orderEntryCount));
    }

    public static Properties GetProperties()
    {
        return new Faker<Properties>()
            .RuleFor(p => p.Id, f => f.IndexFaker + 1)
            .RuleFor(p => p.PropertyName, f => f.Commerce.ProductName());
    }

    public static List<OrderEntry> GetOrderEntries(int count)
    {
        return new Faker<OrderEntry>()
            .RuleFor(oe => oe.Id, f => f.IndexFaker + 1)
            .RuleFor(oe => oe.Quantity, f => f.Random.Int(1, 100))
            .Generate(count);
    }

    public static Order GetOrder(int totalAmount = 1000000)
    {
        return new Faker<Order>()
            .RuleFor(o => o.Id, f => f.IndexFaker + 1)
            .RuleFor(o => o.TotalAmount, f => totalAmount);
    }
}
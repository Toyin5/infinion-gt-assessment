using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository(IApplicationDbContext context) : IProductRepository
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Result> CreateProduct(ProductDto request)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            UserId = request.UserId,
            Status = Status.ACTIVE
        };
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync(new CancellationToken());
        return Result.Success("Product created successfully");
    }

    public async Task<Result> DeleteProduct(int Id)
    {
        var product = await _context.Products.FindAsync(Id);
        if (product == null)
        {
            return Result.Failure("Invalid product id");
        }
        product.Status = Status.DELETED;
        await _context.SaveChangesAsync(new CancellationToken());
        return Result.Success("Product deleted successfully", product);
    }

    public async Task<Result> GetAllProducts(int status)
    {
        var products = await _context.Products.IgnoreQueryFilters().ToListAsync();
        if (status > 0)
        {
            products = FilterByStatus(products, status);
        }
        return Result.Success("Products fetched successfully", products);
    }

    public async Task<Result> GetProduct(int Id)
    {
        var product = await _context.Products.FindAsync(Id);
        return Result.Success("Product fetched successfully", product);
    }

    public async Task<Result> UpdateProduct(int Id, ProductDto request)
    {
        var product = await _context.Products.FindAsync(Id);
        if (product == null)
        {
            return Result.Failure("Invalid product id");
        }
        product.Price = request.Price;
        product.Description = request.Description;
        product.Name = request.Name;
        _context.Products.Update(product);
        await _context.SaveChangesAsync(new CancellationToken());
        return Result.Success("Product updated successfully", product);
    }

    private List<Product> FilterByStatus(List<Product> products, int status)
    {
        Status statusEnum = (Status)status;
        switch (statusEnum)
        {
            case Status.ACTIVE:
                return products.Where(x => x.Status == Status.ACTIVE).ToList();
            case Status.DEACTIVATED:
                return products.Where(x => x.Status == Status.DEACTIVATED).ToList();
            case Status.DELETED:
                return products.Where(x => x.Status == Status.DELETED).ToList();
            default: return products;
        }
    }
}

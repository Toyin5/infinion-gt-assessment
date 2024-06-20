using Application.Models;
using Application.Models.Requests;

namespace Application.Interfaces;

public interface IProductRepository
{
    Task<Result> CreateProduct(ProductDto request);
    Task<Result> UpdateProduct(int Id, ProductDto request);
    Task<Result> DeleteProduct(int Id);
    Task<Result> GetProduct(int Id);
    Task<Result> GetAllProducts(int status);
}

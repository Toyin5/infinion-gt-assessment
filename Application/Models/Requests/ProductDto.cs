﻿namespace Application.Models.Requests;

public class ProductDto
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public string UserId { get; set; }
}

using System.Text.Json;
using clothingLayer.Context;
using clothingLayer.Contracts;
using clothingLayer.Entities;
using clothingLayer.Mappers;
using Microsoft.EntityFrameworkCore;

namespace clothingLayer.Services;

public class ClothingService : IClothingService
{
    private readonly ClothingsDbContext _clothingsDbContext;
    private readonly IWebHostEnvironment _env;

    public ClothingService(ClothingsDbContext clothingsDbContext, IWebHostEnvironment env)
    {
        _clothingsDbContext = clothingsDbContext;
        _env = env;
    }

    public async Task<List<ClothingResponse>> GetClothingsAsync()
    {
        var clothings = await _clothingsDbContext.Clothings.AsNoTracking().ToListAsync();
        var response = clothings.Select(clothingRequest => clothingRequest.ToClothingResponse()).ToList();
        return response;
    }

    public async Task<ClothingResponse> GetByIdAsync(string id)
    {
        var clothing = await _clothingsDbContext.Clothings.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return clothing == null ? throw new Exception("Clothing not found") : clothing.ToClothingResponse();
    }

    public async Task CreateClothingAsync(ClothingRequest request)
    {
        if (await _clothingsDbContext.Clothings.FirstOrDefaultAsync(c => c.Title == request.Title) != null)
        {
            throw new Exception("Clothing title already exists");
        }

        var clothing = new Clothing
        {
            Title = request.Title,
            Description = request.Description,
            Sizes = JsonSerializer.Serialize(request.Sizes),
            Price = request.Price,
            ImagePath = await GetFilePath(request.Image),
            Id = Guid.NewGuid().ToString()
        };
        _clothingsDbContext.Clothings.Add(clothing);
        await _clothingsDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(string id)
    {
        var clothing = await _clothingsDbContext.Clothings.FirstOrDefaultAsync(c => c.Id == id);
        if (clothing == null)
        {
            throw new Exception("Clothing not found");
        }

        _clothingsDbContext.Clothings.Remove(clothing);
        await _clothingsDbContext.SaveChangesAsync();
    }

    private async Task<string> GetFilePath(IFormFile file)
    {
        var dir = Path.Combine(_env.WebRootPath, "images");
        Directory.CreateDirectory(dir);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(dir, fileName);

        await using var stream = File.Create(filePath);
        await file.CopyToAsync(stream);

        return $"/images/{fileName}";
    }
}
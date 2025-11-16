using clothingLayer.Context;
using clothingLayer.Contracts;
using clothingLayer.Entities;
using clothingLayer.Mappers;
using Microsoft.EntityFrameworkCore;

namespace clothingLayer.Services;

public class ClothingService : IClothingService
{
    private readonly ClothingsDbContext _clothingsDbContext;

    public ClothingService(ClothingsDbContext clothingsDbContext)
    {
        _clothingsDbContext = clothingsDbContext;
    }

    public async Task<List<ClothingResponse>> GetClothingsAsync()
    {
        var clothings = await _clothingsDbContext.Clothings.AsNoTracking().ToListAsync();
        var response = clothings.Select(clothingRequest => clothingRequest.ToClothingResponse()).ToList();
        return response;
    }

    public async Task<ClothingResponse> GetByIdAsync(string id)
    {
        var clothing = await _clothingsDbContext.Clothings.FirstOrDefaultAsync(c => c.Id == id);
        return clothing == null ? throw new Exception("Clothing not found") : clothing.ToClothingResponse();
    }

    public async Task CreateClothingAsync(ClothingRequest clothingRequest)
    {
    }

    public async Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}
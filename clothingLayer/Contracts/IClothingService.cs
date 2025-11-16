using clothingLayer.Entities;

namespace clothingLayer.Contracts;

public interface IClothingService
{
    public Task<List<ClothingResponse>> GetClothingsAsync();
    public Task<ClothingResponse> GetByIdAsync(string id);
    public Task CreateClothingAsync(ClothingRequest clothingRequest);
    public Task DeleteAsync(string id);
}
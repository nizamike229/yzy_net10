using System.Text.Json;
using clothingLayer.Entities;

namespace clothingLayer.Mappers;

public static class ClothingMapper
{
    public static ClothingResponse ToClothingResponse(this Clothing clothing)
    {
        return new ClothingResponse
        {
            Title = clothing.Title,
            Description = clothing.Description,
            Price = clothing.Price,
            Sizes = JsonSerializer.Deserialize<char[]>(clothing.Sizes)!,
            ImagePath = clothing.ImagePath
        };
    }
}
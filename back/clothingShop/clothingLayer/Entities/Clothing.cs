namespace clothingLayer.Entities;

public class Clothing
{
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Sizes { get; set; } = null!;

    public int Price { get; set; }

    public string ImagePath { get; set; } = null!;

    public string Id { get; set; } = null!;
}
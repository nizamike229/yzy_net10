using System.ComponentModel.DataAnnotations;

namespace clothingLayer.Entities;

public class ClothingRequest
{
    [Required]
    [MinLength(3)]
    [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The field cannot consist only of spaces")]
    public required string Title { get; set; }

    [Required]
    [MinLength(30)]
    [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "The field cannot consist only of spaces")]
    public required string Description { get; set; }
    
    [Required] public required int Price { get; set; }

    [Required] public required char[] Sizes { get; set; }

    [Required] public required IFormFile Image { get; set; }
}

public class ClothingResponse
{
    [Required] public required string Id { get; set; }

    [Required] public required string Title { get; set; }

    [Required] [MinLength(50)] public required string Description { get; set; }

    [Required] public required int Price { get; set; }

    [Required] public required char[] Sizes { get; set; }

    [Required] public required string ImagePath { get; set; }
}
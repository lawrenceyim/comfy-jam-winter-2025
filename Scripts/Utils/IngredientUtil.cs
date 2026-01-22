using System;

public class IngredientUtil {
    private static readonly Random _random = new();

    private static readonly IngredientName[] _ingredients = [
        IngredientName.Beef,
        IngredientName.Bread,
        IngredientName.Butter,
        IngredientName.Carrots,
        IngredientName.Cheese,
        IngredientName.Chicken,
        IngredientName.Egg,
        IngredientName.Flour,
        IngredientName.Garlic,
        IngredientName.Milk,
        IngredientName.Onion,
        IngredientName.Pasta,
        IngredientName.Pepper,
        IngredientName.Potato,
        IngredientName.Salt,
        IngredientName.Tomato
    ];

    public static TextureId GetTextureId(IngredientName ingredient) {
        return ingredient switch {
            IngredientName.Beef => TextureId.Beef,
            IngredientName.Bread => TextureId.Bread,
            IngredientName.Butter => TextureId.Butter,
            IngredientName.Carrots => TextureId.Carrots,
            IngredientName.Cheese => TextureId.Cheese,
            IngredientName.Chicken => TextureId.Chicken,
            IngredientName.Egg => TextureId.Egg,
            IngredientName.Flour => TextureId.Flour,
            IngredientName.Garlic => TextureId.Garlic,
            IngredientName.Milk => TextureId.Milk,
            IngredientName.Onion => TextureId.Onion,
            IngredientName.Pasta => TextureId.Pasta,
            IngredientName.Pepper => TextureId.Pepper,
            IngredientName.Potato => TextureId.Potato,
            IngredientName.Salt => TextureId.Salt,
            IngredientName.Tomato => TextureId.Tomato,

            // fallback
            _ => TextureId.Beef,
        };
    }

    public static IngredientName[] GetIngredientNames() {
        return [
            IngredientName.Beef,
            IngredientName.Bread,
            IngredientName.Butter,
            IngredientName.Carrots,
            IngredientName.Cheese,
            IngredientName.Chicken,
            IngredientName.Egg,
            IngredientName.Flour,
            IngredientName.Garlic,
            IngredientName.Milk,
            IngredientName.Onion,
            IngredientName.Pasta,
            IngredientName.Pepper,
            IngredientName.Potato,
            IngredientName.Salt,
            IngredientName.Tomato
        ];
    }

    public static IngredientName GetRandomIngredientName() {
        return _ingredients[_random.Next(_ingredients.Length)];
    }
}
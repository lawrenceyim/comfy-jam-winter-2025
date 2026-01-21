public class RecipeUtil {
    public static RecipeName GetRecipeNameSorted(int index) {
        return index switch {
            1 => RecipeName.ChickenNoodleSoup,
            2 => RecipeName.FrenchFries,
            9 => RecipeName.FriedChicken,
            3 => RecipeName.FriedEgg,
            4 => RecipeName.GrilledCheese,
            0 => RecipeName.Mistake,
            5 => RecipeName.Pancakes,
            10 => RecipeName.Pizza,
            6 => RecipeName.Ramen,
            7 => RecipeName.SteakFrite,
            8 => RecipeName.TomatoBisque,
            _ => RecipeName.Mistake
        };
    }

    public static TextureId GetTextureId(RecipeName recipeName) {
        return recipeName switch {
            RecipeName.Mistake => TextureId.Mistake,
            RecipeName.ChickenNoodleSoup => TextureId.ChickenNoodleSoup,
            RecipeName.FrenchFries => TextureId.FrenchFries,
            RecipeName.FriedEgg => TextureId.FriedEgg,
            RecipeName.GrilledCheese => TextureId.GrilledCheese,
            RecipeName.Pancakes => TextureId.Pancakes,
            RecipeName.Ramen => TextureId.Ramen,
            RecipeName.SteakFrite => TextureId.SteakFrite,
            RecipeName.TomatoBisque => TextureId.TomatoBisque,
            RecipeName.FriedChicken => TextureId.FriedChicken,
            RecipeName.Pizza => TextureId.Pizza,

            _ => TextureId.Mistake
        };
    }
}
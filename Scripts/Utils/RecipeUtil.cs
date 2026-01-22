public class RecipeUtil {
    public static RecipeName GetRecipeNameSorted(int index) {
        return index switch {
            0 => RecipeName.Mistake,
            1 => RecipeName.ChickenNoodleSoup,
            2 => RecipeName.FrenchFries,
            3 => RecipeName.FriedEgg,
            4 => RecipeName.GrilledCheese,
            5 => RecipeName.Pancakes,
            6 => RecipeName.SteakFrite,
            7 => RecipeName.TomatoBisque,
            8 => RecipeName.FriedChicken,
            9 => RecipeName.Pizza,
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
            RecipeName.SteakFrite => TextureId.SteakFrite,
            RecipeName.TomatoBisque => TextureId.TomatoBisque,
            RecipeName.FriedChicken => TextureId.FriedChicken,
            RecipeName.Pizza => TextureId.Pizza,

            _ => TextureId.Mistake
        };
    }
}
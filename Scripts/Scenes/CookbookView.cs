using Godot;
using RepositorySystem;
using ServiceSystem;

public partial class CookbookView : Node2D {
    [Export]
    private Sprite2D _selectedRecipeSprite;

    [Export]
    private Label _selectedRecipeLabel;

    private TextureRepository _textureRepository;

    public override void _Ready() {
        DisplaySelectedRecipe(false);

        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        RepositoryLocator repositoryLocator = serviceLocator.GetService<RepositoryLocator>();
        _textureRepository = repositoryLocator.GetRepository<TextureRepository>(RepositoryName.Texture);

        
        // TODO: Remove test
        DisplaySelectedRecipe(true);
        SetSelectedRecipe(RecipeName.FrenchFries);
    }

    public void DisplaySelectedRecipe(bool visible) {
        _selectedRecipeSprite.Visible = visible;
    }

    public void SetSelectedRecipe(RecipeName recipeName) {
        Recipe recipe = RecipeInfo.GetRecipe(recipeName);

        _selectedRecipeLabel.Text = StringUtils.SplitPascalCase(recipe.Name.ToString());

        TextureId textureId = recipeName switch {
            RecipeName.Mistake => TextureId.Mistake,
            RecipeName.ChickenNoodleSoup => TextureId.ChickenNoodleSoup,
            RecipeName.FrenchFries => TextureId.FrenchFries,
            RecipeName.FriedEgg => TextureId.FriedEgg,
            RecipeName.GrilledCheese => TextureId.GrilledCheese,
            RecipeName.Pancakes => TextureId.Pancakes,
            RecipeName.Ramen => TextureId.Ramen,
            RecipeName.SteakFrite => TextureId.SteakFrite,
            RecipeName.TomatoBisque => TextureId.TomatoBisque,
            _ => TextureId.Mistake
        };

        _selectedRecipeSprite.Texture = _textureRepository.GetTexture(textureId);

        // TODO: Set ingredients
    }

    // make 3 by 2 card for ingredients
}
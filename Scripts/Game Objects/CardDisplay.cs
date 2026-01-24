using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using InputSystem;
using RepositorySystem;
using ServiceSystem;

public partial class CardDisplay : Node {
    public event Action<RecipeName, RecipeName> RecipeMadeVsIntended;

    [Export]
    private PackedScene _cardPrefab;

    [Export]
    private Button _cookButton;

    [Export]
    private Sprite2D _cookButtonSprite;

    [Export]
    private AudioStreamPlayer _ingredientSfx;

    private TextureRepository _textureRepository;
    private KitchenService _kitchenService;
    private SoundEffectRepository _soundEffectRepository;

    private readonly HashSet<Card> _selectedCards = [];
    private readonly Dictionary<Card, int> _cardIndex = new();
    private readonly List<Card> _hoveredCards = [];

    private IngredientName[] _ingredients;
    private Card[] _cards = [];

    private const int CardBaseZIndex = 10;
    private const int HoverZIndexBoost = 10;
    private int _cardWidth = 186;
    private int _xOffset = -50;
    private int _xCenter = 640;
    private int _ySpawn = 350;
    private Card _cardHovered = null;
    private int _selectedCardYOffset = 100;

    public override void _Ready() {
        ServiceLocator serviceLocator = GetNode<ServiceLocator>(ServiceLocator.AutoloadPath);
        _kitchenService = serviceLocator.GetService<KitchenService>();
        RepositoryLocator repositoryLocator = serviceLocator.GetService<RepositoryLocator>();
        _textureRepository = repositoryLocator.GetRepository<TextureRepository>(RepositoryName.Texture);
        _soundEffectRepository = repositoryLocator.GetRepository<SoundEffectRepository>(RepositoryName.SoundEffect);
        _cookButton.Pressed += _Cook;
        _DisplayCookButton();
    }

    public override void _Input(InputEvent @event) {
        if (@event is InputEventMouseButton { Pressed: true }) {
            _HandleMouseClick();
        }
    }

    public void ClearCards() {
        for (int i = 0; i < _cards.Length; i++) {
            _cards[i]?.QueueFree();
        }

        _cards = [];
    }

    public void InitIngredients(IngredientName[] ingredients) {
        _ingredients = ingredients;
        _cards = new Card[_ingredients.Length];
        int totalWidth = _ingredients.Length * _cardWidth + (_ingredients.Length - 1) * _xOffset;
        int xStart = _xCenter - totalWidth / 2;

        for (int i = 0; i < _ingredients.Length; i++) {
            Card card = (Card)_cardPrefab.Instantiate();
            card.SetTexture(_textureRepository.GetTexture(IngredientUtil.GetTextureId(_ingredients[i])));
            _cardIndex.Add(card, i);
            _cards[i] = card;
            AddChild(card);
            int x = xStart + i * (_xOffset + _cardWidth);
            card.Position = new Vector2(x, _ySpawn);
            card.Hovered += _HandleHover;
            card.ZIndex = i + CardBaseZIndex;
            card.IngredientName = _ingredients[i];
        }
    }

    private void _Cook() {
        Dictionary<IngredientName, int> ingredients = new();
        foreach (Card card in _selectedCards) {
            ingredients.Add(card.IngredientName, 1);
        }

        RecipeName recipe = RecipeInfo.FindRecipeByIngredients(ingredients);
        _kitchenService.RecipeDiscovered(recipe);

        _selectedCards.Clear();
        ClearCards();
        _DisplayCookButton();
        RecipeMadeVsIntended?.Invoke(recipe, _kitchenService.GetSelectedRecipe());
    }

    private void _HandleHover(Card card, bool hovered) {
        if (hovered) {
            _hoveredCards.Add(card);
            if (_cardHovered == null) {
                _cardHovered = card;
                _HoverEffect(card, true);
            }
        } else {
            _hoveredCards.Remove(card);
            _HoverEffect(card, false);
            _cardHovered = null;

            if (_hoveredCards.Count > 0) {
                _cardHovered = _hoveredCards[^1];
                _HoverEffect(_cardHovered, true);
            }
        }
    }

    private void _DisplayCookButton() {
        _cookButtonSprite.Visible = _selectedCards.Count > 0;
        _cookButton.Visible = _selectedCards.Count > 0;
    }

    private void _HoverEffect(Card card, bool hovered) {
        if (hovered) {
            card.HoverEffect(true);
            card.ZIndex = CardBaseZIndex + _cardIndex[card] + HoverZIndexBoost;
        } else {
            card.HoverEffect(false);
            card.ZIndex = CardBaseZIndex + _cardIndex[card];
        }
    }

    private void _HandleMouseClick() {
        if (_cardHovered != null) {
            if (_selectedCards.Contains(_cardHovered)) {
                _cardHovered.Position = new Vector2(_cardHovered.Position.X, _cardHovered.Position.Y + 100);
                _selectedCards.Remove(_cardHovered);
            } else {
                _ingredientSfx.Stream = _soundEffectRepository.GetSoundEffect(_GetSoundEffectId(_cardHovered.IngredientName));
                _ingredientSfx.Play();
                _selectedCards.Add(_cardHovered);
                _cardHovered.Position = new Vector2(_cardHovered.Position.X, _cardHovered.Position.Y - 100);
            }

            _DisplayCookButton();
        }
    }

    private SoundEffectId _GetSoundEffectId(IngredientName ingredient) {
        return ingredient switch {
            IngredientName.Beef => SoundEffectId.Beef,
            IngredientName.Bread => SoundEffectId.Bread,
            IngredientName.Butter => SoundEffectId.Butter,
            IngredientName.Carrots => SoundEffectId.Carrots,
            IngredientName.Cheese => SoundEffectId.Cheese,
            IngredientName.Chicken => SoundEffectId.Chicken,
            IngredientName.Egg => SoundEffectId.Egg,
            IngredientName.Flour => SoundEffectId.Flour,
            IngredientName.Garlic => SoundEffectId.Garlic,
            IngredientName.Milk => SoundEffectId.Milk,
            IngredientName.Onion => SoundEffectId.Onion,
            IngredientName.Pasta => SoundEffectId.Pasta,
            IngredientName.Pepper => SoundEffectId.Pepper,
            IngredientName.Potato => SoundEffectId.Potato,
            IngredientName.Salt => SoundEffectId.Salt,
            IngredientName.Tomato => SoundEffectId.Tomato,
        };
    }
}
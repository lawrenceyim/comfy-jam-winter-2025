using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class NewScript : Node {
    public override void _Ready() {
        string input = """
                       Fried egg: egg, salt, pepper,
                       Grilled cheese: bread, butter, cheese
                       Pancakes: milk, egg, flour, butter
                       French fries: potato, salt, pepper
                       Chicken noodle soup: carrots, potato, chicken, pasta
                       Tomato bisque: tomato, milk, carrots, onion
                       Steak frite: beef, garlic, onion, salt, pepper, potato
                       """;
        string[] lines = input.Split('\n');

        // Recipes
        HashSet<string> recipeNames = new();
        foreach (string line in lines) {
            recipeNames.Add(line.Split(":")[0]);
        }

        List<string> recipeNamesList = recipeNames.ToList();
        recipeNamesList.Sort();
        int i = 1;
        recipeNamesList.ForEach(recipeName => {
                recipeName = _ToTitleCaseManual(recipeName);
                recipeName = recipeName.Replace(" ", "");
                GD.Print($"{recipeName} = {i++},");
            }
        );

        // Ingredients
        HashSet<string> uniqueIngredients = new HashSet<string>();
        foreach (string line in lines) {
            line.Split(":")[1]
                .Split(",")
                .Select(i => i.Trim())
                .ToList()
                .ForEach(i => uniqueIngredients.Add(i));
        }

        HashSet<string> recipes = [];

        List<string> list = uniqueIngredients.ToList().Where(i => i != "" && !recipes.Contains(i)).ToList();
        list.Sort();
        i = 1;
        foreach (string ingredient in list) {
            GD.Print($"{ingredient.Capitalize()} = {i++},");
        }
    }

    private static string _ToTitleCaseManual(string str) {
        if (string.IsNullOrEmpty(str)) {
            return str;
        }

        IEnumerable<string> words = str.Split(' ')
            .Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower());

        return string.Join(" ", words);
    }
}
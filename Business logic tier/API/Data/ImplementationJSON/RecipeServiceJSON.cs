using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using API.Models;

namespace API.Data
{
    public class RecipeServiceJSON
    {
        private string RecipesFile = "recipes.json";
        private IList<Recipe> allRecipes;

        public RecipeServiceJSON()
        {
            if (File.Exists(RecipesFile))
            {
                string recipesInJSON = File.ReadAllText(RecipesFile);
                allRecipes = JsonSerializer.Deserialize<IList<Recipe>>(recipesInJSON);
            }
            else
            {
                Seed();
                Save();
            }
        }

        private void Seed()
        {
            Ingredient[] ingredients = {new Ingredient() {Calories = 200, Name = "Rice"}};
            Recipe[] recipes =
            {
                new Recipe()
                {
                    Id = 1,
                    Calories = 350,
                    Name = "Risotto",
                    Price = 199.9,
                    Ingredient = ingredients.ToList(),
                    NutritionType = "Vegan"
                }
            };
            allRecipes = recipes.ToList();
        }

        private void Save()
        {
            string recipesInJson = JsonSerializer.Serialize(allRecipes);
            File.WriteAllText(RecipesFile, recipesInJson);
        }

        public Recipe GetRecipe(int id)
        {
            var returned = allRecipes.First(i => i.Id == id);
            if (returned == null) throw new Exception("Recipe do not exist");
            return returned;
        }

        public Recipe AddRecipe(Recipe recipe)
        {
            Recipe first = null;
            try
            {
                first = allRecipes.First(i => i.Name.Equals(recipe.Name));
            }
            catch (Exception e)
            {
            }

            if (first != null)
            {
                throw new Exception("This Recipe already exists");
            }

            int max = allRecipes.Max(i => i.Id);
            recipe.Id = (++max);
            allRecipes.Add(recipe);
            Save();
            return recipe;
        }


        public Recipe UpdateRecipe(Recipe recipe)
        {
            Recipe toUpdate = allRecipes.First(i => i.Id == recipe.Id);
            if (toUpdate == null) throw new Exception("Recipe does not exist");
            toUpdate.Calories = recipe.Calories;
            toUpdate.Name = recipe.Name;
            Save();
            return toUpdate;
        }

        public Recipe RemoveRecipe(Recipe recipe)
        {
            Recipe toRemove = allRecipes.First(i => i.Id == recipe.Id);
            if (toRemove == null) throw new Exception("Recipe does not exist");
            allRecipes.Remove(toRemove);
            Save();
            return toRemove;
        }
    }
}
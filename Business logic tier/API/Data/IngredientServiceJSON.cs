using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using API.Models;

namespace API.Data
{
    public class IngredientServiceJSON : IIngredientService 
    {
        private string IngredientsFile = "ingredients.json";
        private IList<Ingredient> allIngredients;

        public IngredientServiceJSON()
        {
            if (File.Exists(IngredientsFile))
            {
                string ingredientsInJSON = File.ReadAllText(IngredientsFile);
                allIngredients = JsonSerializer.Deserialize<IList<Ingredient>>(ingredientsInJSON);
            }
            else
            {
                Seed();
                Save();
            }
        }

        private void Seed()
        {
            Ingredient[] ingredients =
            {
                new Ingredient()
                {
                    Id = 1,
                    Calories = 29,
                    Name = "Broccoli"
                }
            };
            allIngredients = ingredients.ToList();
        }

        private void Save()
        {
            string ingredientsInJson = JsonSerializer.Serialize(allIngredients);
            File.WriteAllText(IngredientsFile, ingredientsInJson);
        }

        public Ingredient GetIngredient(int id)
        {
            var returned = allIngredients.First(i => i.Id == id);
            if (returned == null) throw new Exception("Ingredient do not exist");
            return returned;
        }

        public IList<Ingredient> GetAllIngredients()
        {
            
            return allIngredients;
        }

        public Ingredient AddIngredient(Ingredient ingredient)
        {
            Ingredient first = null;
            try
            {
                first = allIngredients.First(i => i.Name.Equals(ingredient.Name));
            }
            catch (Exception e)
            {
            }

            if (first != null)
            {
                throw new Exception("This Ingredient already exists");
            }

            int max = allIngredients.Max(i => i.Id);
            ingredient.Id = (++max);
            allIngredients.Add(ingredient);
            Save();
            return ingredient;
        }


        public Ingredient UpdateIngredient(Ingredient ingredient)
        {
            Ingredient toUpdate = allIngredients.First(i => i.Id == ingredient.Id);
            if (toUpdate == null) throw new Exception("Ingredient does not exist");
            toUpdate.Calories = ingredient.Calories;
            toUpdate.Name = ingredient.Name;
            Save();
            return toUpdate;
        }

        public Ingredient RemoveIngredient(Ingredient ingredient)
        {
            Ingredient toRemove = allIngredients.First(i => i.Id == ingredient.Id);
            if (toRemove == null) throw new Exception("Ingredient does not exist");
            allIngredients.Remove(toRemove);
            Save();
            return toRemove;
        }
        
    }
}
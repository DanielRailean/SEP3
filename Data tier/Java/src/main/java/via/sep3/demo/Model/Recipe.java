package via.sep3.demo.Model;

import java.util.ArrayList;
import java.util.List;

public class Recipe {
    int Id;
    String Name;
    ArrayList<Ingredient> Ingredient;
    double Calories;
    String NutritionType;
    String FoodType;
    double Price;

    public Recipe() {
    }

    public Recipe(int id,String name, ArrayList ingredient, double calories, String nutritionType, String foodType, double price) {
        Id=id;
        Name = name;
        Ingredient = ingredient;
        Calories = calories;
        NutritionType = nutritionType;
        FoodType = foodType;
        Price = price;
    }

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public ArrayList<Ingredient> getIngredient() {
        return Ingredient;
    }

    public void setIngredient(ArrayList ingredient) {
        Ingredient = ingredient;
    }

    public double getCalories() {
        return Calories;
    }

    public void setCalories(double calories) {
        Calories = calories;
    }

    public String getNutritionType() {
        return NutritionType;
    }

    public void setNutritionType(String nutritionType) {
        NutritionType = nutritionType;
    }

    public String getFoodType() {
        return FoodType;
    }

    public void setFoodType(String foodType) {
        FoodType = foodType;
    }

    public double getPrice() {
        return Price;
    }

    public void setPrice(double price) {
        Price = price;
    }

    @Override
    public String toString() {
        return "Recipe{" +
                "Name='" + Name + '\'' +
                ", Ingredient=" + Ingredient +
                ", Calories=" + Calories +
                ", NutritionType='" + NutritionType + '\'' +
                ", FoodType='" + FoodType + '\'' +
                ", Price=" + Price +
                '}';
    }
}

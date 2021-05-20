package via.sep3.food.Model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import java.util.List;

@Entity
public class Recipe {
    @javax.persistence.Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    int Id;
    String Name;
    List<Ingredient> Ingredient;
    double Calories;
    String NutritionType;
    String FoodType;
    double Price;

    public Recipe() {
    }

    public Recipe(int id,String name, double calories, String nutritionType, String foodType, double price) {
        Id=id;
        Name = name;
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

    public List<Ingredient> getIngredient() {
        return Ingredient;
    }

    public void setIngredient(List ingredient) {
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
                ", Calories=" + Calories +
                ", NutritionType='" + NutritionType + '\'' +
                ", FoodType='" + FoodType + '\'' +
                ", Price=" + Price +
                '}';
    }
}

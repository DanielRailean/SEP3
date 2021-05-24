package via.sep3.food.Model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity
public class Recipe {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    int id;
    String name;
    String IngredientIdList;
    String IngredientQuantityList;
    double Calories;
    public int MinutesToMake;
    String NutritionType;
    String Description;
    String Image;
    double Price;

    public double getPrice() {
        return Price;
    }

    public void setPrice(double price) {
        Price = price;
    }

    public Recipe() {
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public double getCalories() {
        return Calories;
    }

    public void setCalories(double calories) {
        Calories = calories;
    }

    public int getMinutesToMake() {
        return MinutesToMake;
    }

    public void setMinutesToMake(int minutesToMake) {
        MinutesToMake = minutesToMake;
    }

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public String getImage() {
        return Image;
    }

    public void setImage(String image) {
        Image = image;
    }

    public String getIngredientIdList() {
        return IngredientIdList;
    }

    public void setIngredientIdList(String ingredientIdList) {
        IngredientIdList = ingredientIdList;
    }

    public String getIngredientQuantityList() {
        return IngredientQuantityList;
    }

    public void setIngredientQuantityList(String ingredientQuantityList) {
        IngredientQuantityList = ingredientQuantityList;
    }

    public String getNutritionType() {
        return NutritionType;
    }

    public void setNutritionType(String nutritionType) {
        NutritionType = nutritionType;
    }

    @Override
    public String toString() {
        return "Recipe{" +
                "Name='" + name + '\'' +
                ", Calories=" + Calories +
                ", MinutesToMake=" + MinutesToMake +
                ", Type=" + NutritionType +
                ", Description='" + Description + '\'' +
                ", Image='" + Image + '\'' +
                '}';
    }
}

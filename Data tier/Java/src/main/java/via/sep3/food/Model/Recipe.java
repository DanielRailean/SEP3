package via.sep3.food.Model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import java.util.List;

@Entity
public class Recipe {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    int Id;
    String Name;
    List<Ingredient> Ingredient;
    double Calories;
    public int MinutesToMake;
    NutritionType Type;
    String Description;
    String Image;

    public Recipe() {
    }

    public Recipe( String name, List<Ingredient> ingredient, double calories, int minutesToMake, NutritionType type, String description, String image) {

        Name = name;
        Ingredient = ingredient;
        Calories = calories;
        MinutesToMake = minutesToMake;
        Type = type;
        Description = description;
        Image = image;
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

    public void setIngredient(List<Ingredient> ingredient) {
        Ingredient = ingredient;
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

    public NutritionType getType() {
        return Type;
    }

    public void setType(NutritionType type) {
        Type = type;
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

    @Override
    public String toString() {
        return "Recipe{" +
                "Name='" + Name + '\'' +
                ", Ingredient=" + Ingredient +
                ", Calories=" + Calories +
                ", MinutesToMake=" + MinutesToMake +
                ", Type=" + Type +
                ", Description='" + Description + '\'' +
                ", Image='" + Image + '\'' +
                '}';
    }
}

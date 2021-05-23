package via.sep3.food.Model;


import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;

@Entity
public class Ingredient {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    int Id;
    String name;
    String UnitOfMeasure;
    double Calories;


    public Ingredient() {
    }

    public Ingredient( String name, String unitOfMeasure, double calories) {
        this.name = name;
        UnitOfMeasure = unitOfMeasure;
        Calories = calories;
    }

    public String getUnitOfMeasure() {
        return UnitOfMeasure;
    }

    public void setUnitOfMeasure(String unitOfMeasure) {
        UnitOfMeasure = unitOfMeasure;
    }

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
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

    @Override
    public String toString() {
        return "Ingredient{" +
                "Id=" + Id +
                ", Name='" + name + '\'' +
                ", Calories=" + Calories +
                '}';
    }
}

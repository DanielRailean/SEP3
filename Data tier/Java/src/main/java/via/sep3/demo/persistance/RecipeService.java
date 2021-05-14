package via.sep3.demo.persistance;

import org.springframework.stereotype.Service;
import via.sep3.demo.Model.Ingredient;
import via.sep3.demo.Model.Recipe;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

@Service
public class RecipeService extends Connection implements IRecipeService{

    @Override
    public Recipe AddRecipe(Recipe recipe) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement(
                            "insert into recipes(Name, Calories,NutritionType,FoodType,Price) values(?,?,?,?,?)", Statement.RETURN_GENERATED_KEYS);
            preparedStatement.setString(1,recipe.getName());
            preparedStatement.setDouble(2,recipe.getCalories());
            preparedStatement.setString(3,recipe.getNutritionType());
            preparedStatement.setString(4,recipe.getFoodType());
            preparedStatement.setDouble(5,recipe.getCalories());

            preparedStatement.executeUpdate();
            System.out.println("Recipe is added.");

        }
        catch (SQLException throwables)
        {
            throwables.printStackTrace();
        }
        return recipe;
    }

    @Override
    public List<Recipe> GetAllRecipes() {
         List<Recipe > recipes=new ArrayList<>();
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("select * from Recipes");
            ResultSet resultSet = preparedStatement.executeQuery();

            while(resultSet.next()){
                int id= resultSet.getInt("Id");
                String Name = resultSet.getString("Name");
                int Calories = resultSet.getInt("Calories");
                String NutritionType = resultSet.getString("NutritionType");
                String FoodType = resultSet.getString("FoodType");
                double Price = resultSet.getInt("Price");

                Recipe recipe= new Recipe(id,Name,Calories,NutritionType,FoodType,Price);

                recipes.add(recipe);
            }
        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
        System.out.println("All recipes are here.");

        return recipes;
    };


    @Override
    public Recipe GetRecipe(String name) {
        try(java.sql.Connection connection = getConnection())
        {
            System.out.println(name);
            PreparedStatement preparedStatement =
                    connection.prepareStatement("select * from Recipes where Name=?");
            preparedStatement.setString(1,name);

            ResultSet resultSet = preparedStatement.executeQuery();

            if(resultSet.next()){
                int id= resultSet.getInt("Id");
                String Name = resultSet.getString("Name");
                double Calories = resultSet.getDouble("Calories");
                String NutritionType = resultSet.getString("NutritionType");
                String FoodType = resultSet.getString("FoodType");
                double Price = resultSet.getInt("Price");

                Recipe recipe= new Recipe(id,Name,Calories,NutritionType,FoodType,Price);

                System.out.println("Recipe Found.");
                return recipe ;           }
        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
        return null;
    }




    @Override
    public Recipe RemoveRecipe(Recipe recipe) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("delete from Recipes where Name = ?");
            preparedStatement.setString(1,recipe.getName());
            preparedStatement.executeUpdate();

            System.out.println("Recipe removed.");
        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
        return recipe;}



    @Override
    public Recipe UpdateRecipe(Recipe recipe) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement(
                            "update recipes set calories = ?  where name = ?", Statement.RETURN_GENERATED_KEYS);
            preparedStatement.setDouble(1,recipe.getCalories());
            preparedStatement.setString(2,recipe.getName());


            preparedStatement.executeUpdate();
            System.out.println("Recipe updated.");

        }
        catch (SQLException throwables)
        {
            throwables.printStackTrace();
        }
        return recipe;
    }
    }


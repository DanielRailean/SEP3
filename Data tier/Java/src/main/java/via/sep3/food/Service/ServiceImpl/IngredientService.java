package via.sep3.food.Service.ServiceImpl;

import org.springframework.stereotype.Service;
import via.sep3.food.Model.Ingredient;
import via.sep3.food.Service.Connection;
import via.sep3.food.Service.IIngredientService;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

@Service
public class IngredientService extends Connection implements IIngredientService {
    public java.sql.Connection getConnection() throws SQLException
    {
        return super.getConnection();
    }
    @Override
    public List<Ingredient> GetAllIngredients() {
        List<Ingredient> ingredients=new ArrayList<>();
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("select * from Ingredients");
            ResultSet resultSet = preparedStatement.executeQuery();

            while(resultSet.next()){
                int id= resultSet.getInt("Id");
                String Name = resultSet.getString("Name");
                int Calories = resultSet.getInt("Calories");
                Ingredient ingredient= new Ingredient(id,Name,Calories);

                ingredients.add(ingredient);
            }
        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
        System.out.println("All ingredients are here.");

        return ingredients;
    }


    @Override
    public Ingredient GetOneIngredient(String name) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("select * from Ingredients where name=?");
            preparedStatement.setString(1,name);

            ResultSet resultSet = preparedStatement.executeQuery();

            while(resultSet.next()){
                int id= resultSet.getInt("Id");
                String Name = resultSet.getString("Name");
                int Calories = resultSet.getInt("Calories");
                Ingredient ingredient= new Ingredient(id,Name,Calories);
                System.out.println("Ingredient found.");

            return ingredient ;           }
        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
        return null;
    }

    @Override
    public Ingredient AddIngredient(Ingredient ingredient) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement(
                            "insert into Ingredients(Name, Calories) values(?,?)", Statement.RETURN_GENERATED_KEYS);
            preparedStatement.setString(1,ingredient.getName());
            preparedStatement.setDouble(2,ingredient.getCalories());

            preparedStatement.executeUpdate();
            System.out.println("Ingredient added.");

        }
        catch (SQLException throwables)
        {
            throwables.printStackTrace();
        }
        return ingredient;
    }

    @Override
    public Ingredient RemoveIngredient(Ingredient ingredient) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("delete from Ingredients where Name = ?");
            preparedStatement.setString(1,ingredient.getName());
            preparedStatement.executeUpdate();

            System.out.println("Ingredient removed.");
        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
    return ingredient;}

    @Override
    public Ingredient UpdateIngredient(Ingredient ingredient) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement(
                            "update Ingredients set calories = ?  where name = ?", Statement.RETURN_GENERATED_KEYS);
            preparedStatement.setDouble(1,ingredient.getCalories());
            preparedStatement.setString(2,ingredient.getName());


            preparedStatement.executeUpdate();
            System.out.println("Ingredient updated.");

        }
        catch (SQLException throwables)
        {
            throwables.printStackTrace();
        }
        return ingredient;
    }
    }



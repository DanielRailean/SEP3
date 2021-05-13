package via.sep3.demo.persistance;

import via.sep3.demo.Model.Ingredient;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

public class IngredientService extends Connection implements IIngredientService{
    public java.sql.Connection getConnection() throws SQLException
    {
        return super.getConnection();
    }
    @Override
    public ArrayList<Ingredient> GetAllIngredients() {
        ArrayList<Ingredient> ingredients=new ArrayList<>();
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

        return ingredients;    }


    @Override
    public Ingredient GetOneIngredient(String name) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("select * from Ingredients where Name=?");
            preparedStatement.setString(1,name);

            ResultSet resultSet = preparedStatement.executeQuery();

            while(resultSet.next()){
                int id= resultSet.getInt("Id");
                String Name = resultSet.getString("Name");
                int Calories = resultSet.getInt("Calories");
                Ingredient ingredient= new Ingredient(id,Name,Calories);

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
                            "update Ingredients set password value (?) where email = ?", Statement.RETURN_GENERATED_KEYS);
            preparedStatement.setString(1,ingredient.getName());
            preparedStatement.setDouble(2,ingredient.getCalories());

            preparedStatement.executeUpdate();

        }
        catch (SQLException throwables)
        {
            throwables.printStackTrace();
        }
        return ingredient;
    }
    }



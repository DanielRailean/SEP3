package via.sep3.demo.persistance;

import org.springframework.stereotype.Service;
import via.sep3.demo.Model.User;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

@Service
public class UserService extends Connection implements IUserService{
    public java.sql.Connection getConnection() throws SQLException
    {
        return super.getConnection();
    }

    @Override
    public void AddUser(User user) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement(
                            "insert into Users(Email, Password) values(?,?)", Statement.RETURN_GENERATED_KEYS);
            preparedStatement.setString(1,user.getEmail());
            preparedStatement.setString(2,user.getPassword());

            preparedStatement.executeUpdate();

        }
        catch (SQLException throwables)
        {
            throwables.printStackTrace();
        }

    }

    @Override
    public User getUser(String Email) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("select * from Users where Email = ?");
            preparedStatement.setString(1, Email);

            ResultSet resultSet = preparedStatement.executeQuery();

            if(resultSet.next()){
                String email = resultSet.getString("Email");
                String Password=resultSet.getString("Password");
                User user=new User(email,Password);
                return user;
            }

        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
        return null;
    }


    @Override
    public void DeleteUser(String Email) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("delete from Users where Email = ?");
            preparedStatement.setString(1,Email);
            preparedStatement.executeUpdate();
        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
    }

    @Override
    public void UpdateUser(User user) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement(
                            "update Users set password value (?) where email = ?", Statement.RETURN_GENERATED_KEYS);
            preparedStatement.setString(1,user.getPassword());
            preparedStatement.setString(2,user.getEmail());

            preparedStatement.executeUpdate();

        }
        catch (SQLException throwables)
        {
            throwables.printStackTrace();
        }
    }
}

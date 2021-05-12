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
    public User RegisterUser(User user) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement(
                            "insert into Users(,UserIdEmail, Password,FirstName,LastName,Phone,Address,PostalCode) values(?,?)", Statement.RETURN_GENERATED_KEYS);
            preparedStatement.setInt(1,user.getUserId());
            preparedStatement.setString(1,user.getEmail());
            preparedStatement.setString(2,user.getPassword());
            preparedStatement.setString(2,user.getFirstName());
            preparedStatement.setString(2,user.getLastName());
            preparedStatement.setInt(2,user.getPhone());
            preparedStatement.setString(2,user.getAddress());
            preparedStatement.setInt(2,user.getPostalCode());

            preparedStatement.executeUpdate();

        }
        catch (SQLException throwables)
        {
            throwables.printStackTrace();
        }
        return user;
    }

    @Override
    public User ValidateUser(String Email,String Password) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("select * from Users where FirstName= ?");
            preparedStatement.setString(1, Email);
            preparedStatement.setString(1, Password);

            ResultSet resultSet = preparedStatement.executeQuery();

            if(resultSet.next()){
                int UserId = resultSet.getInt("UserId");
                String email = resultSet.getString("Email");
                String password=resultSet.getString("Password");
                String FirstName=resultSet.getString("FirstName");
                String LastName=resultSet.getString("SecondName");
                int Phone=resultSet.getInt("Phone");
                String Address=resultSet.getString("Address");
                int PostalCode=resultSet.getInt("PostalCode");

                User user=new User(UserId,email,password,FirstName,LastName,Phone,Address,PostalCode);
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
    public User RemoveUser(User user) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("delete from Users where Email = ?");
            preparedStatement.setString(1,user.getEmail());
            preparedStatement.executeUpdate();
        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
        return user;
    }

    @Override
    public User UpdateUser(User user,String Password) {
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
        return user;
    }
}

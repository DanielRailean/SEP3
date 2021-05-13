package via.sep3.demo.persistance;

import org.springframework.stereotype.Service;
import via.sep3.demo.Model.Administrator;
import via.sep3.demo.Model.User;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

@Service
public class AdministratorService extends Connection implements IAdministratorService{

    public java.sql.Connection getConnection() throws SQLException
    {
        return super.getConnection();
    }

    @Override
    public Administrator AddAdministrator(Administrator administrator) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement(
                            "insert into Adminsitrators(Email, Password) values(?,?)", Statement.RETURN_GENERATED_KEYS);
            preparedStatement.setString(1,administrator.getEmail());
            preparedStatement.setString(1,administrator.getPassword());

            preparedStatement.executeUpdate();

        }
        catch (SQLException throwables)
        {
            throwables.printStackTrace();
        }
        return administrator;
    }


    @Override
    public Administrator ValidateAdministrator(String Email, String Password) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("select * from Administrators where Email= ? & Password=?");
            preparedStatement.setString(1, Email);
            preparedStatement.setString(1, Password);

            ResultSet resultSet = preparedStatement.executeQuery();

            if(resultSet.next()){
                int id=resultSet.getInt("Id");
                String email = resultSet.getString("Email");
                String password=resultSet.getString("Password");


                Administrator administrator=new Administrator(id,email,password);
                return administrator;
            }

        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
        return null;
    }



    @Override
    public Administrator RemoveAdministrator(Administrator administrator) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("delete from Administrators where Email = ?");
            preparedStatement.setString(1,administrator.getEmail());

            preparedStatement.executeUpdate();
        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
        return administrator;
    }


    @Override
    public Administrator UpdateAdministrator(Administrator administrator, String Password) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement(
                            "update Administrators set password value (?) where email = ?", Statement.RETURN_GENERATED_KEYS);
            preparedStatement.setString(1,administrator.getPassword());
            preparedStatement.setString(2,administrator.getEmail());

            preparedStatement.executeUpdate();

        }
        catch (SQLException throwables)
        {
            throwables.printStackTrace();
        }
        return administrator;
    }
}

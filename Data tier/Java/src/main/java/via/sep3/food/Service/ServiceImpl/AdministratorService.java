package via.sep3.food.Service.ServiceImpl;

import org.springframework.stereotype.Service;
import via.sep3.food.Model.Administrator;
import via.sep3.food.Service.Connection;
import via.sep3.food.Service.IAdministratorService;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

@Service
public class AdministratorService extends Connection implements IAdministratorService {

    public java.sql.Connection getConnection() throws SQLException
    {
        return super.getConnection();
    }


    @Override
    public Administrator ValidateAdministrator(String Email, String Password) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("select * from administrators where Email=? and Password=?");
            preparedStatement.setString(1, Email);
            preparedStatement.setString(2, Password);

            ResultSet resultSet = preparedStatement.executeQuery();

            if(resultSet.next()){
                int id = resultSet.getInt("Id");
                String email = resultSet.getString("Email");
                String password=resultSet.getString("Password");


                Administrator administrator=new Administrator(id,email,password);

                System.out.println("Administrator is logged in");
                return administrator;
            }

        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
        return null;
    }




}

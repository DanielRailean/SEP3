package via.sep3.demo.persistance;

import org.springframework.stereotype.Service;
import via.sep3.demo.Model.Order;
import via.sep3.demo.Model.User;

import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.time.LocalDate;
import java.util.ArrayList;
@Service
public class OrderService extends Connection implements IOrderService{

    public java.sql.Connection getConnection() throws SQLException
    {
        return super.getConnection();
    }

    @Override
    public Order GetOrder(int orderid) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("select * from Orders where orderid=?");
            preparedStatement.setInt(1, orderid);

            ResultSet resultSet = preparedStatement.executeQuery();

            if(resultSet.next()){
                int OrderId=resultSet.getInt("orderid");
                LocalDate date = (LocalDate) resultSet.getObject("orderDate");
                String invoiceAddress=resultSet.getString("InvoiceAddress");
                String deliveryAddress=resultSet.getString("DeliveryAddress");
                String city=resultSet.getString("City");
                int postalCode=resultSet.getInt("PostalCode");
                int TotalPrice=resultSet.getInt("TotalPrice");
                int itemPrice=resultSet.getInt("ItemPrice");
                int deliveryPrice=resultSet.getInt("DeliveryPrice");
                String Currency=resultSet.getString("Currency");
                boolean isDelivered=resultSet.getBoolean("IsDelivered");
                int UserId=  resultSet.getInt("userId");


                Order order=new Order(OrderId,date,invoiceAddress,deliveryAddress,city,postalCode,TotalPrice,itemPrice,deliveryPrice,Currency,isDelivered,UserId);

                System.out.println("Order found.");
                return order;
            }

        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
        return null;
    }


    @Override
    public Order GetUserOrders(int userId) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("select * from Orders where userId=?");
            preparedStatement.setObject(1, userId);

            ResultSet resultSet = preparedStatement.executeQuery();

            if(resultSet.next()){
                int orderId=resultSet.getInt("orderid");
                LocalDate date = (LocalDate) resultSet.getObject("orderDate");
                String invoiceAddress=resultSet.getString("InvoiceAddress");
                String deliveryAddress=resultSet.getString("DeliveryAddress");
                String city=resultSet.getString("City");
                int postalCode=resultSet.getInt("PostalCode");
                int TotalPrice=resultSet.getInt("TotalPrice");
                int itemPrice=resultSet.getInt("ItemPrice");
                int deliveryPrice=resultSet.getInt("DeliveryPrice");
                String Currency=resultSet.getString("Currency");
                boolean isDelivered=resultSet.getBoolean("IsDelivered");
                int UserId= resultSet.getInt("userId");

                Order order=new Order(orderId,date,invoiceAddress,deliveryAddress,city,postalCode,TotalPrice,itemPrice,deliveryPrice,Currency,isDelivered,UserId);

                System.out.println("Users order found.");
                return order;
            }

        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
        return null;
    }


    @Override
    public Order AddOrder(Order order) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement(
                            "insert into Orders( InvoiceAddress,DeliveryAddress,City,PostalCode,TotalPrice,ItemPrice,DeliveryPrice,Currency,userId) values(?,?,?,?,?,?,?,?,?)");
          preparedStatement.setString(1,order.getInvoiceAddress());
            preparedStatement.setString(2,order.getDeliveryAddress());
            preparedStatement.setString(3,order.getCity());
            preparedStatement.setInt(4,order.getPostalCode());
            preparedStatement.setDouble(5,order.getTotalPrice());
            preparedStatement.setDouble(6,order.getItemPrice());
            preparedStatement.setDouble(7,order.getDeliveryPrice());
            preparedStatement.setString(8,order.getCurrency());
            preparedStatement.setInt(9,order.getUserId());

            preparedStatement.executeUpdate();
            System.out.println("Order is added");

        }
        catch (SQLException throwables)
        {
            throwables.printStackTrace();
        }
        return order;
    }
    @Override
    public Order RemoveOrder(Order order) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement("delete from Orders where orderid=? ");
            preparedStatement.setInt(1,order.getOrderid());
            preparedStatement.executeUpdate();

            System.out.println("Order deleted.");
        }
        catch (SQLException e)
        {
            e.printStackTrace();
        }
        return order;
    }

    @Override
    public Order UpdateOrder(Order order) {
        try(java.sql.Connection connection = getConnection())
        {
            PreparedStatement preparedStatement =
                    connection.prepareStatement(
                            "update orders set deliveryAddress = ? where orderid = ?", Statement.RETURN_GENERATED_KEYS);
            preparedStatement.setString(1, order.getDeliveryAddress());
            preparedStatement.setInt(2,order.getOrderid());

            preparedStatement.executeUpdate();

            System.out.println("order updated.");

        }
        catch (SQLException throwables)
        {
            throwables.printStackTrace();
        }
        return order;
    }

}

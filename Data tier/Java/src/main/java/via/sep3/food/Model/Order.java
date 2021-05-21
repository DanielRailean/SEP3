package via.sep3.food.Model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import java.time.LocalDate;
import java.util.List;
@Entity
public class Order {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    int Id;
    int UserId;
    LocalDate orderdate;
    List<Recipe> Recipes;
    String InvoiceAddress;
    String DeliveryAddress;
    String City;
    int PostalCode;
    double TotalPrice;
    double ItemPrice;
    double DeliveryPrice;
    String Currency;
    boolean IsDelivered;
    String Status;

    public Order() {
    }

    public Order(int userId, LocalDate orderdate, List<Recipe> recipes, String invoiceAddress, String deliveryAddress, String city, int postalCode, double totalPrice, double itemPrice, double deliveryPrice, String currency, boolean isDelivered, String status) {
        UserId = userId;
        this.orderdate = orderdate;
        Recipes = recipes;
        InvoiceAddress = invoiceAddress;
        DeliveryAddress = deliveryAddress;
        City = city;
        PostalCode = postalCode;
        TotalPrice = totalPrice;
        ItemPrice = itemPrice;
        DeliveryPrice = deliveryPrice;
        Currency = currency;
        IsDelivered = isDelivered;
        Status = status;
    }

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public int getUserId() {
        return UserId;
    }

    public void setUserId(int userId) {
        UserId = userId;
    }

    public LocalDate getOrderdate() {
        return orderdate;
    }

    public void setOrderdate(LocalDate orderdate) {
        this.orderdate = orderdate;
    }

    public List<Recipe> getRecipes() {
        return Recipes;
    }

    public void setRecipes(List<Recipe> recipes) {
        Recipes = recipes;
    }

    public String getInvoiceAddress() {
        return InvoiceAddress;
    }

    public void setInvoiceAddress(String invoiceAddress) {
        InvoiceAddress = invoiceAddress;
    }

    public String getDeliveryAddress() {
        return DeliveryAddress;
    }

    public void setDeliveryAddress(String deliveryAddress) {
        DeliveryAddress = deliveryAddress;
    }

    public String getCity() {
        return City;
    }

    public void setCity(String city) {
        City = city;
    }

    public int getPostalCode() {
        return PostalCode;
    }

    public void setPostalCode(int postalCode) {
        PostalCode = postalCode;
    }

    public double getTotalPrice() {
        return TotalPrice;
    }

    public void setTotalPrice(double totalPrice) {
        TotalPrice = totalPrice;
    }

    public double getItemPrice() {
        return ItemPrice;
    }

    public void setItemPrice(double itemPrice) {
        ItemPrice = itemPrice;
    }

    public double getDeliveryPrice() {
        return DeliveryPrice;
    }

    public void setDeliveryPrice(double deliveryPrice) {
        DeliveryPrice = deliveryPrice;
    }

    public String getCurrency() {
        return Currency;
    }

    public void setCurrency(String currency) {
        Currency = currency;
    }

    public boolean isDelivered() {
        return IsDelivered;
    }

    public void setDelivered(boolean delivered) {
        IsDelivered = delivered;
    }

    public String getStatus() {
        return Status;
    }

    public void setStatus(String status) {
        Status = status;
    }

    @Override
    public String toString() {
        return "Order{" +
                "UserId=" + UserId +
                ", orderdate=" + orderdate +
                ", Recipes=" + Recipes +
                ", InvoiceAddress='" + InvoiceAddress + '\'' +
                ", DeliveryAddress='" + DeliveryAddress + '\'' +
                ", City='" + City + '\'' +
                ", PostalCode=" + PostalCode +
                ", TotalPrice=" + TotalPrice +
                ", ItemPrice=" + ItemPrice +
                ", DeliveryPrice=" + DeliveryPrice +
                ", Currency='" + Currency + '\'' +
                ", IsDelivered=" + IsDelivered +
                ", Status='" + Status + '\'' +
                '}';
    }
}

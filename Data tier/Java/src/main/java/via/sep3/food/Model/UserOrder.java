package via.sep3.food.Model;

import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import java.time.LocalDate;
import java.util.Date;


@Entity
public class UserOrder {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    int id;
    int userId;
    Date OrderDate;
    String RecipesIdList;
    String RecipesQuantityList;
    String InvoiceAddress;
    String DeliveryAddress;
    String City;
    int PostalCode;
    double TotalPrice;
    double ItemPrice;
    double DeliveryPrice;
    String Currency;
    boolean IsDelivered;
    String OrderStatus;

    public UserOrder(){
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getUserId() {
        return userId;
    }

    public void setUserId(int userId) {
        this.userId = userId;
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

    public String getOrderStatus() {
        return OrderStatus;
    }

    public void setOrderStatus(String orderStatus) {
        this.OrderStatus = orderStatus;
    }

    public Date getOrderDate() {
        return OrderDate;
    }

    public void setOrderDate(Date orderDate) {
        OrderDate = orderDate;
    }

    public String getRecipesIdList() {
        return RecipesIdList;
    }

    public void setRecipesIdList(String recipesIdList) {
        RecipesIdList = recipesIdList;
    }

    public String getRecipesQuantityList() {
        return RecipesQuantityList;
    }

    public void setRecipesQuantityList(String recipesQuantityList) {
        RecipesQuantityList = recipesQuantityList;
    }

    @Override
    public String toString() {
        return "Order{" +
                "UserId=" + userId +
                ", InvoiceAddress='" + InvoiceAddress + '\'' +
                ", DeliveryAddress='" + DeliveryAddress + '\'' +
                ", City='" + City + '\'' +
                ", PostalCode=" + PostalCode +
                ", TotalPrice=" + TotalPrice +
                ", ItemPrice=" + ItemPrice +
                ", DeliveryPrice=" + DeliveryPrice +
                ", Currency='" + Currency + '\'' +
                ", IsDelivered=" + IsDelivered +
                '}';
    }
}

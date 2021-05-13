package via.sep3.demo.Model;

import org.apache.tomcat.jni.Time;

import java.util.List;

public class Order {
    int order_id;
    Time order_date;
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

    public Order() {
    }

    public Order(int order_id, Time order_date,List<Recipe> recipes, String invoiceAddress, String deliveryAddress, String city, int postalCode, double totalPrice, double itemPrice, double deliveryPrice, String currency, boolean isDelivered) {
        this.order_id = order_id;
        this.order_date=order_date;
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
    }

    public int getOrder_id() {
        return order_id;
    }

    public void setOrder_id(int order_id) {
        this.order_id = order_id;
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

    @Override
    public String toString() {
        return "Order{" +
                "order_id=" + order_id +
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
                '}';
    }
}

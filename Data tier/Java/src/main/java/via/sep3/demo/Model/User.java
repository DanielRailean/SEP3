package via.sep3.demo.Model;

import org.springframework.stereotype.Component;

@Component
public class User {
    int UserId;
    String Email;
    String Password;
    String FirstName;
    String LastName;
    int Phone;
    String Address;
    int PostalCode;

    @Override
    public String toString() {
        return "User{" +
                "UserId=" + UserId +
                ", Email='" + Email + '\'' +
                ", Password='" + Password + '\'' +
                ", FirstName='" + FirstName + '\'' +
                ", LastName='" + LastName + '\'' +
                ", Phone=" + Phone +
                ", Address=" + Address +
                ", PostalCode=" + PostalCode +
                '}';
    }

    public User() {
    }

    public void setEmail(String email) {
        Email = email;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public String getEmail() {

        return Email;
    }

    public String getPassword() {
        return Password;
    }

    public int getUserId() {
        return UserId;
    }

    public void setUserId(int userId) {
        UserId = userId;
    }

    public String getFirstName() {
        return FirstName;
    }

    public void setFirstName(String firstName) {
        FirstName = firstName;
    }

    public String getLastName() {
        return LastName;
    }

    public void setLastName(String lastName) {
        LastName = lastName;
    }

    public int getPhone() {
        return Phone;
    }

    public void setPhone(int phone) {
        Phone = phone;
    }

    public String  getAddress() {
        return Address;
    }

    public void setAddress(String  address) {
        Address = address;
    }

    public int getPostalCode() {
        return PostalCode;
    }

    public void setPostalCode(int postalCode) {
        PostalCode = postalCode;
    }

    public User(int userId, String email, String password, String firstName, String lastName, int phone, String  address, int postalCode) {
        UserId = userId;
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Address = address;
        PostalCode = postalCode;
    }
}

package via.sep3.food.Model;


import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Entity;

@Entity
public class User {

    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private Long id;

    String Email;
    String Password;
    String FirstName;
    String LastName;
    int Phone;
    String Address;
    int PostalCode;
    int SecurityLevel;

    @Override
    public String toString() {
        return "User{" +
                "UserId=" + id +
                ", Email='" + Email + '\'' +
                ", Password='" + Password + '\'' +
                ", FirstName='" + FirstName + '\'' +
                ", LastName='" + LastName + '\'' +
                ", Phone=" + Phone +
                ", Address='" + Address + '\'' +
                ", PostalCode=" + PostalCode +
                ", SecurityLevel=" + SecurityLevel +
                '}';
    }

    public User() {
    }

    public int getSecurityLevel() {
        return SecurityLevel;
    }

    public void setSecurityLevel(int securityLevel) {
        SecurityLevel = securityLevel;
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

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }
}

package via.sep3.demo.Model;

import org.springframework.stereotype.Component;

@Component
public class User {
    String Email;
    String Password;

    @Override
    public String toString() {
        return "User{" +
                "Emai='" + Email + '\'' +
                ", Password='" + Password + '\'' +
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

    public User(String emai, String password) {
        Email = emai;
        Password = password;
    }

}

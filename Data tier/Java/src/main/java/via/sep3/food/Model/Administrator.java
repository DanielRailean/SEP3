package via.sep3.food.Model;

public class Administrator {
    int Id;
     String Email;
     String Password;

    public Administrator() {
    }

    public Administrator(int id,String email, String password) {
        Id=id;
        Email = email;
        Password = password;
    }

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public String getEmail() {
        return Email;
    }

    public void setEmail(String email) {
        Email = email;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    @Override
    public String toString() {
        return "Administrator{" +
                "Id=" + Id +
                ", Email='" + Email + '\'' +
                ", Password='" + Password + '\'' +
                '}';
    }
}

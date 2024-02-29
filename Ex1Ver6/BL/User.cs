namespace Ex1Ver6.BL
{
    public class User
    {
        string firstName;
        string familyName;
        string email;
        string password;
        static List<User> usersList= new List<User>();
        DBServices dbs = new DBServices();

        public User() { }

        public User(string firstName, string familyName, string email, string password)
        {
            FirstName = firstName;
            FamilyName = familyName;
            Email = email;
            Password = password;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string FamilyName { get => familyName; set => familyName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }

        public List<User> Read()
        {
            return dbs.ReadUser();
        }
        public int Insert()
        {
            return dbs.InsertUser(this);
        }

        public int Update(string email, string password)
        {
            return dbs.UpdatePassword(email, password);
        }

        public User Login(string email, string password)
        {
            return dbs.Login(email, password);
        }
    }
}

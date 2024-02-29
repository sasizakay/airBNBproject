using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Ex1Ver6.BL;
using System.Collections;

public class DBServices
{
    public DBServices() { }
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    /// <summary>
    /// This method reads flats from the flats table.
    /// </summary>
    /// <returns>List of all the flats from the database.</returns>
    public List<Flat> ReadFlat()
    {

        SqlConnection con;
        SqlCommand cmd;
        List<Flat> flatsList = new List<Flat>();

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureWithoutParameters("sp_getFlats", con);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Flat f = new Flat();
                f.Id = dataReader["id"].ToString();
                f.City = dataReader["city"].ToString();
                f.Address = dataReader["address"].ToString();
                f.Price = Convert.ToDouble(dataReader["price"]);
                f.NumberOfRooms = Convert.ToInt32(dataReader["numOfRooms"]);
                flatsList.Add(f);
            }
            return flatsList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    /// <summary>
    /// This methos inserts a flat to the flats table.
    /// </summary>
    /// <param name="flat"></param>
    /// <returns>Number of rows affected.</returns>
    public int InsertFlat(Flat flat)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateFlatInsertCommandWithStoredProcedure("sp_insertFlat", con, flat);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    /// <summary>
    /// Get flats by city and price
    /// </summary>
    /// <param name="city"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    public List<Flat> GetByCityAndPrice(string city, double price)
    {
        SqlConnection con;
        SqlCommand cmd;
        List<Flat> flatsList = new List<Flat>();

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateFlatReadCommandWithStoredProcedure("sp_getFlatByCityAndPrice", con, city, price);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Flat f = new Flat();
                f.Id = dataReader["id"].ToString();
                f.City = dataReader["city"].ToString();
                f.Address = dataReader["address"].ToString();
                f.Price = Convert.ToDouble(dataReader["price"]);
                f.NumberOfRooms = Convert.ToInt32(dataReader["numOfRooms"]);
                flatsList.Add(f);
            }
            return flatsList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    
    private SqlCommand CreateFlatInsertCommandWithStoredProcedure(String spName, SqlConnection con, Flat flat)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@id", flat.Id);
        cmd.Parameters.AddWithValue("@city", flat.City);
        cmd.Parameters.AddWithValue("@address", flat.Address);
        cmd.Parameters.AddWithValue("@price", flat.Price);
        cmd.Parameters.AddWithValue("@numOfRooms", flat.NumberOfRooms);
        return cmd;
    }

    private SqlCommand CreateFlatReadCommandWithStoredProcedure(String spName, SqlConnection con, string city, double price)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@city", city);
        
        cmd.Parameters.AddWithValue("@price", price);
        
        return cmd;
    }

    /// <summary>
    /// This method reads vacations from the vacations table.
    /// </summary>
    /// <returns>List of all the flats from the database.</returns>
    public List<Vacation> ReadVacation()
    {

        SqlConnection con;
        SqlCommand cmd;
        List<Vacation> vacationsList = new List<Vacation>();

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureWithoutParameters("sp_getVacations", con);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Vacation v = new Vacation();
                v.Id = dataReader["id"].ToString();
                v.UserId = dataReader["UserId"].ToString();
                v.FlatId = dataReader["FlatId"].ToString();
                v.StartDate =Convert.ToDateTime(dataReader["StartDate"]);
                v.EndDate = Convert.ToDateTime(dataReader["EndDate"]);
                vacationsList.Add(v);
            }
            return vacationsList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    /// <summary>
    /// This methos inserts a vacation to the vacations table.
    /// </summary>
    /// <param name="vacation"></param>
    /// <returns>Number of rows affected.</returns>
    public int InsertVacation(Vacation vacation)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateVacationInsertCommandWithStoredProcedure("sp_insertVacation", con, vacation);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    /// <summary>
    /// Get vacations by start date and end date
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public List<Vacation> getVacationsByDates(DateTime start, DateTime end)
    {
        SqlConnection con;
        SqlCommand cmd;
        List<Vacation> vacationsList = new List<Vacation>();

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateVacationReadCommandWithStoredProcedureWithParameters("sp_getVacationsByDates", con, start, end);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Vacation v = new Vacation();
                v.Id = dataReader["id"].ToString();
                v.UserId = dataReader["UserId"].ToString();
                v.FlatId = dataReader["FlatId"].ToString();
                v.StartDate = Convert.ToDateTime(dataReader["StartDate"]);
                v.EndDate = Convert.ToDateTime(dataReader["EndDate"]);
                vacationsList.Add(v);
            }
            return vacationsList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    private SqlCommand CreateVacationInsertCommandWithStoredProcedure(String spName, SqlConnection con, Vacation vacation)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@id", vacation.Id);
        cmd.Parameters.AddWithValue("@userId", vacation.UserId);
        cmd.Parameters.AddWithValue("@flatId", vacation.FlatId);
        cmd.Parameters.AddWithValue("@startDate", vacation.StartDate);
        cmd.Parameters.AddWithValue("@endDate", vacation.EndDate);
        return cmd;
    }

    private SqlCommand CreateVacationReadCommandWithStoredProcedureWithParameters(String spName, SqlConnection con, DateTime start, DateTime end)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@startDate", start);

        cmd.Parameters.AddWithValue("@endDate", end);

        return cmd;
    }

    public List<User> ReadUser()
    {

        SqlConnection con;
        SqlCommand cmd;
        List<User> usersList = new List<User>();

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureWithoutParameters("sp_getUsers", con);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                User u = new User();
                u.FirstName = dataReader["firstName"].ToString();
                u.FamilyName = dataReader["familyName"].ToString();
                u.Email = dataReader["email"].ToString();
                u.Password = dataReader["password"].ToString();
                usersList.Add(u);
            }
            return usersList;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    public int InsertUser(User user)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUserInsertCommandWithStoredProcedure("sp_insertUser", con, user);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    public int UpdatePassword(string email, string password)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUserUpdateCommandWithStoredProcedureWithParameters("sp_updatePassword", con, email, password);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    public User Login(string email, string password)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUserUpdateCommandWithStoredProcedureWithParameters("sp_loginUser", con, email, password);             // create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            dataReader.Read();
                User u = new User();
                u.FirstName = dataReader["firstName"].ToString();
                u.FamilyName = dataReader["familyName"].ToString();
                u.Email = dataReader["email"].ToString();
                u.Password = dataReader["password"].ToString();

            return u;
        }
        catch (Exception ex)
        {
            throw new HttpRequestException("Not Found", null, System.Net.HttpStatusCode.NotFound);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    private SqlCommand CreateUserInsertCommandWithStoredProcedure(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
        cmd.Parameters.AddWithValue("@familyName", user.FamilyName);
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", user.Password);
        return cmd;
    }

    private SqlCommand CreateUserUpdateCommandWithStoredProcedureWithParameters(String spName, SqlConnection con, string email, string password)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@email", email);

        cmd.Parameters.AddWithValue("@password", password);

        return cmd;
    }
    private SqlCommand CreateCommandWithStoredProcedureWithoutParameters(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }
}

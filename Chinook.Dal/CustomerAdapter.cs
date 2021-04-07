using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;

namespace Chinook.Dal
{
    public class CustomerAdapter
    {
        private string _connectionString = @"Data Source= Chinook_Sqlite_AutoIncrementPKs.sqlite; datetimeformat=CurrentCulture;";

        public List<Customer> GetAll()
        {
            // Declare the return type
            List<Customer> returnValue = new List<Customer>();
            // Create a connection to SQL lite. Wrap in a using statement for safety
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                // Create the commamd
                SQLiteCommand command = connection.CreateCommand();
                // Pass the CommandText to the command
                command.CommandText = "SELECT CustomerId, FirstName, LastName, Country, Email FROM Customer";
                // Open the database connection
                connection.Open();
                // Execute a command and return back a reader
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Call a method to retrieve the results
                    Customer customer = GetFromReader(reader);
                    // add the instance to the return list
                    returnValue.Add(customer);
                }
                // return back the results
                return returnValue;
            }
        }

        public Customer GetById(int customerId)
        {
            Customer returnValue = null;
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                // Create the command
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT CustomerId, FirstName, LastName, Country, Email FROM Customer WHERE CustomerId = " + customerId.ToString();
              connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    returnValue = GetFromReader(reader);
                }

                return returnValue;
            }
        }

        private Customer GetFromReader(DbDataReader reader)
        {
            // Create a new instance of the customer class
            Customer customer = new Customer();
            // Copy the data that you retrieve from the database into the class
            customer.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));
            customer.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
            customer.LastName = reader.GetString(reader.GetOrdinal("LastName"));
            customer.Country = reader.GetString(reader.GetOrdinal("Country"));
            customer.Email = reader.GetString(reader.GetOrdinal("Email"));
            return customer;
        }
        public bool InsertCustomer(Customer customer)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Customer (FirstName, LastName, Country, Email) VALUES ('" + customer.FirstName + "', '" + customer.LastName + "', '" + customer.Country + "', '" + customer.Email + "'); ";
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool UpdateCustomer(Customer customer)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Customer SET FirstName = '" + customer.FirstName + "', LastName = '" + customer.LastName + "', Country = '" + customer.Country + "', Email = '" + customer.Email + "' WHERE CustomerId = " + customer.CustomerId;
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool DeleteCustomer(int customerId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Customer WHERE CustomerId = " + customerId;
                connection.Open();
                int rows = command.ExecuteNonQuery();
                if (rows > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

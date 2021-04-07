using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Data.Common;

namespace Chinook.Dal
{
    public class InvoiceAdapter
    {
        private string _connectionString = @"Data Source= Chinook_Sqlite_AutoIncrementPKs.sqlite;datetimeformat=CurrentCulture;";

        public List<Invoice> GetAll()
        {
            // Declare the return type
            List<Invoice> returnValue = new List<Invoice>();
            // Create a connection to SQL lite. Wrap in a using statement for safety
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                // Create the commamd
                SQLiteCommand command = connection.CreateCommand();
                // Pass the CommandText to the command
                command.CommandText = "SELECT InvoiceId, CustomerId, Total, InvoiceDate FROM Invoice";
                // Open the database connection
                connection.Open();
                // Execute a command and return back a reader
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Call a method to retrieve the results
                    Invoice customer = GetFromReader(reader);
                    // add the instance to the return list
                    returnValue.Add(customer);
                }
                // return back the results
                return returnValue;
            }
        }

        public List<Invoice> GetByCustomerId(int customerId)
        {
            List<Invoice> returnValue = new List<Invoice>();
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                // Create the command
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT InvoiceId, CustomerId, Total, InvoiceDate FROM Invoice WHERE CustomerId = " + customerId.ToString();
              connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Invoice invoice = GetFromReader(reader);
                    returnValue.Add(invoice);
                }

                return returnValue;
            }
        }

        public Invoice GetById(int invoiceId)
        {
            Invoice returnValue = null;
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                // Create the command
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "SELECT InvoiceId, CustomerId, Total, InvoiceDate FROM Invoice WHERE InvoiceId = " + invoiceId.ToString();
              connection.Open();
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    returnValue = GetFromReader(reader);
                }

                return returnValue;
            }
        }

        public bool InsertInvoice(Invoice invoice)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Invoice (CustomerId, Total, InvoiceDate) VALUES ("+ invoice.CustomerId + "," + invoice.Total + ",'" + invoice.InvoiceDate + "')";
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

        public bool UpdateInvoice(Invoice invoice)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Invoice SET Total = " + invoice.Total + ", InvoiceDate = '"+ invoice.InvoiceDate + "' WHERE InvoiceId = " + invoice.InvoiceId;
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

        public bool DeleteInvoice(int invoiceId)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = connection.CreateCommand();
                command.CommandText = "DELETE FROM Invoice WHERE InvoiceId = " + invoiceId;
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

        private Invoice GetFromReader(DbDataReader reader)
        {
            // Create a new instance of the customer class
            Invoice invoice = new Invoice();
            // Copy the data that you retrieve from the database into the class
            invoice.CustomerId = reader.GetInt32(reader.GetOrdinal("CustomerId"));
            invoice.InvoiceId = reader.GetInt32(reader.GetOrdinal("InvoiceId"));
            invoice.Total = reader.GetDecimal(reader.GetOrdinal("Total"));
            invoice.InvoiceDate = reader.GetDateTime(reader.GetOrdinal("InvoiceDate"));
            return invoice;
        }
    }
}

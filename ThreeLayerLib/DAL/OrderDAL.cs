using Persistence;
using MySqlConnector;
namespace DAL
{
    public class OrderDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        public Order GetOrderByID(int orderId)
        {
            Order order = new Order();
            try
            {
                string query = @"SELECT o.order_ID, o.order_date, c.customer_name, c.phoneNumber, c.customer_address, b.book_name, b.price, od.quantity
                    FROM Orders o
                    INNER JOIN Customers c ON o.customer_ID = c.customer_ID
                    INNER JOIN OrderDetails od ON o.order_ID = od.order_ID
                    INNER JOIN Books b ON b.Book_ID = od.Book_ID
                    WHERE o.order_ID = @orderId;";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                MySqlDataReader reader = command.ExecuteReader();

                List<Book> booksList = new List<Book>();

                while (reader.Read())
                {
                    Book book = new Book();
                    book.BookName = reader.GetString("book_name");
                    book.Price = reader.GetDecimal("price");
                    book.Amount = reader.GetInt32("quantity");
                    booksList.Add(book);

                    if (order.OrderID == 0)
                    {
                        order = GetOrder(reader);
                    }
                }
                reader.Close();

                order.BooksList = booksList;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return order;
        }

        internal Order GetOrder(MySqlDataReader reader)
        {
            Order o = new Order();
            o.OrderID = reader.GetInt32("order_ID");
            o.OrderDate = reader.GetDateTime("order_date");
            o.OrderCustomer = new Customer();
            o.OrderCustomer.CustomerName = reader.GetString("customer_name");
            o.OrderCustomer.PhoneNumber = reader.GetString("phoneNumber");
            o.OrderCustomer.CustomerAddress = reader.GetString("customer_address");
            o.BooksList = new List<Book>();
            Book book = new Book();
            book.BookName = reader.GetString("book_name");
            book.Price = reader.GetDecimal("price");
            book.Amount = reader.GetInt32("quantity");
            o.BooksList.Add(book);
            return o;
        }



        public bool CreateOrder(Order order)
        {

            bool result = false;
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();

                using (MySqlTransaction trans = connection.BeginTransaction())
                using (MySqlCommand cmd = connection.CreateCommand())
                {
                    cmd.Transaction = trans;
                    cmd.CommandText = "lock tables Customers write, Orders write, Books write, OrderDetails write;";
                    cmd.ExecuteNonQuery();

                    try
                    {
                        if (order.OrderCustomer == null || string.IsNullOrEmpty(order.OrderCustomer.CustomerName))
                        {
                            order.OrderCustomer = new Customer();
                        }

                        if (order.OrderCustomer.CustomerID == 0)
                        {
                            // Insert new Customer
                            cmd.CommandText = @"INSERT INTO Customers (customer_name, phoneNumber, customer_address)
                                            VALUES (@customerName, @phoneNumber, @customerAddress);";

                            cmd.Parameters.AddWithValue("@customerName", order.OrderCustomer.CustomerName);
                            cmd.Parameters.AddWithValue("@phoneNumber", order.OrderCustomer.PhoneNumber);
                            cmd.Parameters.AddWithValue("@customerAddress", order.OrderCustomer.CustomerAddress);

                            cmd.ExecuteNonQuery();

                            // Get new customer id
                            cmd.CommandText = "SELECT LAST_INSERT_ID() AS customer_ID;";
                            order.OrderCustomer.CustomerID = Convert.ToInt32(cmd.ExecuteScalar());
                        }
                        else
                        {
                            // Get Customer by Id
                            cmd.CommandText = "SELECT * FROM Customers WHERE customer_ID = @customerId;";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@customerId", order.OrderCustomer.CustomerID);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    order.OrderCustomer = new CustomerDAL().GetCustomer(reader);
                                }
                            }
                        }

                        if (order.OrderCustomer == null || order.OrderCustomer.CustomerID == 0)
                        {
                            throw new Exception("Can't find Customer!");
                        }

                        if (order.OrderStaff == null || string.IsNullOrEmpty(order.OrderStaff.StaffName))
                        {
                            order.OrderStaff = new Staff();
                        }

                        // Insert Order
                        cmd.CommandText = "INSERT INTO Orders (staff_ID, customer_ID, order_status) VALUES (@staffId, @customerId, @orderStatus);";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@staffId", order.OrderStaff.StaffID);
                        cmd.Parameters.AddWithValue("@customerId", order.OrderCustomer.CustomerID);
                        cmd.Parameters.AddWithValue("@orderStatus", OrderStatus.CREATE_NEW_ORDER);
                        cmd.ExecuteNonQuery();

                        // Get new Order_ID
                        cmd.CommandText = "SELECT LAST_INSERT_ID() AS order_id;";
                        order.OrderID = Convert.ToInt32(cmd.ExecuteScalar());

                        // Insert Order Details table
                        foreach (Book book in order.BooksList)
                        {
                            if (book.BookID == 0 || book.Amount <= 0)
                            {
                                throw new Exception("Not Exists Book");
                            }

                            // Get unit_price
                            cmd.CommandText = "SELECT price FROM Books WHERE book_ID = @bookId;";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@bookId", book.BookID);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    book.Price = reader.GetDecimal("price");
                                }
                                else
                                {
                                    throw new Exception("Not Exists Book");
                                }
                            }

                            // Insert to Order Details
                            cmd.CommandText = @"INSERT INTO OrderDetails (order_ID, book_ID, unit_price, quantity) VALUES 
                                            (@orderId, @bookId, @unitPrice, @quantity);";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@orderId", order.OrderID);
                            cmd.Parameters.AddWithValue("@bookId", book.BookID);
                            cmd.Parameters.AddWithValue("@unitPrice", book.Price);
                            cmd.Parameters.AddWithValue("@quantity", book.Amount);
                            cmd.ExecuteNonQuery();

                            // Update amount in Books
                            cmd.CommandText = "UPDATE Books SET amount = amount - @quantity WHERE book_ID = @bookId;";
                            cmd.ExecuteNonQuery();
                        }

                        // Commit transaction
                        trans.Commit();
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"ERROR: {ex.Message}");
                        try
                        {
                            trans.Rollback();
                        }
                        catch { }
                    }
                    finally
                    {
                        // Unlock all tables
                        cmd.CommandText = "UNLOCK TABLES;";
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }

            return result;
        }
    }
}

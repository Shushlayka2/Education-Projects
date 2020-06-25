using System;
using System.Data.Common;
using System.Data.SQLite;

namespace ForYandex
{
    static class DB
    {
        const string databaseName = @"..\..\MainDB.db";
        static SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0}", databaseName));
        static SQLiteCommand command = new SQLiteCommand(connection);

        static internal void CreateTables()
        {
            connection.Open();
            command.CommandText = @"CREATE TABLE [Orders] (
            [id] integer PRIMARY KEY NOT NULL,
            [dt] integer NOT NULL,
            [product_id] integer NOT NULL,
            [amount] real
            );";
            command.ExecuteNonQuery();

            command.CommandText = @"CREATE TABLE [Products] (
            [id] integer PRIMARY KEY NOT NULL,
            [name] text
            );";
            command.ExecuteNonQuery();

            connection.Close();
        }

        static internal void FillTheProductTable()
        {
            connection.Open();
            for (int i = 1; i <= 7; i++)
            {
                command.CommandText = string.Format("INSERT INTO 'Products' ('id', 'name') VALUES ({0}, '{1}');", i, (char)(i + 64));
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        static internal void FillTheOrderTable(string filePath)
        {
            connection.Open();
            Order ord = new Order(filePath);
            int currentRowNumber = 1;
            bool isComplete = true;
            while (isComplete)
            {
                ord = null;
                ord = Order.GiveNextOrder(currentRowNumber, out isComplete);
                if (ord != null)
                {
                    command.CommandText = string.Format("INSERT OR IGNORE INTO 'Orders' ('id', 'dt', 'product_id', 'amount') VALUES ('{0}', strftime('%s','{1}'), '{2}', '{3}');", ord.Id, ord.Dt, ord.Product_id, ord.Amount);
                    command.ExecuteNonQuery();
                }
                currentRowNumber++;
            }
            connection.Close();
        }

        static internal void ExecuteFirstQuery()
        {
            Console.WriteLine("\nПродукт\tКоличество\tСумма");
            connection.Open();
            command.CommandText = @"select name, count(*), sum(amount)
                                    from (Orders ord join Products pr
                                    on ord.product_id = pr.id)
                                    where strftime('%m-%Y', dt, 'unixepoch') = strftime('%m-%Y','now')
                                    group by name";
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                string result = record["name"].ToString() + "\t" + record["count(*)"].ToString() + "\t\t" + record["sum(amount)"].ToString();
                Console.WriteLine(result);
            }

            reader.Close();
            connection.Close();
        }

        static internal void ExecuteSecondQuery()
        {
            Console.WriteLine("\nПродукты");
            connection.Open();
            command.CommandText = @"select name
                                    from (Orders ord join Products pr
                                    on ord.product_id = pr.id)
                                    group by name
                                    having strftime('%m-%Y', dt, 'unixepoch') = strftime('%m-%Y','now') and strftime('%m-%Y', dt, 'unixepoch') != strftime('%m-%Y','now','-1 month')";
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                string result = record["name"].ToString();
                Console.WriteLine(result);
            }

            reader.Close();
            connection.Close();
        }

        static internal void ExecuteThirdQuery()
        {
            Console.WriteLine("\nВ прошлом");
            connection.Open();
            command.CommandText = @"select name
                                    from (Orders ord join Products pr
                                    on ord.product_id = pr.id)
                                    group by name
                                    having strftime('%m-%Y', dt, 'unixepoch') = strftime('%m-%Y','now','-1 month') and strftime('%m-%Y', dt, 'unixepoch') != strftime('%m-%Y','now')";
            SQLiteDataReader reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                string result = record["name"].ToString();
                Console.WriteLine(result);
            }

            reader.Close();

            Console.WriteLine("\nВ текущем");
            command.CommandText = @"select name
                                    from (Orders ord join Products pr
                                    on ord.product_id = pr.id)
                                    group by name
                                    having strftime('%m-%Y', dt, 'unixepoch') = strftime('%m-%Y','now') and strftime('%m-%Y', dt, 'unixepoch') != strftime('%m-%Y','now','-1 month')";
            reader = command.ExecuteReader();
            foreach (DbDataRecord record in reader)
            {
                string result = record["name"].ToString();
                Console.WriteLine(result);
            }

            reader.Close();
            connection.Close();
        }

        static internal void ExecuteFourthQuery()
        {
            Console.WriteLine("\nПериод\tПродукт\tСумма\tДоля");
            connection.Open();
            command.CommandText = @"select strftime('%m-%Y', dt, 'unixepoch'), name, max(amount), max(amount)/sum(amount)*100
                                    from (Orders ord join Products pr
                                    on ord.product_id = pr.id)
                                    group by strftime('%m-%Y', dt, 'unixepoch')";
            SQLiteDataReader reader = command.ExecuteReader();

            foreach (DbDataRecord record in reader)
            {
                string result = record["strftime('%m-%Y', dt, 'unixepoch')"].ToString() + "\t" + record["name"].ToString() + "\t" + record["max(amount)"].ToString() + "\t" + record["max(amount)/sum(amount)*100"].ToString();
                Console.WriteLine(result);
            }

            reader.Close();
            connection.Close();
        }
    }
}

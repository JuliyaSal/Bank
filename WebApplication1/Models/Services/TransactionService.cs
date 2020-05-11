using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models.Services
{
    public class TransactionService
    {
        private string connectionString = "Server=localhost;Database=bank;User Id=postgres;Password=456rtyrty;Pooling=false";


        public void AddFunds(Transaction transaction)
        {
            using (IDbConnection db = CreateConnection())
            {
                db.Open();
                var sqlQuery = @"INSERT INTO transactions
                             (source, destination, amount, transactionDate) 
                             VALUES(@source, @destination, (@amount)::money, @transactionDate);";
                db.Execute(sqlQuery, transaction);

                var sqlQuery2 = @"update accounts
                                set balance = balance+(@amount)::money
                                where accountNumber = @destination";
                db.Execute(sqlQuery2, new { transaction.Amount, transaction.Destination});
                db.Close();
            }
        }

        public void MoveFunds(Transaction transaction)
        {
            using (IDbConnection db = CreateConnection())
            {
                db.Open();
                var sqlQuery = @"INSERT INTO transactions
                             (source, destination, amount, transactionDate) 
                             VALUES(@source, @destination, (@amount)::money, @transactionDate);";
                db.Execute(sqlQuery, transaction);

                var sqlQuery3 = @"update accounts
                                set balance = balance-(@amount)::money
                                where accountNumber = @source";
                db.Execute(sqlQuery3, new { transaction.Amount, transaction.Source });

                var sqlQuery2 = @"update accounts
                                set balance = balance+(@amount)::money
                                where accountNumber = @destination";
                db.Execute(sqlQuery2, new { transaction.Amount, transaction.Destination });
                db.Close();
            }
        }

        public List<Account> GetBalance(int userId)
        {
            using (IDbConnection db = CreateConnection())
            {
                db.Open();
                var accounts = db.Query<Account>(@"SELECT *
                                                FROM accounts
                                                WHERE userId = @userId;", new { userId }).ToList();
                db.Close();
                return accounts;
            }
        }
        //public List<Transaction> GetTransactions(int UserId)
        //{
        //    using (IDbConnection db = CreateConnection())
        //    {
        //        db.Open();
        //        var user = db.Query<User>(@"SELECT source, destination, amount, transaction, type
        //                                FROM transactions
        //                                WHERE id = @id;", new { id }).FirstOrDefault();
        //        db.Close();
        //        return List<Transaction>().;
        //    }
        //}
        private NpgsqlConnection CreateConnection()
        {
            var connection = new NpgsqlConnection(connectionString);

            return connection;
        }
    }
}

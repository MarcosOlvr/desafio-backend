using desafio_backend.Data;
using desafio_backend.Models;
using desafio_backend.Repository.Contracts;

namespace desafio_backend.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _db;

        public TransactionRepository(AppDbContext db) 
        {
            _db = db;
        }

        public Transaction AddTransaction(Transaction transaction)
        {
            var payer = _db.Users.FirstOrDefault(p => p.Id == transaction.Payer);
            var payee = _db.Users.FirstOrDefault(p => p.Id == transaction.Payee);

            if (payer == null || payee == null)
                throw new Exception("Payer or payee is null");

            if (payer.UserType.ToString() == "MERCHANT")
                throw new Exception($"Merchants can't transfer money {payee.UserType.ToString()} and {payer.UserType.ToString()}"); // REVISAR O ENGLISH OF STREETS MY FRIEND

            _db.Transactions.Add(transaction);
            _db.SaveChanges();

            AddValue(payee.Id, transaction.Value);
            RmValue(payer.Id, transaction.Value);

            return transaction;
        }

        public void DeleteTransaction(int id)
        {
            var transaction = _db.Transactions.FirstOrDefault(t => t.Id == id);
            if (transaction != null)
            {
                _db.Transactions.Remove(transaction);
                _db.SaveChanges();  
            }
        }

        public Transaction GetTransaction(int id)
        {
            var transaction = _db.Transactions.FirstOrDefault(t => t.Id == id);

            return transaction;
        }

        public Transaction UpdateTransaction(int id, Transaction transaction)
        {
            var transactionById = _db.Transactions.FirstOrDefault(t => t.Id == id);

            if (transactionById != null)
            {
                var payer = _db.Users.FirstOrDefault(p => p.Id == transaction.Payer);
                var payee = _db.Users.FirstOrDefault(p => p.Id == transaction.Payee);

                if (payer == null || payer == null)
                    throw new Exception("Payer or payee is null");

                if (payer.UserType.ToString() == "MERCHANT")
                    throw new Exception("Merchants can't transfer money"); // REVISAR O ENGLISH OF STREETS MY FRIEND

                transactionById.Payer = transaction.Payer;
                transactionById.Payee = transaction.Payee;
                transactionById.Value = transaction.Value;

                _db.Transactions.Update(transactionById);
                _db.SaveChanges();

                return transactionById;
            }

            throw new Exception("Update deu ruim!");
        }

        public void AddValue(int userId, decimal value)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == userId);

            if (user != null)
            {
                user.Balance += value;

                _db.Users.Update(user);
                _db.SaveChanges();
            }
        }

        public void RmValue(int userId, decimal value)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == userId);

            if (user != null)
            {
                user.Balance -= value;

                _db.Users.Update(user);
                _db.SaveChanges();
            }
        }
    }
}

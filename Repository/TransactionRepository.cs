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

        public List<Transaction> GetAllTransactions()
        {
            List<Transaction> transactions = _db.Transactions.ToList();

            return transactions;
        }

        public Transaction AddTransaction(Transaction transaction)
        {
            var response = AuthorizeTransaction();

            if (!response.Result)
                throw new Exception("Transaction was not authorized");


            TransferValue(transaction.Payer, transaction.Value);
            ReceiveValue(transaction.Payee, transaction.Value);

            _db.Transactions.Add(transaction);
            _db.SaveChanges();

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

                transactionById.Payer = transaction.Payer;
                transactionById.Payee = transaction.Payee;
                transactionById.Value = transaction.Value;

                _db.Transactions.Update(transactionById);
                _db.SaveChanges();

                return transactionById;
            }

            throw new Exception("Update error");
        }

        public void ReceiveValue(int userId, decimal value)
        {
            try
            {
                var user = _db.Users.FirstOrDefault(x => x.Id == userId);

                if (user != null)
                {
                    user.Balance += value;

                    _db.Users.Update(user);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void TransferValue(int userId, decimal value)
        {
            try
            {
                var payer = _db.Users.FirstOrDefault(x => x.Id == userId);

                if (payer != null)
                {
                    if (payer.UserType.ToString() == "MERCHANT")
                        throw new Exception($"Merchants can't transfer money");

                    if (payer.Balance < value)
                        throw new Exception("Insufficient funds");

                    payer.Balance -= value;

                    _db.Users.Update(payer);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AuthorizeTransaction()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://run.mocky.io/v3/8fafdd68-a090-496f-8c9a-3442cf30dae6");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            if (responseBody.Contains("Autorizado"))
            {
                return true;
            }

            return false;
        }
    }
}

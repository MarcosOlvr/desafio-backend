using desafio_backend.Models;

namespace desafio_backend.Repository.Contracts
{
    public interface ITransactionRepository
    {
        void TransferValue(int userId, decimal value);
        void ReceiveValue(int userId, decimal value);
        List<Transaction> GetAllTransactions();
        Transaction GetTransaction(int id);
        Transaction AddTransaction(Transaction transaction);
        Transaction UpdateTransaction(int id, Transaction transaction);
        void DeleteTransaction(int id);
    }
}

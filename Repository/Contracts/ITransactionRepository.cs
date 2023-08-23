using desafio_backend.Models;

namespace desafio_backend.Repository.Contracts
{
    public interface ITransactionRepository
    {
        void AddValue(int userId, decimal value);
        void RmValue(int userId, decimal value);
        Transaction GetTransaction(int id);
        Transaction AddTransaction(Transaction transaction);
        Transaction UpdateTransaction(int id, Transaction transaction);
        void DeleteTransaction(int id);
        Task<bool> AuthorizeTransaction();
    }
}

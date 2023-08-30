using desafio_backend.Models;
using desafio_backend.Models.DTO;

namespace desafio_backend.Repository.Contracts
{
    public interface ITransactionRepository
    {
        List<TransactionDTO> GetAllTransactions();
        Transaction GetTransaction(int id);
        Transaction AddTransaction(Transaction transaction);
        Transaction UpdateTransaction(int id, Transaction transaction);
        void DeleteTransaction(int id);
    }
}

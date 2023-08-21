namespace desafio_backend.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public int Payer { get; set; }
        public int Payee { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

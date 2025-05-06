namespace Backend.Interfaces.IBalance
{
    public interface IBalanceRepository
    {

        // Få saldo på bruger
        Task<decimal> GetBalanceAsync(int userId);

        // User Deposit and User Withdraw
        Task UpdateBalanceAsync(int userId, decimal newBalance);

    }
}

namespace Backend.Interfaces.IBalance
{
    public interface IBalanceService
    {

        // Få saldo på bruger
        Task<decimal> GetBalanceAsync(int userId);

        // User Deposit
        Task<decimal> DepositAsync(int userId, decimal amount);

        // bruger udbetaler
        Task<decimal> WithdrawAsync(int userId, decimal amount);

        // Spillebeløb
        Task<decimal> PlaceBetAsync(int userId, decimal amount);

        // Præmie indsætning
        Task<decimal> WinAmountAsync(int userId, decimal amount);

    }
}

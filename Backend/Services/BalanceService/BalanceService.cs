using Backend.Interfaces.IBalance;

namespace Backend.Services.BalanceService
{
    public class BalanceService : IBalanceService
    {

        private readonly IBalanceRepository _balanceRepository;

        public BalanceService(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }



        public async Task<decimal> GetBalanceAsync(int userId)
        {
            var currentBalance = await _balanceRepository.GetBalanceAsync(userId);

            return currentBalance;
        }



        public async Task<decimal> DepositAsync(int userId, decimal amount)
        {
            if (amount < 50) throw new ArgumentException(ErrorText("deposit"));

            var currentBalance = await _balanceRepository.GetBalanceAsync(userId);
            var newBalance = currentBalance + amount;

            await _balanceRepository.UpdateBalanceAsync(userId, newBalance);
            return newBalance;

        }



        public async Task<decimal> WithdrawAsync(int userId, decimal amount)
        {

            if (amount < 50) throw new ArgumentException(ErrorText("withdraw"));


            var currentBalance = await _balanceRepository.GetBalanceAsync(userId);
            if (currentBalance < amount) throw new InvalidOperationException(ErrorText("insufficient funds"));
       
            var newBalance = currentBalance - amount;

            await _balanceRepository.UpdateBalanceAsync(userId, newBalance);
            return newBalance;
        }



        public async Task<decimal> PlaceBetAsync(int userId, decimal amount)
        {
            CheckForAmount(amount, "bet");

            var currentBalance = await _balanceRepository.GetBalanceAsync(userId);
            if (currentBalance < amount) throw new InvalidOperationException(ErrorText("insufficient funds"));

            var newBalance = currentBalance - amount;

            await _balanceRepository.UpdateBalanceAsync(userId, newBalance);
            return newBalance;
        }



        public async Task<decimal> WinAmountAsync(int userId, decimal amount)
        {
            CheckForAmount(amount, "win");

            var currentBalance = await _balanceRepository.GetBalanceAsync(userId);
            var newBalance = currentBalance + amount;

            await _balanceRepository.UpdateBalanceAsync(userId, newBalance);
            return newBalance;
        }


        private bool CheckForAmount(decimal amount, string state)
        {
            if (amount <= 0) throw new ArgumentException(ErrorText(state));
            return true;
        }


        private string ErrorText(string state)
        {
            if (state == "deposit") return "Minimuns beløb er 50 kroner.";
            else if (state == "withdraw") return "Beløbet skal være højere end 0.";
            else if (state == "insufficient funds") return "Ikke tilstrækkelig beløb på kontoen.";
            else if (state == "bet") return "Spillebeløbet skal være højere end 0.";
            else if (state == "win") return "Gevinstbeløb skal være højere end 0.";

            return "Unknown error";
        }

    }
}

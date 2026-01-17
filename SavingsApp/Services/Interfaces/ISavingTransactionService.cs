public interface ISavingTransactionService
{
    Task AddTransactionAsync(AddSavingTransactionDto dto);
    Task WithdrawAsync(WithdrawDto dto);
}

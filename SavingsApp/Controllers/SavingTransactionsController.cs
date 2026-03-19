using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/saving-transactions")]
public class SavingTransactionsController : ControllerBase
{
    private readonly ISavingTransactionService _service;

    public SavingTransactionsController(ISavingTransactionService service)
    {
        _service = service;
    }

    /// <summary>
    /// Add funds to a saving goal
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddSavingTransactionDto dto)
    {
        await _service.AddTransactionAsync(dto);

        return Ok(new
        {
            message = "Amount added successfully"
        });
    }

    /// <summary>
    /// Withdraw funds from a saving goal
    /// </summary>
    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] WithdrawDto dto)
    {
        await _service.WithdrawAsync(dto);

        return Ok(new
        {
            message = "Withdraw completed successfully"
        });
    }
}

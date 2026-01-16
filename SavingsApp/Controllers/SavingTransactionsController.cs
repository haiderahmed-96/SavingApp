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

    [HttpPost]
    public async Task<IActionResult> Add(AddSavingTransactionDto dto)
    {
        try
        {
            await _service.AddTransactionAsync(dto);
            return Ok(new { message = "Amount added successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}

namespace AppointmentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController : ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpGet("{patientId}")]
    public async Task<IActionResult> GetBasket(string patientId)
    {
        var basket = await _basketService.GetBasketByPatientIdAsync(patientId);
        if (basket == null)
            return NotFound(new { Message = "Basket not found." });

        return Ok(basket);
    }


    [HttpPost("{patientId}/items")]
    public async Task<IActionResult> AddItem(string patientId, [FromBody] AddBasketItemDto itemDto)
    {
        var basket = await _basketService.AddItemToBasketAsync(
            patientId, itemDto.DoctorId, itemDto.AvailabilityId, itemDto.MedicalServiceId);

        return Ok(basket);
    }

    [HttpDelete("{patientId}/items/{itemId}")]
    public async Task<IActionResult> RemoveItem(string patientId, string itemId)
    {
        var basket = await _basketService.RemoveItemFromBasketAsync(patientId, itemId);
        return Ok(basket);
    }


    [HttpPost("{patientId}/checkout")]
    public async Task<IActionResult> Checkout(string patientId, [FromQuery] string? promoCode = null)
    {
        try
        {

            decimal totalPrice = await _basketService.CheckoutAsync(patientId, promoCode);

            return Ok(new
            {
                Message = "Checkout completed successfully.",
                TotalPrice = totalPrice
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}

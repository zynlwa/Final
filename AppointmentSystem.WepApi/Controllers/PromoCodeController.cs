using AppointmentSystem.Application.Services.Interfaces;
using AppointmentSystem.Application.Common.Models.PromoCode;

namespace AppointmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromoCodesController : ControllerBase
    {
        private readonly IPromoCodeService _promoCodeService;

        public PromoCodesController(IPromoCodeService promoCodeService)
        {
            _promoCodeService = promoCodeService;
        }

        // GET: api/PromoCodes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var promoCodes = await _promoCodeService.GetAllAsync();
            return Ok(promoCodes);
        }
        [HttpGet("code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var promo = await _promoCodeService.GetByCodeAsync(code);
            if (promo == null || !promo.IsValid)
                return NotFound();
            return Ok(promo);
        }


        // GET: api/PromoCodes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var promoCode = await _promoCodeService.GetByIdAsync(id);
            if (promoCode == null)
                return NotFound();
            return Ok(promoCode);
        }

        // POST: api/PromoCodes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePromoCodeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var promoCode = await _promoCodeService.CreateAsync(dto);
            return Ok(promoCode);
        }

        // PUT: api/PromoCodes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdatePromoCodeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedPromo = await _promoCodeService.UpdateAsync(id, dto);
            if (updatedPromo == null)
                return NotFound();

            return Ok(updatedPromo);
        }

        // DELETE: api/PromoCodes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _promoCodeService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }


    }
}

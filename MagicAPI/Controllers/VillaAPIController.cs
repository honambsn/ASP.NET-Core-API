using MagicAPI.Data;
using MagicAPI.Models;
using MagicAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace MagicAPI.Controllers
{
    [Route("api/VillaAPI")]
    //[Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public ActionResult <IEnumerable<VillaDTO>> GetVillas() {
            return Ok(VillaStore.villaList);
        }

        [HttpGet("get2/{id:int}")]
        public VillaDTO GetVillaByID2(int id) {
            return VillaStore.villaList.FirstOrDefault(u => u.ID == id);
        }

        [HttpGet("{id:int}")]
        //[HttpGet("id")]
        //public VillaDTO GetVillaByID(int id) {
        public ActionResult<VillaDTO> GetVillaByID(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = VillaStore.villaList.FirstOrDefault(u => u.ID == id);
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }
    }
}

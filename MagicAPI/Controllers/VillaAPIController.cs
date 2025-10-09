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
        public IEnumerable<VillaDTO> GetVillas() {
            return VillaStore.villaList;
        }

        [HttpGet("id")]
        public VillaDTO GetVillaByID(int id) {
            return VillaStore.villaList.FirstOrDefault(u => u.ID == id);
        }
    }
}

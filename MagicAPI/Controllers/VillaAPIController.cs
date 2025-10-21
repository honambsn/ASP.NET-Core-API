using MagicAPI.Data;
using MagicAPI.Models;
using MagicAPI.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace MagicAPI.Controllers
{
    [Route("api/VillaAPI")]
    //[Route("api/[controller]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {
        public ILogger<VillaAPIController> logger { get; }

        public VillaAPIController(ILogger<VillaAPIController> _logger)
        {
            logger = _logger;
        }

        [HttpGet]
        public ActionResult <IEnumerable<VillaDTO>> GetVillas() {
            return Ok(VillaStore.villaList);
        }
        
        [HttpGet("get2/{id:int}", Name = "GetVillaByID2")]
        public VillaDTO GetVillaByID2(int id) {
            return VillaStore.villaList.FirstOrDefault(u => u.ID == id);
        }

        [HttpGet("{id:int}", Name = "GetVillaByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type = typeof(VillaDTO))]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //[HttpGet("id")]
        //public VillaDTO GetVillaByID(int id) {
        //public ActionResult<VillaDTO> GetVillaByID(int id)
        //public ActionResult GetVillaByID(int id)
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


            [HttpPost]
            [ProducesResponseType(StatusCodes.Status201Created)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]
            public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villaDTO)
            {
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                if (VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("CustomError", "Villa already Exists!");
                    return BadRequest(ModelState);
                }

                if (villaDTO == null)
                {
                    return BadRequest(villaDTO);
                }
            
                if (villaDTO.ID > 0)
                {
                    //return StatusCode(StatusCodes.Status500InternalServerError);
                    return BadRequest("ID should be 0 or not provided for new villa.");

                }

                villaDTO.ID = VillaStore.villaList.OrderByDescending(u => u.ID).FirstOrDefault().ID + 1;
                VillaStore.villaList.Add(villaDTO);


                return CreatedAtRoute("GetVillaByID", new { id = villaDTO.ID }, villaDTO);
            }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public IActionResult DeleteVilla (int id)
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

            VillaStore.villaList.Remove(villa);

            return NoContent();
        }


        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        
        public IActionResult UpdateVila(int id, [FromBody]VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.ID)
            {
                return BadRequest();
            }
            
            var villa = VillaStore.villaList.FirstOrDefault (u => u.ID == id);
            villa.Name = villaDTO.Name;
            villa.Sqft = villaDTO.Sqft;
            villa.Occupancy = villaDTO.Occupancy;

            return NoContent();
        }


        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var villa =  VillaStore.villaList.FirstOrDefault(u => u.ID == id);
            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villa, ModelState);
            return NoContent();
        }
    }
}

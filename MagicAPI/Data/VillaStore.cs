using MagicAPI.Models.Dto;

namespace MagicAPI.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
            new VillaDTO {ID = 1, Name = "Pool View", Sqft = 100, Occupancy = 4},
            new VillaDTO {ID = 2, Name = "Beach View", Sqft = 100, Occupancy = 3},

        };
    }
}

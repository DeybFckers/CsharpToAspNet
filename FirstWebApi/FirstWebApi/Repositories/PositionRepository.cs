using FirstWebApi.Contracts;
using FirstWebApi.Models;

namespace FirstWebApi.Repositories
{
    /*we just inherit the BaseRepository and then
    *the letter t of BaseReporsitory will be replace as Position*/
    public class PositionRepository : BaseRepository<Position>, IPositionRepository
    {
        /*we use a constructor chaining on base so we can
         * initialize the table of BaseRepository*/
        public PositionRepository() : base()
        {
        }
    }
}

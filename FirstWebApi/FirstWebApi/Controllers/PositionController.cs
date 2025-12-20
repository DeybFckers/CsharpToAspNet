using FirstWebApi.Contracts;
using FirstWebApi.Models;

namespace FirstWebApi.Controllers
{
    /*inherit from basecontroller and the letter T 
     * change it to model class Name then automatically
     we have crud operations now*/
    public class PositionController : BaseController<Position>
    {
        //apply a constructor chaining so we can
        //pass the repository from injection
        public PositionController(IBaseRepository<Position> repository) : base(repository)
        //update the paramater(IBaseRepository) to this IPositionRepository repository if you want to add a function
        {

        }
    }
}

using FirstWebApi.Models;

namespace FirstWebApi.Contracts
{
    //we inherit from IBaseRepository with Position as the generic type
    public interface IPositionRepository : IBaseRepository<Position>
    {
        
    }
}

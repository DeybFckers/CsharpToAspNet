using FirstWebApi.Models;

namespace FirstWebApi.Contracts
{
    public interface IHeroRepository: IBaseRepository<Hero>
    {
        //if you want to add a new functions not just crud operation
        // then do it like this then add the method in HeroRepository 
        IEnumerable<Hero> GetByAge(int age);
    }
}

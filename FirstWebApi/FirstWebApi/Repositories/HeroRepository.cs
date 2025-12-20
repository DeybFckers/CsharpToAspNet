using FirstWebApi.Contracts;
using FirstWebApi.Models;

namespace FirstWebApi.Repositories
{
    /*we just inherit the BaseRepository and then
    *the letter t of BaseReporsitory will be replace as Hero*/
    public class HeroRepository : BaseRepository<Hero>, IHeroRepository
    {
        /*we use a constructor chaining on base so we can
         * initialize the table of BaseRepository*/
        public HeroRepository() : base() { }
        //everytime you add a function in IHeroRepository, then do this
        public IEnumerable<Hero> GetByAge(int age)
        {
            return _table.Where(t => t.Age == age);//lambda function

        }
        //then after you do this, update the HeroController so it will work
    }
    
}

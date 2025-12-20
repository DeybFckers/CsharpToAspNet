using FirstWebApi.Contracts;
using FirstWebApi.Infrastructure;
using FirstWebApi.Models;

namespace FirstWebApi.Repositories
{
                        /* T is a generic type parameter.
                        * It represents the model type that this repository will work with
                        * (for example: User, Product, etc.).
                        * The actual type is provided when the repository is used. */
    public class BaseRepository<T> : IBaseRepository<T>
                                            //interface repository
        where T : IBaseModel
        /* This constraint ensures that T must implement IBaseModel,
         * so the repository can safely use properties like Id. */
    {
        public readonly FakeDbContext _db = new FakeDbContext();
        public readonly List<T> _table;
                        /*it means it will list of models
                         * for example if we provide a user
                         then it means theres a list of user*/
        public BaseRepository()
        {
            _table = _db.GetTable<T>().ToList();
            /*value of _table is from GetTable of FakeDbContext*/
        }
        public void Add(T model) // add user
        {
            //the reason why we can still access
            //the id because of IBaseModel has a property of Id
            // same with Update, GetOne and Delete methods below
            model.Id = _table.Count + 1;
            _table.Add(model);
        }

        public IEnumerable<T> GetAll() // get all users
        {
            return _table;
        }
        
        public T GetOne(int id) // get user by id
        {
            return _table.FirstOrDefault(u => u.Id == id);
            
        }
        public void Update(T model)// update user by id
        {
            //findindex means find the id/index of the user
            var index = _table.FindIndex(u => u.Id == model.Id);
            _table[index] = model;
        }

        public void Delete(int id)// delete user by id
        {
            _table.RemoveAll(u => u.Id == id);
        }
    }
}

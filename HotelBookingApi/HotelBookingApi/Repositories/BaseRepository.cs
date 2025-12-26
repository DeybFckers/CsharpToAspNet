using HotelBookingApi.Contracts;
using HotelBookingApi.Infrastructure;

namespace HotelBookingApi.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : IBaseModel
    {
        public readonly FakeDbContext _db = new FakeDbContext();
        public readonly List<T> _table;
        
        public BaseRepository()
        {
            _table = _db.GetTable<T>().ToList();

        }
        public void Add(T model)
        {
            model.Id = _table.Count + 1;
            _table.Add(model);
        }

        public void Delete(int Id)
        {
            _table.RemoveAll(u => u.Id == Id);
        }

        public IEnumerable<T> GetAll()
        {
            return _table;
        }

        public T GetOne(int Id)
        {
            return _table.FirstOrDefault(u => u.Id == Id);
        }

        public void Update(T model)
        {
            var index = _table.FindIndex(u => u.Id ==  model.Id);
            _table[index] = model;
        }
    }
}

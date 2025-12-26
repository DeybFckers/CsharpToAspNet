namespace HotelBookingApi.Contracts
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetOne(int Id);
        void Add(T model);
        void Update(T model);
        void Delete(int Id);
    }
}

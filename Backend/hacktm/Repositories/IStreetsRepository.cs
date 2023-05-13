using hacktm.Entities;

namespace hacktm.Repositories
{
    public interface IStreetsRepository : IGenericRepository<Street>
    {
        public Street FindByName(string name);
    }
}

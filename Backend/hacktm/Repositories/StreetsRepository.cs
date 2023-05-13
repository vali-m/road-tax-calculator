using hacktm.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace hacktm.Repositories
{
    public class StreetsRepository : GenericRepository<Street, APIDbContext>, IStreetsRepository
    {
        public StreetsRepository(APIDbContext context) :
            base(context)
        {
        }

        public Street FindByName(string name)
        {
            return base.GetQuery().Where(s => this.Compare(s.Name, name)).FirstOrDefault();
        }

        private bool Compare(string a, string b)
        {
            string[] aw = a.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] bw = b.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);


            var similar = aw.Intersect(bw, StringComparer.OrdinalIgnoreCase).Count();
            return similar >= 1;
        }
    }
}

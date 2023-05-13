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
            var query = base.GetQuery().ToList();
            query.Sort(delegate (Street s, ) { return c1.date.CompareTo(c2.date); })

            return query.Where(s => this.IsSimilarW(s.Name, name, 0.7));
            return null;
        }

        private double Compare(string a, string b)
        {
            string[] aw = a.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] bw = b.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);


            var similar = aw.Intersect(bw, StringComparer.OrdinalIgnoreCase).Count();
            return (double)similar / aw.Length;
        }

        private bool IsSimilarW(string a, string b, double similarity)
        {
            string[] aw = a.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            string[] bw = b.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);


            var similar = aw.Intersect(bw, StringComparer.OrdinalIgnoreCase).Count();
            return similar / aw.Length >= similarity;
        }
    }
}

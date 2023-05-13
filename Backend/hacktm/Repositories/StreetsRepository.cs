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
            //var simPairs = new List<Tuple<Street, double>>();
            //foreach(var s in query)
            //{
            //    var similarity = this.Compare(s.Name, name);
            //    if (similarity > 0.7)
            //        simPairs.Add(new Tuple<Street, double>(s, similarity));
            //}

            //if (simPairs.Count == 0)
            //    return null;

            //Tuple<Street, double> result = simPairs[0];

            //foreach (var p in simPairs)
            //{
            //    if(p.Item2 > result.Item2)
            //        result = p;
            //}

            //return result.Item1;
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

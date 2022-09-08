using Microsoft.EntityFrameworkCore;
using Tracking_api.Models;

namespace Tracking_api.Data
{
    public class IssueDbContext: DbContext
    {
        public IssueDbContext(DbContextOptions<IssueDbContext> options)  // custom constructor allowing to set certain settings needed by DbContext to establish a connection
            : base(options)
        {
        }

        public DbSet<Issue> Issues { get; set; }  // representation of a table in the Database used to manipulate data of an issue
    }
}

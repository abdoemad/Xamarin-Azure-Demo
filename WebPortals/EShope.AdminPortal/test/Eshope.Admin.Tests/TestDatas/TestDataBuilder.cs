using Eshope.Admin.EntityFrameworkCore;

namespace Eshope.Admin.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly AdminDbContext _context;

        public TestDataBuilder(AdminDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}
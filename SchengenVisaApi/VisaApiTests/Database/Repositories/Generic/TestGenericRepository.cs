using Domains.Users;
using Infrastructure.Database.Generic;

namespace VisaApi.Database.Repositories.Generic
{
    public class TestGenericRepository(IGenericReader reader, IGenericWriter writer) : GenericRepository<User>(reader, writer);
}

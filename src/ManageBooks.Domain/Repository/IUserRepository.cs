using ManageBooks.Domain.Entity;
using ManageBooks.Domain.SeedWork.SearchableRepository;

namespace ManageBooks.Domain.Repository;

public interface IUserRepository : ISearchableRepository<User>
{
    public Task Insert(User user, CancellationToken cancellationToken);
    public Task<User> Get(Guid id, CancellationToken cancellationToken);
    public Task Delete(User user, CancellationToken cancellationToken);
    public Task Update(User user, CancellationToken cancellationToken);
}

using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Contract.Infrastructure
{
    public interface IAuthorRepository : IGenericRepositry<Author>
    {
        Task<List<Author>> GetAuthorByName(List<string> Names);
    }
}

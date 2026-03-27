using LibrarySys.Application.Contract.Infrastructure.GenericContract;
using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Contract.Infrastructure.AuthorContract
{
    public interface IAuthorRepository : IGenericRepositry<Author>
    {
        Task<List<Author>> GetAuthorByName(List<string> Names);
    }
}

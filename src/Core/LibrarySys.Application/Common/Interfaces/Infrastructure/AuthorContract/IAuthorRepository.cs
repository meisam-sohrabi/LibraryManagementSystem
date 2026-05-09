using LibrarySys.Application.Common.Interfaces.Infrastructure.GenericContract;
using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Common.Interfaces.Infrastructure.AuthorContract
{
    public interface IAuthorRepository : IGenericRepositry<Author>
    {
        Task<List<Author>> GetAuthorByName(List<string> Names);
    }
}

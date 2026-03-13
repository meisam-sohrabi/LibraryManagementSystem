using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Contract.Infrastructure
{
    public interface IBookRepository : IGenericRepositry<Book>
    {
        //Task<Book> GetBySearch(string  search);    
        Task<bool> ExistByTitleAndGenre(string  Title,string Genre);
        Task<bool> ExistById(Guid Id);


    }
}

using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Contract.Infrastructure
{
    public interface IBookRepository : IGenericRepositry<Book>
    {
        //Task<Book> GetBySearch(string  search);    
        Task<bool> Exist(string  Title,string Genre);
        
    }
}

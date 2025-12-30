using Dapper;
using LibrarySys.Application.Contract.BookAuthor;
using LibrarySys.Application.DTOs;
using LibrarySys.Domain.Entity;
using LibrarySys.Infrastructure.EntityFrameworkCore.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LibrarySys.Infrastructure.EntityFrameworkCore.Service
{
    public class BookAuthorService : IBookAuthorService
    {
        private readonly AppDbContext _context;

        public BookAuthorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<GetBookResponseDto>> GetBookAuthor()
        {
            var spName = "GetBookAuthor";
            using var connection = _context.Database.GetDbConnection();
            if(connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            var rows = await connection.QueryAsync<GetBookResponseDto>(spName);
            return rows.ToList();
        }

        public async Task<BookAuthorRequestDto> GetByIdWithAuthor(Guid Id)
        {
            return await (from book in _context.Book
                          join bookauthor in _context.BookAuthor on book.Id equals bookauthor.BookId
                          join author in _context.Author on bookauthor.AuthorId equals author.Id
                          where book.Id == Id
                          select new BookAuthorRequestDto
                          {
                              Title = book.Title,
                              Genere = book.Genere,
                              AvailableCopies = book.AvailableCopies,
                              Authors = book.Authors.Select(c => new AuthorRequestDto
                              {
                                  Name = c.Author.Name,
                                  BirthYear = c.Author.BirthYear
                              }).ToList()
                          }).FirstOrDefaultAsync();

            //return await _context.Book.Where(c => c.Id == Id).Select(c => new BookAuthorRequestDto
            //{
            //    Title = c.Title,
            //    Genere = c.Genere,
            //    AvailableCopies = c.AvailableCopies,
            //    Authors = c.Authors.Select(c => new AuthorRequestDto
            //    {
            //        Name = c.Author.Name,
            //        BirthYear = c.Author.BirthYear
            //    }).ToList()
            //}).FirstOrDefaultAsync();
        }
    }
}

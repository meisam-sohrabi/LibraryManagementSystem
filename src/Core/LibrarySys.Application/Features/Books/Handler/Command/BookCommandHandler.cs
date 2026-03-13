using FluentValidation;
using LibrarySys.Application.Contract;
using LibrarySys.Application.Contract.Infrastructure;
using LibrarySys.Application.DTOs;
using LibrarySys.Application.Features.Books.Request.Command;
using LibrarySys.Domain.Entity;
using MediatR;
using System.Net;

namespace LibrarySys.Application.Features.Books.Handler.Command
{
    public class BookCommandHandler : IRequestHandler<BookCommand, BaseResponseDto<BookAuthorRequestDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IUnitOfWork _uow;
        private readonly IValidator<BookAuthorRequestDto> _validator;
        public BookCommandHandler(IBookRepository bookRepository
            , IAuthorRepository authorRepository
            , IUnitOfWork uow
            , IValidator<BookAuthorRequestDto> validator)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _uow = uow;
            _validator = validator;
        }
        public async Task<BaseResponseDto<BookAuthorRequestDto>> Handle(BookCommand request, CancellationToken cancellationToken)
        {
            var output = new BaseResponseDto<BookAuthorRequestDto>();

            var validation = await _validator.ValidateAsync(request.BookAuthorRequest);
            if (!validation.IsValid)
            {
                output.Message = "خطاهای اعتبارسنجی رخ داده است.";
                output.Success = false;
                output.StatusCode = HttpStatusCode.BadRequest;
                output.ValidationErrors = validation.ToDictionary();
                return output;
            }

            var bookExist = await _bookRepository.ExistByTitleAndGenre(request.BookAuthorRequest.Title, request.BookAuthorRequest.Genere);
            if (bookExist)
            {
                output.Message = "کتاب از قبل ایجاد شده است";
                output.Success = true;
                output.StatusCode = HttpStatusCode.OK;
                return output;
            }
            await _uow.BeginTransactionAsync();
            try
            {
                var book = new Book
                {
                    Title = request.BookAuthorRequest.Title,
                    PublishYear = request.BookAuthorRequest.PublishYear,
                    AvailableCopies = request.BookAuthorRequest.AvailableCopies,
                    Genere = request.BookAuthorRequest.Genere,
                };

                var authorsName = request.BookAuthorRequest.Authors.Select(auth => auth.Name).ToList();
                var authorExist = await _authorRepository.GetAuthorByName(authorsName);
                var authorDic = authorExist.ToDictionary(c => c.Name);
                foreach (var a in request.BookAuthorRequest.Authors)
                {
                    var author = authorDic.GetValueOrDefault(a.Name) ?? new Author { Name = a.Name, BirthYear = a.BirthYear };

                    book.Authors.Add(new BookAuthor
                    {
                        Author = author
                    });

                }


                await _bookRepository.AddAsync(book);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                output.Message = "کتاب و نویسنده ایجاد شد";
                output.StatusCode = HttpStatusCode.OK;
                output.Success = true;
                output.Data = new BookAuthorRequestDto
                {
                    Title = book.Title,
                    Genere = book.Genere,
                    AvailableCopies = book.AvailableCopies,
                    PublishYear = book.PublishYear,
                    Authors = book.Authors.Select(c => new AuthorRequestAppDto { Name = c.Author.Name, BirthYear = c.Author.BirthYear }).ToList()
                };
                return output;
            }
            catch (Exception ex)
            {
                await _uow.RollBackTransactionAsync();

                output.Message = "خطای غیرمنتظره رخ داد" + ex.Message;
                output.Success = false;
                output.StatusCode = HttpStatusCode.InternalServerError;
            }
            return output;
        }
    }
}

using FluentValidation;
using LibraryManagement.Test.Mock;
using LibrarySys.Application.Contract;
using LibrarySys.Application.Contract.Infrastructure;
using LibrarySys.Application.DTOs;
using LibrarySys.Application.Features.Handlers.Books.Handler.Command;
using LibrarySys.Application.Features.Handlers.Books.Request.Command;
using Moq;
using Shouldly;

namespace LibraryManagement.Test.BookAuthorService.Command
{

    public class CreateBookAuthorCommandHandlerTest
    {
        Mock<IBookRepository> _mock;
        Mock<IUnitOfWork> _unitOfWork;
        Mock<IAuthorRepository> _authorRepository;
        Mock<IValidator<BookAuthorRequestDto>> _validator;

        public CreateBookAuthorCommandHandlerTest()
        {
            _mock = MockBookAuthRepository.BookAuthRepo();
            _unitOfWork = MockUnitOfWork.UOW();
            _authorRepository = MockAuthorRepository.AuthorRepo();
            _validator = MockBookAuthorRequestDto.BookAuthorRequest();
        }


        [Fact]
        public async Task CreateBookAuthor_Add_ShouldReturn_BookAuthors()
        {
            var handler = new BookCommandHandler(_mock.Object, _authorRepository.Object, _unitOfWork.Object, _validator.Object);
            var bookAuth = new BookAuthorRequestDto
            {
                Title = "دختر زیبا",
                Genere = "روانشانی",
                AvailableCopies = 12,
                PublishYear = 1370,
                Authors = new List<AuthorRequestDto>
                {
                    new AuthorRequestDto
                    {
                        Name = "محمود دولت آبادی",
                        BirthYear = 1365
                    }
                }
            };
            var result = await handler.Handle(new BookCommand { BookAuthorRequest = bookAuth }, CancellationToken.None);

            result.ShouldBeOfType<BaseResponseDto<BookAuthorRequestDto>>();
            result.ShouldNotBeNull();
            var bookAuthors = await _mock.Object.GetAll();
            bookAuthors.Count.ShouldBe(3);
        }
    }
}

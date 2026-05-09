using FluentValidation;
using LibrarySys.Application.Common.DTOs;
using Moq;

namespace LibraryManagement.Test.Mock
{
    public static class MockBookAuthorRequestDto
    {
        public static Mock<IValidator<BookAuthorRequestDto>> BookAuthorRequest()
        {
            var mockDto = new Mock<IValidator<BookAuthorRequestDto>>();
            mockDto.Setup(c => c.ValidateAsync(It.IsAny<BookAuthorRequestDto>(),It.IsAny<CancellationToken>())).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            return mockDto;
        }
    }
}

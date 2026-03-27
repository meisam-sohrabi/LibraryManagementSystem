using LibrarySys.Application.Contract.Infrastructure.UserContract;
using LibrarySys.Application.DTOs;
using LibrarySys.Application.Features.Account.Request.Command;
using LibrarySys.Domain.Entity;
using MediatR;
using System.Net;
using LibrarySys.Domain.Enum;
using LibrarySys.Application.Contract;
namespace LibrarySys.Application.Features.Account.Handler.Command
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, BaseResponseDto<string>>
    {
        private readonly IUserManagerRepository _userManagerRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAccountCommandHandler(IUserManagerRepository userManagerRepo, IUnitOfWork unitOfWork)
        {
            _userManagerRepo = userManagerRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponseDto<string>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            BaseResponseDto<string> outPut = new BaseResponseDto<string>();

            await _unitOfWork.BeginTransactionAsync();

            try
            {

                // 1.check duplication 
                bool duplicateUser = await _userManagerRepo.ExistByUsername(request.Register.UserName);

                if (duplicateUser)
                {
                    outPut.Message = $"کاربر{request.Register.UserName} از قبل در سیستم ثبت شده است";
                    outPut.Success = false;
                    outPut.StatusCode = HttpStatusCode.BadRequest;
                    return outPut;
                }

                // 2. validation of current password
                if (!isValidPassword(request.Register.Password))
                {
                    outPut.Message = "رمز عبور جدید ضعیف است";
                    outPut.Success = false;
                    outPut.StatusCode = HttpStatusCode.Forbidden;
                    return outPut;
                }


                // 3. hash current pass
                string hashPass = _userManagerRepo.PasswordHash(request.Register.Password);

                // 4. create a user

                User identityUser = new User
                {
                    UserName = request.Register.UserName,
                    Email = request.Register.Email,
                    PhoneNumber = request.Register.PhoneNumber,
                    Role = UserRole.User,
                    UserStatus = UserActivity.Active,
                    PasswordHash = hashPass,
                    FullName = request.Register.FullName
                };

                // .5 add user in db
                await _userManagerRepo.CreateAsync(identityUser);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                outPut.Message = $"کاربر {identityUser.FullName} باموفقیت ثب شد";
                outPut.Success = true;
                outPut.StatusCode = HttpStatusCode.OK;
                outPut.Data = outPut.Message;
                return outPut;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackTransactionAsync();

                outPut.Message = "خطای غیرمنتظره رخ داد" + ex.Message;
                outPut.Success = false;
                outPut.StatusCode = HttpStatusCode.InternalServerError;
                return outPut;
            }
        }

        private bool isValidPassword(string password)
        {
            return password.Length >= 8 && password.Any(char.IsUpper) && password.Any(char.IsDigit);
        }
    }
}

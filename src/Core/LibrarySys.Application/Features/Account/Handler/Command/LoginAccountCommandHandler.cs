using LibrarySys.Application.Contract;
using LibrarySys.Application.Contract.Infrastructure.RefreshTokenContract;
using LibrarySys.Application.Contract.Infrastructure.UserContract;
using LibrarySys.Application.Contract.Token;
using LibrarySys.Application.DTOs;
using LibrarySys.Application.Features.Account.Request.Command;
using LibrarySys.Domain.Entity;
using MediatR;
using System.Net;
namespace LibrarySys.Application.Features.Account.Handler.Command
{
    public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, BaseResponseDto<Tuple<string, string>>>
    {
        private readonly IUserManagerRepository _userManagerRepo;
        private readonly ITokenService _tokenService;
        private readonly IRefreshTokenRepository _refreshTokenRepo;
        private readonly IUnitOfWork _unitOfWork;

        public LoginAccountCommandHandler(IUserManagerRepository userManagerRepo, ITokenService tokenService,
            IRefreshTokenRepository refreshTokenRepo, IUnitOfWork unitOfWork)
        {
            _userManagerRepo = userManagerRepo;
            _tokenService = tokenService;
            _refreshTokenRepo = refreshTokenRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponseDto<Tuple<string, string>>> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            BaseResponseDto<Tuple<string, string>> outPut = new BaseResponseDto<Tuple<string, string>>();

            // 1. find user 

            User userExist = await _userManagerRepo.FindByUsernameAsync(request.Login.Username);

            // 2. user and password validation

            if (userExist == null || !(_userManagerRepo.VerifyPassword(request.Login.Password, userExist)))
            {
                outPut.Message = "نام کاربری یا رمز عبور اشتباه است";
                outPut.StatusCode = HttpStatusCode.BadRequest;
                outPut.Success = false;
                return outPut;
            }

            // 3. Generate access token

            string generateAccessToken = _tokenService.GenerateJWT(userExist);



            // 4. check the existence of refresh token

            RefreshToken refreshTokenExist = await _refreshTokenRepo.GetTokenByUserId(userExist.Id);


            // 5. new hashed refresh token

            string hashRefreshToken = _tokenService.TokenHash(_tokenService.GenerateRefreshToken());


            // 6. using variable to check if is null or its not n
            string finalRefreshToken;

            if (refreshTokenExist == null)
            {
                RefreshToken newRefreshToken = new RefreshToken
                {
                    Token = hashRefreshToken,
                    UserId = userExist.Id,
                    User = userExist,
                    CreatedAt = DateTime.UtcNow,
                    ExpireAt = DateTime.UtcNow.AddMinutes(5),
                    IsRevoked = false
                };
                await _refreshTokenRepo.AddAsync(newRefreshToken);
                finalRefreshToken = hashRefreshToken;
            }
            else
            {
                refreshTokenExist.Token = hashRefreshToken;
                refreshTokenExist.CreatedAt = DateTime.UtcNow;
                refreshTokenExist.ExpireAt = DateTime.UtcNow.AddMinutes(5);
                refreshTokenExist.IsRevoked = false;
                _refreshTokenRepo.Update(refreshTokenExist);
                finalRefreshToken = hashRefreshToken;
            }
            await _unitOfWork.SaveChangesAsync();




            outPut.Message = "کاربر با موفقیت ورود کرد";
            outPut.StatusCode = HttpStatusCode.OK;
            outPut.Success = true;
            outPut.Data = new Tuple<string, string>(string.Concat("Bearer ", generateAccessToken), hashRefreshToken);
            return outPut;
        }
    }

}

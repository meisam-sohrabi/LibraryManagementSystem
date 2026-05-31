using LibrarySys.Application.Common.DTOs;
using LibrarySys.Application.Common.Interfaces;
using LibrarySys.Application.Common.Interfaces.Infrastructure.RefreshTokenContract;
using LibrarySys.Application.Common.Interfaces.Token;
using LibrarySys.Application.Features.RefreshTokens.Request.Command;
using LibrarySys.Domain.Entity;
using System.Net;

namespace LibrarySys.Application.Features.RefreshTokens.Handler.Command
{
    public class RequestRefreshTokenCommandHandler : IRequestHandler<RequestForRefreshTokenCommand, BaseResponseDto<Tuple<string, string>>>
    {
        private readonly IRefreshTokenRepository _refreshToken;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RequestRefreshTokenCommandHandler(IRefreshTokenRepository refreshToken, ITokenService tokenService
            , IUnitOfWork unitOfWork)
        {
            _refreshToken = refreshToken;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponseDto<Tuple<string, string>>> Handle(RequestForRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            BaseResponseDto<Tuple<string, string>> outPut = new BaseResponseDto<Tuple<string, string>>();

            await _unitOfWork.BeginTransactionAsync();


            try
            {
                // 1. authenticat the incoming token

                RefreshToken getRefreshToken = await _refreshToken.GetTokenWithFlag(request.token);

                if (getRefreshToken == null || getRefreshToken.ExpireAt < DateTime.UtcNow)
                {
                    outPut.Message = "رفرش توکن نا معتبر لاگ این مجدد";
                    outPut.Success = false;
                    outPut.StatusCode = HttpStatusCode.BadRequest;
                    return outPut;
                }

                // 2. Check user existence

                if (getRefreshToken.User == null)
                {
                    outPut.Message = "یوزر یافت نشد";
                    outPut.Success = false;
                    outPut.StatusCode = HttpStatusCode.BadRequest;
                    return outPut;
                }

                // 3. Making the old refresh token true first

                getRefreshToken.IsRevoked = true;
                _refreshToken.Update(getRefreshToken);

                // 3. Generate access token with hashed refreshtoken , change the revoke flag to false

                string generateAccessToken = _tokenService.GenerateJWT(getRefreshToken.User);

                getRefreshToken.Token = _tokenService.TokenHash(_tokenService.GenerateRefreshToken());
                getRefreshToken.IsRevoked = false;

                // 4. Update db

                _refreshToken.Update(getRefreshToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();
                outPut.Message = "رفرش توکن ایجاد شد";
                outPut.Success = true;
                outPut.Data = new Tuple<string, string>(string.Concat("Bearer ", generateAccessToken), getRefreshToken.Token);
                outPut.StatusCode = HttpStatusCode.OK;
                //return outPut;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackTransactionAsync();
                outPut.Message = "خطای غیرمنتظره رخ داد" + ex.Message;
                outPut.Success = false;
                outPut.StatusCode = HttpStatusCode.InternalServerError;
            }
            return outPut;
        }
    }
}

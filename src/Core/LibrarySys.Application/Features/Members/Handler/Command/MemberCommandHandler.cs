using LibrarySys.Application.Common.DTOs;
using LibrarySys.Application.Common.Interfaces;
using LibrarySys.Application.Common.Interfaces.Infrastructure.MemberContract;
using LibrarySys.Application.Common.Interfaces.Infrastructure.UserContract;
using LibrarySys.Application.Features.Members.Request.Command;
using LibrarySys.Domain.Entity;
using System.Net;
namespace LibrarySys.Application.Features.Members.Handler.Command
{
    public class MemberCommandHandler : IRequestHandler<MemberCommand, BaseResponseDto<Member>>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _uow;
        private readonly IUserManagerRepository _userManager;

        public MemberCommandHandler(IMemberRepository memberRepository
            , IUnitOfWork uow
            , IUserManagerRepository userManager)
        {
            _memberRepository = memberRepository;
            _uow = uow;
            _userManager = userManager;
        }
        public async Task<BaseResponseDto<Member>> Handle(MemberCommand request, CancellationToken cancellationToken)
        {
            var output = new BaseResponseDto<Member>();
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                output.Message = "خطا در ارسال داده";
                output.StatusCode = HttpStatusCode.BadRequest;
                output.Success = false;
                return output;
            }
            var userRegistered = await _userManager.ExistByEmail(request.Email);
            if (!userRegistered)
            {
                output.Message = "ابتدا باید در سامانه ثبت نام کنید";
                output.StatusCode = HttpStatusCode.BadRequest;
                output.Success = true;
                return output;
            }
            var memberRegistered = await _memberRepository.GetMember(Email: request.Email);
            if(memberRegistered != null)
            {
                output.Message = "شما قبلا عضو شده اید";
                output.StatusCode = HttpStatusCode.Conflict;
                output.Success = true;
                return output;
            }
            var member = new Member
            {
                Email = request.Email,
                JoinDate = DateTime.UtcNow
            };

            await _memberRepository.AddAsync(member);
            await _uow.SaveChangesAsync();

            output.Message = "عضو جدید ایجاد شد";
            output.StatusCode = HttpStatusCode.OK;
            output.Success = true;
            output.Data = member;
            return output;
        }
    }
}

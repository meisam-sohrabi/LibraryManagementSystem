using LibrarySys.Application.Common.Interfaces.Infrastructure.GenericContract;
using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Common.Interfaces.Infrastructure.MemberContract
{
    public interface IMemberRepository : IGenericRepositry<Member>
    {
        Task<Member> GetMember(string Email);
    }
}

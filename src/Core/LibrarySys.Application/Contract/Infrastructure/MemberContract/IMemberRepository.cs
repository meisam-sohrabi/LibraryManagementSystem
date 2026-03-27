using LibrarySys.Application.Contract.Infrastructure.GenericContract;
using LibrarySys.Domain.Entity;

namespace LibrarySys.Application.Contract.Infrastructure.MemberContract
{
    public interface IMemberRepository : IGenericRepositry<Member>
    {
        Task<Member> GetMember(string Email);
    }
}

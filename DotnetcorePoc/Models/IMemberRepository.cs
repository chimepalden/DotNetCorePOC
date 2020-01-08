using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetcorePoc.Models
{
    // this is an interface which is implemented by class, MockMemberRepository
    // specifies operations supported by the repository
    public interface IMemberRepository
    {
        Member GetMember(int Id);
        IEnumerable<Member> GetMembers();
        Member Add(Member member);
        Member Update(Member memberEdited);
        Member Delete(int id);
    }
}

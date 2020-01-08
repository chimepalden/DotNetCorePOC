using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetcorePoc.Models
{
    public class SqlMemberRepository : IMemberRepository
    {
        private readonly AppDbContext context;

        public SqlMemberRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Member Add(Member member)
        {
            context.Members.Add(member);
            context.SaveChanges();
            return member;
        }

        public Member Delete(int id)
        {
            Member member = context.Members.FirstOrDefault(m => m.Id == id);
            if(member != null)
            {
                context.Members.Remove(member);
                context.SaveChanges();
            }
            return member;
        }

        public Member GetMember(int Id)
        {
            return context.Members.Find(Id);
        }

        public IEnumerable<Member> GetMembers()
        {
            return context.Members;
        }

        public Member Update(Member memberEdited)
        {
            var member = context.Members.Attach(memberEdited);
            // attach returns type Member and we have to tell EF that attached is modified
            member.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return memberEdited;
        }
    }
}

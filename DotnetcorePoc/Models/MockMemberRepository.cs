using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetcorePoc.Models
{
    // this class provides implementation for interface, IMemberRepository
    // details of how operations in interface are performed here.
    public class MockMemberRepository : IMemberRepository
    {
        private List<Member> memberList;
        public MockMemberRepository()
        {
            memberList = new List<Member>()
            {
                new Member() { Id = 1, Name = "Chime", Email = "Chime@gmail.com", Department = Dept.HR},
                new Member() { Id = 2, Name = "C", Email = "C@gmail.com", Department = Dept.IT},
                new Member() { Id = 3, Name = "P", Email = "P@gmail.com", Department = Dept.Accounting},
                new Member() { Id = 4, Name = "Palden", Email = "Palden@gmail.com", Department = Dept.Sales}
            };
        }

        public Member Add(Member member)
        {
            member.Id = memberList.Max(e => e.Id) + 1;
            memberList.Add(member);
            return member;
        }

        public Member Delete(int id)
        {
            Member member = memberList.FirstOrDefault(m => m.Id == id);
            if(member != null)
            {
                memberList.Remove(member);
            }
            return member;
        }

        public Member GetMember(int Id)
        {
            return memberList.FirstOrDefault(m => m.Id == Id);
        }

        public IEnumerable<Member> GetMembers()
        {
            return memberList;
        }

        public Member Update(Member memberEdited)
        {
            Member member = memberList.FirstOrDefault(m => m.Id == memberEdited.Id);
            if(member != null)
            {
                member.Name = memberEdited.Name;
                member.Email = memberEdited.Email;
                member.Department = memberEdited.Department;
            }
            return member;
        }
    }
}

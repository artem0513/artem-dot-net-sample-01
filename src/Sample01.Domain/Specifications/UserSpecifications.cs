using Sample01.Domain.Entities;
using Sample01.Domain.Enums;
using Sample01.Domain.Core.Specifications;

namespace Sample01.Domain.Specifications
{
    public static class UserSpecifications
    {
        public static BaseSpecification<User> GetUserByEmailAndPasswordSpec(string emailId, string password)
        {
            return new BaseSpecification<User>(x => x.EmailId == emailId && x.Password == password && x.IsDeleted == false);
        }

        public static BaseSpecification<User> GetAllActiveUsersSpec()
        {
            return new BaseSpecification<User>(x => x.Status == UserStatus.Active && x.IsDeleted == false);
        }
    }
}

using Sample01.Application.Models.Requests;
using Sample01.Application.Models.Responses;

namespace Sample01.Application.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserRes> CreateUser(CreateUserReq req);

        Task<ValidateUserRes> ValidateUser(ValidateUserReq req);

        Task<GetAllActiveUsersRes> GetAllActiveUsers();
    }
}
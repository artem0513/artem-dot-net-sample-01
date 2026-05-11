using Sample01.Application.Models.DTOs;

namespace Sample01.Application.Models.Responses
{
    public class GetAllActiveUsersRes
    {
        public IList<UserDTO> Data { get; set; }
    }
}

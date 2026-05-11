using System.ComponentModel.DataAnnotations;
using Sample01.Domain.Enums;

namespace Sample01.Application.Models.Requests
{
    public class CreateUserReq
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string EmailId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public UserStatus Status { get; set; }
    }
}

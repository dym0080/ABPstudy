namespace SNAS.Application.SoftUsers.Dto
{
    
    public class ChangePasswordDto
    {
        public long SoftUserId { get; set; }

        public string NewPassword { get; set; }

        public string NewPassword2 { get; set; }
    }
}

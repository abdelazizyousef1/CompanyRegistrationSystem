namespace Company.Services.DTOS
{
    public class SetPasswordRequestDto
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

}

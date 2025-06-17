namespace SkillSystem.Aplication.Dtos;

public class LogInResponseDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string TokenType { get; set; }
    public int Expires { get; set; }
}

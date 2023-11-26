namespace WebLABA_3.Models;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public int RoleId { get; set; }
    public Role? Role { get; set; }
}
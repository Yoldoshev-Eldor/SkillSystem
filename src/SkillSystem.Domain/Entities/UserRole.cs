using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSystem.Domain.Entities;

public class UserRole
{
    public long RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public string? Decscription { get; set; }
    public ICollection<User> Users { get; set; }
}

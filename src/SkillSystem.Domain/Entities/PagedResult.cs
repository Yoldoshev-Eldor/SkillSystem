using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillSystem.Domain.Entities;

public class PagedResult<User>
{
    public int TotalCount { get; set; } 
    public ICollection<User> Data { get; set; } 
    public PagedResult()
    {
        Data = new List<User>();
    }
}

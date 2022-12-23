using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities;

public class ApplicationUser : IdentityUser
{
    public string nickName { get; set; }
    public string aboutMe { get; set; }
    public byte[] profileImg { get; set; }
    public DateTime tokenCreationTime { get; set; }

    #region ICollections
    public ICollection<Topic> topics { get; set; }
    public ICollection<Post> posts { get; set; }
    public ICollection<Report> reports { get; set; }
    public ICollection<Like> likes { get; set; }
    public ICollection<Save> saves { get; set; }
    #endregion
}

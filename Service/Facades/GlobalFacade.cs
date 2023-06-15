using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Service.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Facades
{
    public class GlobalFacade
    {
        public readonly DBforum db;
        public readonly IMessageService messageService;
        public readonly UserManager<ApplicationUser> userManager;

        public GlobalFacade(DBforum db, IMessageService messageService, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.messageService = messageService;
            this.userManager = userManager;
        }
    }
}

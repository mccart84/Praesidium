using Praesidium.Data_Context;
using Praesidium.Models.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Praesidium.Data_Models;
using Praesidium.Data_Context.Admin;

namespace Praesidium.DAL
{
    public class NavigationItems : AdminContext
    {
        public List<NavItem> GetActiveNavigationItems()
        {
            var navItems = _context.ShSyNavigationItems.Where(x => x.IsActive && x.ParentId == null && x.ShSySection.IsActive == true && x.ShSySection.Name != "Admin").Select(x => new NavItem
            {
                RecId = x.RecId,
                Controller = x.Controller,
                Action = x.Action,
                DisplayText = x.DisplayText,
                FkShSySection = x.FkShSySection,
                Section = x.ShSySection.Name,
                SortOrder = x.SortOrder,
                IsActive = x.IsActive,
                SubPages = _context.ShSyNavigationItems.Where(s => s.IsActive && s.ParentId == x.RecId).OrderBy(s => s.SortOrder).Select(s => new NavSubItems
                {
                    RecId = s.RecId,
                    Controller = s.Controller,
                    Action = s.Action,
                    DisplayText = s.DisplayText,
                    FkShSySection = s.FkShSySection,
                    IsActive = s.IsActive,
                    ParentId = s.ParentId,
                    SortOrder = s.SortOrder
                }).ToList()
            }).OrderBy(x => x.SortOrder).ToList();

            return navItems;
        }

        public List<AdminNavItem> GetAdminNavItems()
        {
            var adminItems = _context.ShSyNavigationItems.Where(x => x.IsActive && x.ShSySection.Name == "Admin").Select(x => new AdminNavItem
            {
                RecId = x.RecId,
                Controller = x.Controller,
                Action = x.Action,
                DisplayText = x.DisplayText,
                FkShSySection = x.FkShSySection,
                Section = x.ShSySection.Name,
                SortOrder = x.SortOrder,
                IsActive = x.IsActive,
            }).OrderBy(x => x.SortOrder).ToList();

            return adminItems;
        }
    }
}
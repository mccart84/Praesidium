using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Praesidium.Data_Models.Admin;

namespace Praesidium.Data_Context.Admin
{
    public abstract class AdminContext : IDisposable
    {      
        protected AdminEntity _context { get; set; }

        public AdminContext(AdminEntity context)
        {
            _context = context;
        }

        public AdminContext()
        {
            _context = new AdminEntity();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
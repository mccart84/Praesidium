using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Praesidium.Data_Models;

namespace Praesidium.Data_Context
{
    public abstract class NavigationContext : IDisposable
    {
        protected NavigationEntity _context { get; set; }

        public NavigationContext(NavigationEntity context)
        {
            _context = context;
        }

        public NavigationContext()
        {
            _context = new NavigationEntity();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
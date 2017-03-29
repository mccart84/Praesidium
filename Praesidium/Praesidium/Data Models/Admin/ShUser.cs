//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Praesidium.Data_Models.Admin
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShUser()
        {
            this.ShFiles = new HashSet<ShFile>();
            this.ShFiles1 = new HashSet<ShFile>();
        }
    
        public int RecId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string email { get; set; }
        public Nullable<int> FkShUserType { get; set; }
        public Nullable<int> UserCreatedBy { get; set; }
        public Nullable<int> UserUpdatedBy { get; set; }
        public Nullable<int> UserDeletedBy { get; set; }
        public Nullable<int> DateCreated { get; set; }
        public Nullable<int> DateUpdated { get; set; }
        public Nullable<int> DateDeleted { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShFile> ShFiles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShFile> ShFiles1 { get; set; }
        public virtual ShUserType ShUserType { get; set; }
    }
}

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
    
    public partial class ShUserType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ShUserType()
        {
            this.ShUsers = new HashSet<ShUser>();
        }
    
        public int RecId { get; set; }
        public string Type { get; set; }
        public Nullable<int> UserCreatedBy { get; set; }
        public Nullable<int> UserUpdatedBy { get; set; }
        public Nullable<int> UserDeletedBy { get; set; }
        public Nullable<int> DateCreated { get; set; }
        public Nullable<int> DateUpdated { get; set; }
        public Nullable<int> DateDeleted { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShUser> ShUsers { get; set; }
    }
}
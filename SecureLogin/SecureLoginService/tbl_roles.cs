//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SecureLoginService
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_roles
    {
        public tbl_roles()
        {
            this.tbl_userinfo = new HashSet<tbl_userinfo>();
        }
    
        public string Role_id { get; set; }
        public string Role_desc { get; set; }
    
        public virtual ICollection<tbl_userinfo> tbl_userinfo { get; set; }
    }
}

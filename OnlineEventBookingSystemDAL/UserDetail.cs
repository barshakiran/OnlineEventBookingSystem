//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineEventBookingSystemDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserDetail()
        {
            this.BookingDetails = new HashSet<BookingDetail>();
        }
    
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Password { get; set; }
        public string User_PhoneNo { get; set; }
        public string User_Email { get; set; }
        public string User_Address { get; set; }
        public bool IsAdmin { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}

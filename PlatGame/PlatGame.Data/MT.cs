//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlatGame.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class MT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MT()
        {
            this.DNs = new HashSet<DN>();
        }
    
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> SubscriberId { get; set; }
        public string MtPurpose { get; set; }
        public decimal Price { get; set; }
        public string message { get; set; }
        public int MessagePartsCount { get; set; }
        public System.DateTime DateSent { get; set; }
        public string Status { get; set; }
        public string msisdn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DN> DNs { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PlatGames.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class TelcoInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TelcoInfo()
        {
            this.Subscriptions = new HashSet<Subscription>();
            this.SubscriptionHistories = new HashSet<SubscriptionHistory>();
            this.Transactions = new HashSet<Transaction>();
        }
    
        public int Id { get; set; }
        public string TelcoName { get; set; }
        public string Aggregator { get; set; }
        public decimal Shear { get; set; }
        public Nullable<int> Code { get; set; }
        public Nullable<int> CMId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Subscription> Subscriptions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionHistory> SubscriptionHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}

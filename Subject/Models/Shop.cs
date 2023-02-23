//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Subject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shop
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Shop()
        {
            this.ShopImage = new HashSet<ShopImage>();
            this.ShopMenu = new HashSet<ShopMenu>();
            this.ShopTag = new HashSet<ShopTag>();
            this.UserFeedback = new HashSet<UserFeedback>();
            this.UserSave = new HashSet<UserSave>();
            this.Pay = new HashSet<Pay>();
        }
    
        public int ShopNumber { get; set; }
        public string ShopName { get; set; }
        public string District { get; set; }
        public string ShopAddress { get; set; }
        public string ShopTime { get; set; }
        public string Phone { get; set; }
        public string Web { get; set; }
        public Nullable<bool> Outlet { get; set; }
        public Nullable<bool> WIFI { get; set; }
        public Nullable<bool> LimitedTime { get; set; }
        public Nullable<bool> IsOrder { get; set; }
        public Nullable<System.DateTime> ShopDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public int AdmNumber { get; set; }
        public Nullable<bool> Closed { get; set; }
    
        public virtual Adm Adm { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShopImage> ShopImage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShopMenu> ShopMenu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShopTag> ShopTag { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserFeedback> UserFeedback { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserSave> UserSave { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pay> Pay { get; set; }
    }
}

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
    
    public partial class ShopMenu
    {
        public int MenuNumber { get; set; }
        public int UserNumber { get; set; }
        public int ShopNumber { get; set; }
        public string Item { get; set; }
        public short Price { get; set; }
        public Nullable<System.DateTime> MenuDate { get; set; }
    
        public virtual Shop Shop { get; set; }
        public virtual Users Users { get; set; }
    }
}

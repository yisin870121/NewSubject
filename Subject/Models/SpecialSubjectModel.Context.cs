﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SpecialSubjectEntities : DbContext
    {
        public SpecialSubjectEntities()
            : base("name=SpecialSubjectEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Adm> Adm { get; set; }
        public virtual DbSet<Pay> Pay { get; set; }
        public virtual DbSet<PostShop> PostShop { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<ShopImage> ShopImage { get; set; }
        public virtual DbSet<ShopMenu> ShopMenu { get; set; }
        public virtual DbSet<ShopPay> ShopPay { get; set; }
        public virtual DbSet<ShopTag> ShopTag { get; set; }
        public virtual DbSet<Suggest> Suggest { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}

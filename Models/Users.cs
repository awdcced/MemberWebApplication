//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MemberWebApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.ExchangLogs = new HashSet<ExchangLogs>();
            this.TransferLogs = new HashSet<TransferLogs>();
        }
    
        public int U_ID { get; set; }
        public Nullable<int> S_ID { get; set; }
        public string U_LoginName { get; set; }
        public string U_Password { get; set; }
        public string U_RealName { get; set; }
        public string U_Sex { get; set; }
        public string U_Telephone { get; set; }
        public Nullable<int> U_Role { get; set; }
        public Nullable<bool> U_CanDelete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExchangLogs> ExchangLogs { get; set; }
        public virtual Shops Shops { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransferLogs> TransferLogs { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MasirPal
{
    using System;
    using System.Collections.Generic;
    
    public partial class C_Сертификат_качества__
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public C_Сертификат_качества__()
        {
            this.Продукция_ = new HashSet<Продукция_>();
        }
    
        public int Код_ { get; set; }
        public Nullable<int> Номер_сертификата { get; set; }
        public Nullable<System.DateTime> Дата_выдачи { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Продукция_> Продукция_ { get; set; }
    }
}

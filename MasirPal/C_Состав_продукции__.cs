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
    
    public partial class C_Состав_продукции__
    {
        public int Код { get; set; }
        public Nullable<int> Код_продукции { get; set; }
        public Nullable<int> Код_материала { get; set; }
    
        public virtual Материалы_ Материалы_ { get; set; }
        public virtual Продукция_ Продукция_ { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SewingFactory.Models
{
    using System;
    
    public partial class Select_Order_Result
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> DateOfCompletion { get; set; }
        public string Size { get; set; }
        public short Cost { get; set; }
        public Nullable<int> sum { get; set; }
        public Nullable<int> Done { get; set; }
        public Nullable<int> Status { get; set; }
        public string FullName { get; set; }
        public string Рабочая_группа { get; set; }
        public string Модель { get; set; }
    }
}

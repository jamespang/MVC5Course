namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Validations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        [Required(ErrorMessage = "請輸入商品名稱 ({0})")]
        [DisplayName("商品名稱")] // 示範修改 Model 的顯示欄位名稱
        [商品名稱不能有Will字串]
        public string ProductName { get; set; }

        [Required]
        [Range(10, 9999, ErrorMessage = "金額設定錯誤")]
        [DisplayFormat(DataFormatString = "{0:N0}")] // 將 Price 顯示為沒有小數點（在 Model 中實作）
        public Nullable<decimal> Price { get; set; }

        [Required]
        public Nullable<bool> Active { get; set; }

        [Required]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}

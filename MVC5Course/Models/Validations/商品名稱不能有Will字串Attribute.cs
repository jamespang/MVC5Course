using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.Validations
{
    public class 商品名稱不能有Will字串Attribute : DataTypeAttribute
    {
        public 商品名稱不能有Will字串Attribute() : base(DataType.Text)
        {
            this.ErrorMessage = "商品名稱不能有Will字串";
        }

        public override bool IsValid(object value)
        {
            //return base.IsValid(value);
            String str = Convert.ToString(value);
            if (str.Contains("will"))
            {
                return false;
            }
            return true;
        }
    }
}
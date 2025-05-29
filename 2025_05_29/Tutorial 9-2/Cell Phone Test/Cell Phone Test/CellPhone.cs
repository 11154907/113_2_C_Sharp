using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cell_Phone_Test
{
    class CellPhone
    {
        //儲存手機品牌的私有欄位
        private string _brand;

        //儲存手機型號的私有欄位   
        private string _model;

        //儲存手機價格的私有欄位
        private string _price;

        public CellPhone()
        {
            // 建構函式，初始化手機品牌、型號和價格
            _brand = "";
            _model = "";
            _price = 0m;
        }
         
        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }

        }

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public string Price
        {
            get { return _price; }
            set { _price = value; }
        }

        public decimal Price
        {
            get { return _price; }
            set
            {
                // 確保價格是非負數
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Price cannot be negative.");
                }
            }
        }
    }
}

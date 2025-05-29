using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coin_Toss
{
    class Coin
    {
        public string sideUp; // 硬幣正面朝上的一面
        Random rand = new Random(); //亂數產生器
        
        public Coin()
        {
            sideUp = "正面"; // 預設為正面
        }

        public void Toss()
        {
            // 產生0或1，決定硬幣的正反面
            if (rand.Next(2) == 0)
            {
                sideUp = "正面"; // 0代表正面
            }
            else
            {
                sideUp = "反面"; // 1代表反面
            }
        }

        public string GetsideUp()
        {
            // 返回硬幣正面朝上的一面
            return sideUp;
        }
    }
}

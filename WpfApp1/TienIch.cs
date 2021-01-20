using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public static class TienIch
    {
        public static string TaoMaTuDong(string s, string sMa) //s="17DH110798", sMa = "0798"
        {
            var so = int.Parse(sMa); //"0798" => 798
            so++; //799
            switch (sMa.Length)
            {
                case 1:
                    sMa = string.Format("{0:0}", so); //799 => "799"
                    break;
                case 2:
                    sMa = string.Format("{0:00}", so); //799 => "799"
                    break;
                case 3:
                    sMa = string.Format("{0:000}", so); //799 => "799"
                    break;
                case 4:
                    sMa = string.Format("{0:0000}", so); //799 => "0799"
                    break;
                case 5:
                    sMa = string.Format("{0:00000}", so); //799 => "00799"
                    break;
                case 6:
                    sMa = string.Format("{0:000000}", so); //799 => "000799"
                    break;
                case 7:
                    sMa = string.Format("{0:0000000}", so); //799 => "0000799"
                    break;
                case 8:
                    sMa = string.Format("{0:00000000}", so); //799 => "00000799"
                    break;
                case 9:
                    sMa = string.Format("{0:000000000}", so); //799 => "000000799"
                    break;
            }
            return s.Substring(0, s.Length - sMa.Length) + sMa;  //"17DH11"+"0799"
        }
        public static string MaHoaMD5(string s)
        {
            MD5 mh = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(s);
            //mã hóa chuỗi đã chuyển
            byte[] hash = mh.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
            //nếu bạn muốn các chữ cái in thường thay vì in hoa thì bạn 
            //thay chữ "X" in hoa trong "X2" thành "x"
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CacKieuDL
{
    class Program
    {
        static void Main(string[] args)
        {
            Nhap3SoNguyenDuong();
        }

        public static void Nhap3SoNguyenDuong()
        {

        }

        public static void NhapDanhSachTen()
        {
            List<String> ds = new List<string>();
            // Nhap vao mot gia tri nguyen > 0
            Console.Write("Moi ban nhap vao ten nhan vien dau tien: ");
            String a = Console.ReadLine();
            if (a.ToLower().Equals("exit"))
            {
                Console.WriteLine("Ban chua nhap thong tin nhan vien nao ca");
            }
            while (!a.ToLower().Equals("exit"))
            {
                ds.Add(a);
                Console.Write("Moi ban nhap vao ten nhan vien tiep theo: ");
                a = Console.ReadLine();
            }
            Console.WriteLine("Danh sach da nhap gom " + ds.Count.ToString() + " nhan vien:");
            foreach (String ten in ds)
            {
                Console.WriteLine(ten);
            }
            Console.Read();
        }
    }
}

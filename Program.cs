/*****************************************
 * Mã sinh viên: 202418836
 * Họ tên      : Nguyễn Vũ Quang Anh
 *****************************************/
using System;
using System.Collections.Generic;

namespace DeviceManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== KHỞI TẠO DỮ LIỆU ===");
          
            Computer pc1 = new Computer("PC01", "Dell OptiPlex", 2023, 15000000m, 16, "Core i5", false, Device.DeviceStatus.Active);
            Computer pc2 = new Computer("PC02", "ASUS ROG", 2019, 30000000m, 32, "Core i7", true, Device.DeviceStatus.UnderMaintenance);

            Printer pr1 = new Printer("PR01", "Canon LBP2900", 2024, 4000000m, "laser", 20000, false);
            NetworkPrinter pr2 = new NetworkPrinter("PR02", "HP LaserJet", 2020, 12000000m, "laser", 120000, true, "192.168.1.50");

            Projector pj1 = new Projector("PJ01", "Sony VPL", 2021, 10000000m, 4000, 3500);

            LabRoom lab1 = new LabRoom("LAB01", "Phòng Máy Tính 1", 30);
            LabRoom lab2 = new LabRoom("LAB02", "Phòng Đa Phương Tiện 2", 20);

            Console.WriteLine("Đã khởi tạo thành công thiết bị và phòng học.\n");

            // =========================================================
            // TEST CASE 1: Thêm thiết bị vào phòng
            // =========================================================
            Console.WriteLine("--- 1. TEST THÊM THIẾT BỊ ---");
            lab1.AddDevice(pc1);
            lab1.AddDevice(pc2);
            lab1.AddDevice(pr1);
            lab2.AddDevice(pr2);
            lab2.AddDevice(pj1);
            Console.WriteLine("Đã thêm PC01, PC02, PR01 vào LAB01.");
            Console.WriteLine("Đã thêm PR02, PJ01 vào LAB02.\n");

            // =========================================================
            // TEST CASE 2: Bắt lỗi trùng mã
            // =========================================================
            Console.WriteLine("--- 2. TEST BẮT LỖI TRÙNG MÁ ---");
            try
            {
                Computer pcTrung = new Computer("PC01", "Máy tính Fake", 2024, 10000000m, 8, "i3", false);
                lab1.AddDevice(pcTrung); // PC01 đã có trong lab1
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Bắt lỗi thành công: " + ex.Message + "\n");
            }

            // =========================================================
            // TEST CASE 3: In danh sách phòng
            // =========================================================
            Console.WriteLine("--- 3. THÔNG TIN PHÒNG THỰC HÀNH ---");
            Console.WriteLine(lab1.ToString());
            Console.WriteLine(lab2.ToString() + "\n");

            // =========================================================
            // TEST CASE 4: Tính tổng chi phí bảo trì (Sử dụng đa hình)
            // =========================================================
            Console.WriteLine("--- 4. TỔNG CHI PHÍ BẢO TRÌ DỰ KIẾN ---");
            Console.WriteLine($"LAB01: {lab1.CalculateAnnualMaintenanceCost():N0} VNĐ");
            Console.WriteLine($"LAB02: {lab2.CalculateAnnualMaintenanceCost():N0} VNĐ\n");

            // =========================================================
            // TEST CASE 5: Liệt kê thiết bị cần bảo trì (> 5 năm hoặc lỗi)
            // =========================================================
            Console.WriteLine("--- 5. DANH SÁCH THIẾT BỊ CẦN BẢO TRÌ GẤP ---");
            Device[] maintLab1 = lab1.GetDevicesRequiringMaintenance();
            Console.WriteLine($"Phòng LAB01 ({maintLab1.Length} thiết bị):");
            foreach (var dev in maintLab1)
            {
                Console.WriteLine("  + " + dev.ToString());
            }

            Device[] maintLab2 = lab2.GetDevicesRequiringMaintenance();
            Console.WriteLine($"Phòng LAB02 ({maintLab2.Length} thiết bị):");
            foreach (var dev in maintLab2)
            {
                Console.WriteLine("  + " + dev.ToString());
            }
            Console.WriteLine();

            // =========================================================
            // TEST CASE 6: Kết nối mạng
            // =========================================================
            Console.WriteLine("--- 6. TEST KẾT NỐI MẠNG ---");
            pc1.Connect("192.168.1.101");
            pc2.Connect("192.168.1.102");
            
            // Ngắt kết nối pr2 trước khi gán IP mới để tránh lỗi InvalidOperationException
            pr2.Disconnect(); 
            pr2.Connect("192.168.1.103");
            Console.WriteLine("Đã cấp IP mới thành công cho các thiết bị mạng.\n");

            // =========================================================
            // TEST CASE 7: Duyệt thiết bị mạng qua Interface INetworkable
            // =========================================================
            Console.WriteLine("--- 7. KIỂM TRA TRẠNG THÁI MẠNG (QUA INTERFACE) ---");
            Device[] allDevices = new Device[] { pc1, pc2, pr1, pr2, pj1 };
            
            foreach (Device dev in allDevices)
            {
                // Chỉ xử lý những thiết bị có triển khai INetworkable
                if (dev is INetworkable networkDevice)
                {
                    string status = networkDevice.IsConnected ? $"Kết nối (IP: {networkDevice.IpAddress})" : "Ngắt kết nối";
                    Console.WriteLine($"- {dev.DeviceName} ({dev.DeviceID}) -> Trạng thái mạng: {status}");
                }
            }
            
            Console.WriteLine("\n=== HOÀN TẤT KIỂM THỬ ===");
        }
    }
}
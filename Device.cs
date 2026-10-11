using System;

namespace DeviceManagement
{

    public abstract class Device
    {
        public string DeviceID { get; protected set; }
        public string DeviceName { get; protected set; }
        public int UsageYear { get; protected set; }
        public decimal PurchasePrice { get; protected set; }

        public enum DeviceStatus
        {
            Active,
            UnderMaintenance,
            Retired
        }

        public DeviceStatus Status { get; protected set; }

        public Device(string deviceID, string deviceName, int usageYear, decimal purchasePrice, DeviceStatus status = DeviceStatus.Active)
        {
            if (deviceID == null || deviceID == "")
            {
                throw new ArgumentException("Mã thiết bị không rỗng");
            }
            if (deviceName == null || deviceName == "")
            {
                throw new ArgumentException("Tên thiết bị không rỗng");
            }
            if (usageYear > 2026)
            {
                throw new ArgumentException("Năm sử dụng không lớn hơn năm hiện tại");
            }
            if (purchasePrice <= 0)
            {
                throw new ArgumentException("Giá mua lớn hơn 0");
            }

           this.DeviceID = deviceID;
           this.DeviceName = deviceName;
           this.UsageYear = usageYear;
           this.PurchasePrice = purchasePrice;
           this.Status = status;
        }

        public abstract decimal CalculateAnnualMaintenanceCost();

        public override string ToString()
        {
           return $"Mã: {DeviceID} - Tên: {DeviceName} - Năm sử dụng: {UsageYear} - Giá mua: {PurchasePrice} - Trạng thái: {Status}";
        }        
        
    }

}
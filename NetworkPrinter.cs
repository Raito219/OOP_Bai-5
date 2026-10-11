using System;

namespace DeviceManagement
{
    public class NetworkPrinter : Printer, INetworkable
    {
        private string ipAddress = "";
        private bool isConnected = false;

        public string IpAddress => ipAddress;
        public bool IsConnected => isConnected;

        public NetworkPrinter(string deviceID, string deviceName, int usageYear, decimal purchasePrice, string printType, int pageCount, bool hasNetworkSupport, string ipAddress, DeviceStatus status = DeviceStatus.Active)
            : base(deviceID, deviceName, usageYear, purchasePrice, printType, pageCount, hasNetworkSupport, status)
        {
            if (!string.IsNullOrEmpty(ipAddress)) Connect(ipAddress);
        }

        public void Connect(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress)) throw new ArgumentException("IP rỗng");
            if (this.isConnected) throw new InvalidOperationException("Đang kết nối");
            this.ipAddress = ipAddress;
            this.isConnected = true;
        }

        public void Disconnect()
        {
            this.ipAddress = "";
            this.isConnected = false;
        }

        public override string ToString()
        {
            return base.ToString() + (isConnected ? $" - IP: {ipAddress}" : " - Ngắt kết nối");
        }
    }
}
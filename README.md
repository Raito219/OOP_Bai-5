# BÀI TẬP TUẦN 5: HỆ THỐNG QUẢN LÝ THIẾT BỊ PHÒNG THỰC HÀNH
- **Mã sinh viên:** 202418836
- **Họ tên:** Nguyễn Vũ Quang Anh
- **Ngôn ngữ:** C# (tương thích C# 5 / .NET Framework 4 và .NET 10)

---

## 1. Mục tiêu kiến thức áp dụng
Dự án được xây dựng nhằm củng cố các kỹ năng OOP cốt lõi:
*   **Kế thừa (Inheritance):** Xây dựng hệ thống phân cấp thiết bị logic (is-a).
*   **Lớp trừu tượng (Abstract Class):** Định nghĩa khung chuẩn (blueprint) cho các thiết bị.
*   **Giao diện (Interface):** Định nghĩa khả năng kết nối mạng (can-do) tách biệt với cây kế thừa.
*   **Đa hình (Polymorphism):** Tính toán chi phí bảo trì động mà không cần ép kiểu cứng (if/switch).
*   **Kết tập (Aggregation):** Quản lý mối quan hệ giữa Phòng thực hành (`LabRoom`) và Thiết bị (`Device`).
*   **Đóng gói & Kiểm chứng dữ liệu:** Đảm bảo tính toàn vẹn của dữ liệu đầu vào.

---

## 2. Bối cảnh nghiệp vụ
Trường đại học cần một phần mềm để quản lý vòng đời và chi phí bảo trì của các thiết bị công nghệ. 
*   **Phân loại:** Hiện tại hỗ trợ `Computer` (Máy tính), `Printer` (Máy in), và `Projector` (Máy chiếu).
*   **Chi phí:** Mỗi thiết bị có một công thức tính chi phí bảo trì hàng năm khác nhau.
*   **Mạng lưới:** Máy tính bắt buộc có mạng, máy in tuỳ chọn, máy chiếu hiện tại chưa có.
*   **Quản lý vật lý:** Thiết bị được phân bổ vào các phòng thực hành (`LabRoom`). Tuy nhiên, thiết bị tồn tại độc lập và có thể chưa thuộc về phòng nào.

---

## 3. Kiến trúc hệ thống

### 3.1. Lớp cốt lõi: `Device` (Abstract)
Lớp nền tảng cho mọi thiết bị trong hệ thống.
*   **Thuộc tính cơ bản:** `DeviceID` (Mã, chỉ định một lần), `DeviceName` (Tên), `UsageYear` (Năm bắt đầu dùng), `PurchasePrice` (Giá mua), `Status` (Trạng thái: Active / UnderMaintenance / Retired).
*   **Ràng buộc:** Mã không rỗng, giá mua $> 0$, năm sử dụng $\le$ năm hiện tại.
*   **Phương thức trừu tượng:** `CalculateAnnualMaintenanceCost()` (Tính chi phí bảo trì dự kiến).

### 3.2. Các thực thể thiết bị (Derived Classes)

#### `Computer`
*   **Mở rộng:** RAM, Loại CPU, Có GPU rời hay không.
*   **Tính phí bảo trì:** 
    *   Cơ bản: $5\%$ giá mua.
    *   Phụ phí: $+2\%$ nếu có GPU rời, $+1\%$ nếu tuổi thọ $> 5$ năm.

#### `Printer`
*   **Mở rộng:** Loại in (Laser/Ink), Có in màu không, Số trang đã in, Hỗ trợ mạng.
*   **Tính phí bảo trì:**
    *   Cơ bản: $4\%$ giá mua.
    *   Phụ phí: $+500.000\text{ VNĐ}$ nếu đã in $> 100.000$ trang, $+300.000\text{ VNĐ}$ nếu là máy in màu.

#### `Projector`
*   **Mở rộng:** Độ sáng (Lumens), Thời gian dùng bóng đèn (Giờ).
*   *(Tuỳ chọn thiết kế: Bổ sung tuổi thọ bóng đèn `LampLifeExpectancy` để linh hoạt hơn thay vì hardcode 3000 giờ).*
*   **Tính phí bảo trì:**
    *   Cơ bản: $3\%$ giá mua.
    *   Phụ phí: $+1.500.000\text{ VNĐ}$ nếu bóng đèn đã dùng $> 3.000$ giờ.

---

## 4. Quản lý kết nối mạng (`INetworkable`)
Hệ thống sử dụng interface `INetworkable` để cấp quyền truy cập mạng cho các thiết bị đủ điều kiện.
*   **Tính năng:** Đọc IP (`IpAddress`), Trạng thái (`IsConnected`), Kết nối (`Connect`), Ngắt (`Disconnect`).
*   **Quy tắc:** Không kết nối nếu IP rỗng hoặc đang có kết nối sẵn. Xoá IP khi ngắt.
*   **Giải pháp cho Máy in có mạng (Trade-off):** Hệ thống sử dụng phương án tạo lớp `NetworkPrinter` (kế thừa `Printer`, thực thi `INetworkable`). 
    > *Lý do: Tận dụng được tính đa hình và kiểm tra kiểu an toàn tại thời điểm biên dịch (compile-time), ngăn chặn việc gọi hàm Connect() trên các máy in không hỗ trợ. Nhược điểm là rủi ro tăng số lượng class nếu sau này chia nhỏ nhiều loại máy in.*

---

## 5. Quản lý phòng thực hành (`LabRoom`)
Lớp quản lý tập hợp các thiết bị.
*   **Chức năng:** Thêm, Xoá, Tìm kiếm thiết bị theo mã.
*   **Tính toán:** Cung cấp phương thức `CalculateAnnualMaintenanceCost()` để tính tổng chi phí của cả phòng dựa trên tính Đa hình của các `Device`.
*   **Cảnh báo bảo trì:** Phương thức `GetDevicesRequiringMaintenance()` lọc ra các thiết bị đang lỗi (`UnderMaintenance`) hoặc đã dùng quá 5 năm.

 **Quyết định thiết kế:** Quan hệ giữa `LabRoom` và `Device` là **Kết tập (Aggregation)**. Thiết bị được tạo ra độc lập, sau đó mới `AddDevice()` vào phòng. Nếu phòng bị giải thể, thiết bị vẫn nằm trong kho dữ liệu của trường chờ điều phối.

---

## 6. Kịch bản chạy thử (Test Cases)
File `Program.cs` chạy một kịch bản hoàn chỉnh để chứng minh hệ thống hoạt động:
1.  Khởi tạo danh sách thiết bị mẫu (2 PC, 2 Máy in, 1 Máy chiếu). Đảm bảo điều kiện kích hoạt các phụ phí bảo trì (như PC có GPU, máy in $> 100k$ trang).
2.  Khởi tạo 2 phòng Lab và phân bổ thiết bị.
3.  Test chặn thêm thiết bị trùng mã định danh.
4.  In báo cáo danh sách thiết bị và tổng chi phí bảo trì dự kiến cho từng phòng.
5.  Xuất danh sách các thiết bị đang cần bảo trì gấp.
6.  Demo tính năng kết nối IP, chặn kết nối đè, và ngắt kết nối thông qua đa hình interface `INetworkable`.


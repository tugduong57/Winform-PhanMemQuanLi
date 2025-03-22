# Quản Lý Đại Lý Bán Hàng

## Giới Thiệu

Phần mềm **Quản Lý Đại Lý Bán Hàng** được xây dựng nhằm giúp chủ cửa hàng và nhân viên dễ dàng quản lý thông tin hàng hóa, đối tác, hóa đơn giao dịch cũng như báo cáo thống kê về tình hình kinh doanh. Hệ thống được thiết kế với giao diện thân thiện, hướng tới đối tượng là những người không thông thạo tin học văn phòng, đảm bảo dữ liệu được liên kết logic và hỗ trợ đầy đủ các chức năng quản lý (thêm, sửa, xóa, thống kê).

## Mục Tiêu Dự Án

- **Tối ưu hóa quản lý kho hàng:** Hiển thị danh sách hàng hóa, tìm kiếm và lọc dữ liệu nhanh chóng.
- **Quản lý thông tin đối tác:** Hỗ trợ quản lý thông tin khách hàng và nhà cung cấp.
- **Xử lý hóa đơn:** Quản lý hóa đơn nhập, xuất cùng với việc cập nhật số liệu kho và lịch sử giao dịch.
- **Báo cáo thống kê:** Cung cấp báo cáo chi tiết về doanh thu, lợi nhuận qua biểu đồ và bảng tóm tắt.
- **Quản lý bảng giá:** Điều chỉnh giá bán sản phẩm và tự động cập nhật trên các chức năng liên quan.
- **Quản lý tài khoản:** Kiểm soát quyền truy cập và thông tin người dùng (chủ cửa hàng, nhân viên).

## Đối Tượng Sử Dụng

- **Chủ cửa hàng:** Có quyền truy cập đầy đủ tất cả các chức năng quản lý.
- **Nhân viên:** Chỉ được phép truy cập các chức năng cơ bản như quản lý kho hàng, đối tác và hóa đơn xuất.

## Chức Năng Chính

1. **Quản Lý Kho Hàng:**  
   - Hiển thị danh sách sản phẩm với các thông tin chi tiết (mã, tên, số lượng, giá bán, phân loại, đơn vị, ...).
   - Tích hợp thanh tìm kiếm và lọc dữ liệu tự động.
   - Hỗ trợ chỉnh sửa ghi chú cho sản phẩm khi cần thiết.

2. **Quản Lý Bảng Giá:**  
   - Cho phép chủ cửa hàng thay đổi giá bán sản phẩm.
   - Tự động cập nhật giá mới cho các chức năng liên quan.
   - Hỗ trợ xuất bảng giá hiện tại dưới dạng PDF.

3. **Quản Lý Báo Cáo Thống Kê:**  
   - Cung cấp báo cáo doanh thu, lợi nhuận dựa trên các tiêu chí thời gian, đối tác, nhân viên và loại sản phẩm.
   - Hiển thị biểu đồ thống kê với các lựa chọn hiển thị theo ngày, tháng hoặc năm.
   - Hỗ trợ xuất báo cáo chi tiết dưới dạng file PDF.

4. **Quản Lý Hóa Đơn:**  
   - Hỗ trợ quản lý hóa đơn xuất kho và hóa đơn nhập kho.
   - Cập nhật tự động thông tin kho hàng và lịch sử giao dịch sau mỗi hóa đơn.
   - Cho phép xem lịch sử hóa đơn và chi tiết hóa đơn.

5. **Quản Lý Tài Khoản:**  
   - Tạo mới, sửa và xóa tài khoản cho nhân viên.
   - Kiểm soát quyền truy cập cho từng nhóm người dùng.

6. **Quản Lý Thông Tin Đối Tác:**  
   - Quản lý thông tin khách hàng và nhà cung cấp.
   - Hỗ trợ các thao tác thêm, sửa, xóa và tìm kiếm thông tin đối tác.

## Hướng Dẫn Cài Đặt

1. **Yêu Cầu Hệ Thống:**  
   - Hệ điều hành: Windows
   - Cơ sở dữ liệu: SQL Server 
   - .NET Framework 

2. **Cài Đặt:**  
   - **Bước 1:** Clone repository từ GitHub.  
   - **Bước 2:** Kết nối cơ sở dữ liệu: kết nối, tạo, chèn dữ liệu mẫu với 3 file python, thư viện pyodbc  
   - **Bước 3:** Biên dịch và chạy ứng dụng qua Visual Studio hoặc công cụ phát triển tương thích.

## Hướng Dẫn Sử Dụng

- **Đăng Nhập:**  
  Sử dụng tài khoản đã được cấp để đăng nhập vào hệ thống (admin, admin).

- **Sử Dụng Chức Năng:**  
  - **Kho hàng:** Tra cứu thông tin sản phẩm.
  - **Bảng giá:** Cập nhật và điều chỉnh giá bán.
  - **Hóa đơn:** Thực hiện các giao dịch nhập, xuất và quản lý lịch sử hóa đơn.
  - **Báo cáo:** Xem và xuất báo cáo thống kê kinh doanh.
  - **Tài khoản & Đối tác:** Quản lý thông tin người dùng và đối tác kinh doanh.

## Công Nghệ Sử Dụng

- **Ngôn ngữ lập trình:** C# (.NET Framework)
- **Cơ sở dữ liệu:** SQL Server
- **Các thư viện hỗ trợ:**  
  - Xuất PDF: iTextSharp
  - Python-SQL: pyodbc

## Liên Hệ

- **Nhóm Thực Hiện:**  
  - Nguyễn Tùng Dương  
  - Phạm Quang Anh

- **Giảng Viên Hướng Dẫn:**  
  - Nguyễn Thị Phương Dung

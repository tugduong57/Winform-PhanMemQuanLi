import pyodbc
def C_database():

	conn_string = (
	    "DRIVER={ODBC Driver 17 for SQL Server};"
	    "SERVER=localhost;"  		
	    "DATABASE=master;"  
	    "Trusted_Connection=yes;"  	
	)

	conn = pyodbc.connect(conn_string, autocommit=True)	

	# Tạo con trỏ để thực hiện các câu lệnh SQL
	cursor = conn.cursor()								

	database_name = 'dataForProject'			

	# Sử dụng cơ sở dữ liệu
	use_db = f'USE {database_name}'
	cursor.execute(use_db)

	# Clear dữ liệu
	cursor.execute("DELETE FROM SanPham;")

	# Bảng Sản Phẩm
	SanPham = '''
		[Mã sản phẩm] varchar(10) not null primary key,
		[Tên sản phẩm] nvarchar(MAX) not null,
		Hãng nvarchar(10) not null,
		[Phân loại] nvarchar(20) not null,
		[Ghi chú] nvarchar(MAX)
	'''

	# Danh sách dữ liệu
	san_pham_list = [
    ('SP001', 'Bột trét trong nhà cao cấp', 'Mykolor', 'Bột trét', ''),
    ('SP002', 'Bột trét ngoài nhà cao cấp', 'Mykolor', 'Bột trét', ''),
    ('SP003', 'Bột trét nội & ngoại thất', 'Mykolor', 'Bột trét', ''),
    ('SP004', 'Sơn lót sinh học nội thất', 'Mykolor', 'Sơn lót chống kiềm', ''),
    ('SP005', 'Sơn lót chống thấm ngược cao cấp', 'Mykolor', 'Sơn lót chống kiềm', ''),
    ('SP006', 'Sơn lót chống kiềm ngoại thất cao cấp', 'Mykolor', 'Sơn lót chống kiềm', ''),
    ('SP007', 'Sơn nước nội thất', 'Mykolor','Sơn nội thất', 'Màu trắng 111'),
    ('SP008', 'Sơn nội thất cao cấp', 'Mykolor', 'Sơn nội thất', 'Màu trắng 111'),
    ('SP009', 'Sơn ngoại thất cao cấp chống bám bẩn, chống phai màu', 'Mykolor', 'Sơn ngoại thất', ''),
	]

	# Câu lệnh SQL
	sql = "INSERT INTO SanPham ([Mã sản phẩm], [Tên sản phẩm], Hãng, [Phân loại], [Ghi chú]) VALUES (?, ?, ?, ?, ?)"

	# Chèn dữ liệu hàng loạt
	cursor.executemany(sql, san_pham_list)
	conn.commit()

	cursor.execute("DELETE FROM dvtSanPham;")

	# Bảng Đơn vị tính của từng sản phẩm
	create_dvtSanPham = '''
		[Mã sản phẩm] varchar(10) not null,
		[Đơn vị tính] nvarchar(10) not null,
		[Số lượng] smallint not null,
		[Giá nhập trung bình] decimal(18,2) not null,
		[Đơn giá] decimal(18,2) not null,
		PRIMARY KEY ([Mã sản phẩm], [Đơn vị tính]),
		CONSTRAINT FK_dvtSanPham_SanPham FOREIGN KEY ([Mã sản phẩm])
			REFERENCES SanPham([Mã sản phẩm]) ON DELETE CASCADE

	'''

	# Danh sách dữ liệu
	san_pham_list = [
    ('SP001', '20 kg', '10', '400000', '545000'),
    ('SP002', '20 kg', '50', '500000', '683000'),
    ('SP003', '40 kg', '20', '600000', '736000'),
    ('SP004', '4.375L', '10', '1000000', '1193000'),
    ('SP004', '18L', '10', '4000000', '4510000'),
    ('SP005', '4.375L', '10', '1500000', '1613000'),
    ('SP006', '4.375L', '10', '1200000', '1316000'),
    ('SP006', '18L', '10', '4000000', '4601000'),
    ('SP007', '4.375L', '10', '500000', '768000'),
    ('SP007', '18L', '10', '2000000', '2442000'),
    ('SP008', '4.375L', '10', '800000', '973000'),
    ('SP008', '18L', '10', '3000000', '3506000'),
    ('SP009', '0.875L', '10', '700000', '918000'),
    ('SP009', '4.375L', '10', '3000000', '3520000'),
	]

	# Câu lệnh SQL
	sql = "INSERT INTO dvtSanPham ([Mã sản phẩm], [Đơn vị tính], [Số lượng], [Giá nhập trung bình], [Đơn giá]) VALUES (?, ?, ?, ?, ?)"

	# Chèn dữ liệu hàng loạt
	cursor.executemany(sql, san_pham_list)

	cursor.execute("DELETE FROM DoiTac;")

	# Tạo bảng Đối tác
	create_DoiTac = '''
		[Mã đối tác] varchar(10) not null primary key,
		[Tên đối tác] nvarchar(MAX) not null,
		[Phân loại] nvarchar(15) not null,
		Tuổi tinyint not null,
		[Địa chỉ] nvarchar(MAX) not null,
		[Số điện thoại] varchar(20) not null,
		[Ghi chú] nvarchar(MAX)
	'''

	# Danh sách dữ liệu
	san_pham_list = [
    ('Dt001', 'Anh Duy Phùng Xá', 'Khách hàng', '35', 'Phùng Xá, Thạch Thất', '0123456789', 'ghichu'),
    ('Dt002', 'Anh Nam Bình Yên', 'Khách hàng', '33', 'Phùng Xá, Thạch Thất', '0123456789', 'ghichu'),
    ('Dt003', 'Anh chị Trung Oanh', 'Khách hàng', '32', 'Vĩnh Lộc', '0123456789', 'ghichu'),
    ('Dt004', 'Em Dũng Hữu Bằng', 'Khách hàng', '31', 'Hữu Bằng, Thạch Thất', '0123456789', 'ghichu'),
    ('Dt005', 'Anh Nam', 'Khách hàng', '30', 'Phùng Xá, Thạch Thất', '0123456789', 'ghichu'),
    ('Dt006', 'Tổng công ty', 'Nhà cung cấp', '25', 'Long An, Việt Nam', '0272 377 9601', 'ghichu'),
	]

	# Câu lệnh SQL
	sql = "INSERT INTO DoiTac ([Mã đối tác], [Tên đối tác], [Phân loại], Tuổi, [Địa chỉ], [Số điện thoại], [Ghi chú]) VALUES (?, ?, ?, ?, ?, ?, ?)"

	# Chèn dữ liệu hàng loạt
	cursor.executemany(sql, san_pham_list)



	cursor.execute("DELETE FROM NguoiDung;")

	# Tạo bảng Người dùng
	create_NguoiDung = '''
		[Tài khoản] varchar(20) not null primary key,
		[Mật khẩu] nvarchar(15) not null,
		[Tên người dùng] nvarchar(20),
		[Ghi chú] nvarchar(MAX)
	'''

	# Danh sách dữ liệu
	san_pham_list = [
    ('admin', 'admin', 'Nguyễn Tùng Dương', 'ghichu'),
    ('abcxyz', '1', 'Nguyễn Văn A', 'ghichu'),
    ('a', '0', 'Nguyễn Văn B', 'ghichu'),
    ('b', '0', 'Nguyễn Văn Trường', 'ghichu'),
    ('s', '0', 'Nguyễn Văn Huỳnh', 'ghichu'),
    ('d', '0', 'Phạm Quang Anh', 'ghichu'),
	]

	# Câu lệnh SQL
	sql = "INSERT INTO NguoiDung ([Tài khoản], [Mật khẩu], [Tên người dùng], [Ghi chú]) VALUES (?, ?, ?, ?)"

	# Chèn dữ liệu hàng loạt
	cursor.executemany(sql, san_pham_list)

	# Tạo bảng Hóa đơn
	create_HoaDon = '''
		if not exists ( select * from sysobjects where name = 'HoaDon' and xtype = 'U')
		begin
			create table HoaDon (
				[Mã hóa đơn] varchar(10) not null primary key,
				[Mã đối tác] varchar(10) not null,
				[Mã người bán] varchar(20) not null,
				[Ngày tạo] datetime not null,
				[Loại hóa đơn] nvarchar(5) not null,
				[Tổng tiền] decimal(18,2) not null,
				[Ghi chú] nvarchar(MAX),
				CONSTRAINT FK_HoaDon_DoiTac FOREIGN KEY ([Mã đối tác])
					REFERENCES DoiTac([Mã đối tác]) ON DELETE CASCADE,
				CONSTRAINT FK_HoaDon_NguoiDung FOREIGN KEY ([Mã người bán])
					REFERENCES NguoiDung([Tài khoản]) ON DELETE CASCADE
			)
		end
	'''

	cursor.execute(create_HoaDon)

	'''
		Foregin key: Rằng buộc khóa ngoại, ON Delete cascde: xóa các dòng khóa ngoại khi thông tin gốc bị xóa
		ON Update Cascade: cập nhật khi mã References cập nhật ?
	'''

	# Tạo bảng Chi tiết Hóa đơn
	create_ChiTietHoaDon = '''
		if not exists ( select * from sysobjects where name = 'ChiTietHoaDon' and xtype = 'U')
		begin
			create table ChiTietHoaDon (
				[Mã hóa đơn] varchar(10) not null,
				[Mã sản phẩm] varchar(10) not null,
				[Số lượng] smallint not null,
				[Đơn giá] decimal(18,2) not null,
				PRIMARY KEY ([Mã hóa đơn], [Mã sản phẩm]),
				CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY ([Mã hóa đơn])
					REFERENCES HoaDon([Mã hóa đơn]) ON DELETE CASCADE,
				CONSTRAINT FK_ChiTietHoaDon_SanPham FOREIGN KEY ([Mã sản phẩm])
					REFERENCES SanPham([Mã sản phẩm]) ON DELETE CASCADE
			)
		end
	'''

	cursor.execute(create_ChiTietHoaDon)

	print("Done!")

	cursor.close()

def Delete_database():
    conn_string = (
        "DRIVER={ODBC Driver 17 for SQL Server};"
        "SERVER=localhost;"  		
        "DATABASE=master;"  
        "Trusted_Connection=yes;"  	
    )

    conn = pyodbc.connect(conn_string, autocommit=True)	
    cursor = conn.cursor()								

    database_name = 'dataForProject'

    # Kiểm tra xem database có tồn tại không, nếu có thì xóa
    drop_db_query = f'''
        IF EXISTS (SELECT * FROM sys.databases WHERE name = '{database_name}')
        BEGIN
            ALTER DATABASE [{database_name}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            DROP DATABASE [{database_name}];
        END
    '''
    
    try:
        cursor.execute(drop_db_query)
        print(f"Database '{database_name}' đã bị xóa thành công!")
    except Exception as e:
        print(f"Lỗi khi xóa database: {e}")

    cursor.close()
    conn.close()


# Tạo database
C_database()

# Xóa database
# Delete_database()







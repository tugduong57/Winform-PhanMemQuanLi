import pyodbc
def insert_TestData():

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

#---------------------------------------------------------------------------------------------------------------------------------------
#	BẢNG SẢN PHẨM
#---------------------------------------------------------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------------------------------------------------------

	# Clear dữ liệu
	cursor.execute("DELETE FROM SanPham;")

	# Bảng Sản Phẩm
	SanPham = '''
		[Mã sản phẩm] varchar(10) not null primary key,
		[Tên sản phẩm] nvarchar(MAX) not null,
		Hãng nvarchar(10) not null,
		[Phân loại] nvarchar(20) not null,
	'''

	# Danh sách dữ liệu
	san_pham_list = [
	('SP001', 'Bột trét trong nhà cao cấp', 'Mykolor', 'Bột trét'),
	('SP002', 'Bột trét ngoài nhà cao cấp', 'Hang2', 'Bột trét'),
	('SP003', 'Bột trét nội & ngoại thất', 'Mykolor', 'Bột trét',),
	('SP004', 'Sơn lót sinh học nội thất', 'Hang3', 'Sơn lót chống kiềm',),
	('SP005', 'Sơn lót chống thấm ngược cao cấp', 'Mykolor', 'Sơn lót chống kiềm',),
	('SP006', 'Sơn lót chống kiềm ngoại thất cao cấp', 'Mykolor', 'Sơn lót chống kiềm',),
	('SP007', 'Sơn nước nội thất', 'Mykolor','Sơn nội thất', ),
	('SP008', 'Sơn nội thất cao cấp', 'Mykolor', 'Sơn nội thất', ),
	('SP009', 'Sơn ngoại thất cao cấp chống bám bẩn, chống phai màu', 'Mykolor', 'Sơn ngoại thất',),
	]

	# Câu lệnh SQL
	sql = "INSERT INTO SanPham ([Mã sản phẩm], [Tên sản phẩm], Hãng, [Phân loại]) VALUES (?, ?, ?, ?)"

	# Chèn dữ liệu hàng loạt
	cursor.executemany(sql, san_pham_list)
	conn.commit()

#---------------------------------------------------------------------------------------------------------------------------------------
#	BẢNG ĐƠN VỊ TÍNH
#---------------------------------------------------------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------------------------------------------------------

	cursor.execute("DELETE FROM dvtSanPham;")

	# Bảng Đơn vị tính của từng sản phẩm
	create_dvtSanPham = '''
		[Mã sản phẩm] varchar(10) not null,
		[Đơn vị tính] nvarchar(10) not null,
		[Số lượng] smallint not null,
		[Giá nhập trung bình] decimal(18,2) not null,
		[Đơn giá] decimal(18,2) not null,
		[Ghi chú] nvarchar(MAX),
		PRIMARY KEY ([Mã sản phẩm], [Đơn vị tính]),
		CONSTRAINT FK_dvtSanPham_SanPham FOREIGN KEY ([Mã sản phẩm])
			REFERENCES SanPham([Mã sản phẩm]) ON DELETE CASCADE

	'''

	# Danh sách dữ liệu
	san_pham_list = [
	('SP001', '20 kg', '10', '400000', '545000', ""),
	('SP002', '20 kg', '50', '500000', '683000', ""),
	('SP003', '40 kg', '20', '600000', '736000', ""),
	('SP004', '4.375L', '10', '1000000', '1193000', ""),
	('SP004', '18L', '10', '4000000', '4510000', ""),
	('SP005', '4.375L', '10', '1500000', '1613000', ""),
	('SP006', '4.375L', '10', '1200000', '1316000', ""),
	('SP006', '18L', '10', '4000000', '4601000', ""),
	('SP007', '4.375L', '10', '500000', '768000', ""),
	('SP007', '18L', '10', '2000000', '2442000', ""),
	('SP008', '4.375L', '10', '800000', '973000', ""),
	('SP008', '18L', '10', '3000000', '3506000', ""),
	('SP009', '0.875L', '10', '700000', '918000', ""),
	('SP009', '4.375L', '10', '3000000', '3520000', ""),
	]

	# Câu lệnh SQL
	sql = "INSERT INTO dvtSanPham ([Mã sản phẩm], [Đơn vị tính], [Số lượng], [Giá nhập trung bình], [Đơn giá], [Ghi chú]) VALUES (?, ?, ?, ?, ?, ?)"

	# Chèn dữ liệu hàng loạt
	cursor.executemany(sql, san_pham_list)

	cursor.execute("DELETE FROM DoiTac;")

#---------------------------------------------------------------------------------------------------------------------------------------
#	BẢNG ĐỐI TÁC
#---------------------------------------------------------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------------------------------------------------------


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


#---------------------------------------------------------------------------------------------------------------------------------------
#	BẢNG NGƯỜI DÙNG
#---------------------------------------------------------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------------------------------------------------------


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

#---------------------------------------------------------------------------------------------------------------------------------------
#	BẢNG HÓA ĐƠN
#---------------------------------------------------------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------------------------------------------------------


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

#---------------------------------------------------------------------------------------------------------------------------------------
#	BẢNG CHI TIẾT HÓA ĐƠN
#---------------------------------------------------------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------------------------------------------------------

	
	# Tạo bảng Chi tiết Hóa đơn
	create_ChiTietHoaDon = '''
		if not exists ( select * from sysobjects where name = 'ChiTietHoaDon' and xtype = 'U')
		begin
			create table ChiTietHoaDon (
				[Mã hóa đơn] varchar(10) not null,
				[Mã sản phẩm] varchar(10) not null,
				[Đơn vị tính] nvarchar(10) not null,
				[Số lượng] smallint not null,
				[Đơn giá] decimal(18,2) not null,
				PRIMARY KEY ([Mã hóa đơn], [Mã sản phẩm], [Đơn vị tính]),
				CONSTRAINT FK_ChiTietHoaDon_HoaDon FOREIGN KEY ([Mã hóa đơn])
					REFERENCES HoaDon([Mã hóa đơn]) ON DELETE CASCADE,
				CONSTRAINT FK_ChiTietHoaDon_SanPham FOREIGN KEY ([Mã sản phẩm])
					REFERENCES SanPham([Mã sản phẩm]) ON DELETE CASCADE
			)
		end
	'''

	cursor.execute(create_ChiTietHoaDon)
	
#---------------------------------------------------------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------------------------------------------------------
#---------------------------------------------------------------------------------------------------------------------------------------
	
	#-----------------------------------------------------
	# THÊM DỮ LIỆU MẪU CHO BẢNG HOA DON VÀ CHI TIẾT HOA DON
	#-----------------------------------------------------

	cursor.execute("DELETE FROM HoaDon;")
	cursor.execute("DELETE FROM ChiTietHoaDon;")

	hoa_don_list = [
    ('HD001', 'Dt001', 'admin',   '2025-01-01', 'Xuất', 3139000.00, 'Hóa đơn bán hàng mẫu 1'),
    ('HD002', 'Dt002', 'admin',  '2025-01-04 09:30:00', 'Nhập', 2600000.00, 'Hóa đơn nhập hàng mẫu 2'),
    ('HD003', 'Dt003', 'a',       '2025-01-04 07:30:00', 'Xuất', 4542000.00, 'Hóa đơn bán hàng mẫu 3'),
    ('HD004', 'Dt004', 'admin',       '2025-01-04 08:30:00', 'Nhập', 3100000.00, 'Hóa đơn nhập hàng mẫu 4'),
    ('HD005', 'Dt005', 's',       '2025-02-05', 'Xuất', 5135000.00, 'Hóa đơn bán hàng mẫu 5'),
    ('HD006', 'Dt006', 'admin',       '2025-02-06', 'Nhập', 3800000.00, 'Hóa đơn nhập hàng mẫu 6'),
    ('HD007', 'Dt001', 'admin',   '2025-02-07', 'Xuất', 10633000.00, 'Hóa đơn bán hàng mẫu 7'),
    ('HD008', 'Dt002', 'admin',  '2025-02-08', 'Nhập', 8000000.00, 'Hóa đơn nhập hàng mẫu 8'),
    ('HD009', 'Dt003', 'a',       '2025-02-09', 'Xuất', 10532000.00, 'Hóa đơn bán hàng mẫu 9'),
    ('HD010', 'Dt004', 'admin',       '2025-02-10', 'Nhập', 2200000.00, 'Hóa đơn nhập hàng mẫu 10'),
    ('HD011', 'Dt005', 's',       '2025-03-11', 'Xuất', 5051000.00, 'Hóa đơn bán hàng mẫu 11'),
    ('HD012', 'Dt006', 'admin',       '2025-03-12', 'Nhập', 3900000.00, 'Hóa đơn nhập hàng mẫu 12'),
    ('HD013', 'Dt001', 'admin',   '2025-03-13', 'Xuất', 4660000.00, 'Hóa đơn bán hàng mẫu 13'),
    ('HD014', 'Dt002', 'admin',  '2025-03-14', 'Nhập', 2900000.00, 'Hóa đơn nhập hàng mẫu 14'),
    ('HD015', 'Dt003', 'a',       '2025-03-15', 'Xuất', 2155000.00, 'Hóa đơn bán hàng mẫu 15'),
    ('HD016', 'Dt004', 'admin',       '2025-03-16', 'Nhập', 7000000.00, 'Hóa đơn nhập hàng mẫu 16'),
    ('HD017', 'Dt005', 's',       '2024-12-17', 'Xuất', 14571000.00, 'Hóa đơn bán hàng mẫu 17'),
    ('HD018', 'Dt006', 'admin',       '2024-12-18', 'Nhập', 9000000.00, 'Hóa đơn nhập hàng mẫu 18'),
    ('HD019', 'Dt001', 'admin',   '2024-12-19', 'Xuất', 2863000.00, 'Hóa đơn bán hàng mẫu 19'),
    ('HD020', 'Dt002', 'admin',  '2024-12-20', 'Nhập', 3800000.00, 'Hóa đơn nhập hàng mẫu 20'),
	]
	hoa_don_list2 = [
    ('HD001', 'Dt001', 'admin',   '2025-01-01', 'Xuất', 3139000.00, 'Hóa đơn bán hàng mẫu 1'),
    ('HD002', 'Dt002', 'abcxyz',  '2025-01-02', 'Nhập', 2600000.00, 'Hóa đơn nhập hàng mẫu 2'),
    ('HD003', 'Dt003', 'a',       '2025-01-03', 'Xuất', 4542000.00, 'Hóa đơn bán hàng mẫu 3'),
    ('HD004', 'Dt004', 'b',       '2025-01-04', 'Nhập', 3100000.00, 'Hóa đơn nhập hàng mẫu 4'),
    ('HD005', 'Dt005', 's',       '2025-02-05', 'Xuất', 5135000.00, 'Hóa đơn bán hàng mẫu 5'),
	]
	sql_hd = "INSERT INTO HoaDon ([Mã hóa đơn], [Mã đối tác], [Mã người bán], [Ngày tạo], [Loại hóa đơn], [Tổng tiền], [Ghi chú]) VALUES (?, ?, ?, ?, ?, ?, ?)"
	cursor.executemany(sql_hd, hoa_don_list)

	chi_tiet_list = [
    ('HD001', 'SP001', '20 kg', 2, 545000.00),  # 2 * 545000 = 1090000
    ('HD001', 'SP002', '20 kg', 3, 683000.00),  # 3 * 683000 = 2049000 -> Tổng = 3139000
    ('HD002', 'SP003', '40 kg', 1, 600000.00),   # 1 * 600000 = 600000
    ('HD002', 'SP004', '4.375L', 2, 1000000.00),  # 2 * 1000000 = 2000000 -> Tổng = 2600000
    ('HD003', 'SP005', '4.375L', 2, 1613000.00),  # 2 * 1613000 = 3226000
    ('HD003', 'SP006', '4.375L', 1, 1316000.00),  # 1 * 1316000 = 1316000 -> Tổng = 4542000
    ('HD004', 'SP007', '4.375L', 3, 500000.00),   # 3 * 500000 = 1500000
    ('HD004', 'SP008', '4.375L', 2, 800000.00),   # 2 * 800000 = 1600000 -> Tổng = 3100000
    ('HD005', 'SP009', '0.875L', 5, 918000.00),   # 5 * 918000 = 4590000
    ('HD005', 'SP001', '20 kg', 1, 545000.00),     # 1 * 545000 = 545000 -> Tổng = 5135000
    ('HD006', 'SP002', '20 kg', 4, 500000.00),     # 4 * 500000 = 2000000
    ('HD006', 'SP003', '40 kg', 3, 600000.00),     # 3 * 600000 = 1800000 -> Tổng = 3800000
    ('HD007', 'SP004', '18L', 2, 4510000.00),      # 2 * 4510000 = 9020000
    ('HD007', 'SP005', '4.375L', 1, 1613000.00),    # 1 * 1613000 = 1613000 -> Tổng = 10633000
    ('HD008', 'SP006', '18L', 1, 4000000.00),      # 1 * 4000000 = 4000000
    ('HD008', 'SP007', '18L', 2, 2000000.00),      # 2 * 2000000 = 4000000 -> Tổng = 8000000
    ('HD009', 'SP008', '18L', 2, 3506000.00),      # 2 * 3506000 = 7012000
    ('HD009', 'SP009', '4.375L', 1, 3520000.00),   # 1 * 3520000 = 3520000 -> Tổng = 10532000
    ('HD010', 'SP001', '20 kg', 3, 400000.00),     # 3 * 400000 = 1200000
    ('HD010', 'SP002', '20 kg', 2, 500000.00),     # 2 * 500000 = 1000000 -> Tổng = 2200000
    ('HD011', 'SP003', '40 kg', 2, 736000.00),     # 2 * 736000 = 1472000
    ('HD011', 'SP004', '4.375L', 3, 1193000.00),   # 3 * 1193000 = 3579000 -> Tổng = 5051000
    ('HD012', 'SP005', '4.375L', 1, 1500000.00),   # 1 * 1500000 = 1500000
    ('HD012', 'SP006', '4.375L', 2, 1200000.00),   # 2 * 1200000 = 2400000 -> Tổng = 3900000
    ('HD013', 'SP007', '4.375L', 1, 768000.00),    # 1 * 768000 = 768000
    ('HD013', 'SP008', '4.375L', 4, 973000.00),    # 4 * 973000 = 3892000 -> Tổng = 4660000
    ('HD014', 'SP009', '0.875L', 3, 700000.00),    # 3 * 700000 = 2100000
    ('HD014', 'SP001', '20 kg', 2, 400000.00),     # 2 * 400000 = 800000 -> Tổng = 2900000
    ('HD015', 'SP002', '20 kg', 1, 683000.00),     # 1 * 683000 = 683000
    ('HD015', 'SP003', '40 kg', 2, 736000.00),     # 2 * 736000 = 1472000 -> Tổng = 2155000
    ('HD016', 'SP004', '18L', 1, 4000000.00),      # 1 * 4000000 = 4000000
    ('HD016', 'SP005', '4.375L', 2, 1500000.00),    # 2 * 1500000 = 3000000 -> Tổng = 7000000
    ('HD017', 'SP006', '18L', 3, 4601000.00),      # 3 * 4601000 = 13803000
    ('HD017', 'SP007', '4.375L', 1, 768000.00),     # 1 * 768000 = 768000 -> Tổng = 14571000
    ('HD018', 'SP008', '18L', 1, 3000000.00),      # 1 * 3000000 = 3000000
    ('HD018', 'SP009', '4.375L', 2, 3000000.00),    # 2 * 3000000 = 6000000 -> Tổng = 9000000
    ('HD019', 'SP001', '20 kg', 4, 545000.00),     # 4 * 545000 = 2180000
    ('HD019', 'SP002', '20 kg', 1, 683000.00),     # 1 * 683000 = 683000 -> Tổng = 2863000
    ('HD020', 'SP003', '40 kg', 3, 600000.00),     # 3 * 600000 = 1800000
    ('HD020', 'SP004', '4.375L', 2, 1000000.00),   # 2 * 1000000 = 2000000 -> Tổng = 3800000
	]	
	chi_tiet_list2 = [
    ('HD001', 'SP001', '20 kg', 2, 545000.00),  # 2 * 545000 = 1090000
    ('HD001', 'SP002', '20 kg', 3, 683000.00),  # 3 * 683000 = 2049000 -> Tổng = 3139000
    ('HD002', 'SP003', '40 kg', 1, 600000.00),   # 1 * 600000 = 600000
    ('HD002', 'SP004', '4.375L', 2, 1000000.00),  # 2 * 1000000 = 2000000 -> Tổng = 2600000
    ('HD003', 'SP005', '4.375L', 2, 1613000.00),  # 2 * 1613000 = 3226000
    ('HD003', 'SP006', '4.375L', 1, 1316000.00),  # 1 * 1316000 = 1316000 -> Tổng = 4542000
    ('HD004', 'SP007', '4.375L', 3, 500000.00),   # 3 * 500000 = 1500000
    ('HD004', 'SP008', '4.375L', 2, 800000.00),   # 2 * 800000 = 1600000 -> Tổng = 3100000
    ('HD005', 'SP009', '0.875L', 5, 918000.00),   # 5 * 918000 = 4590000
    ('HD005', 'SP001', '20 kg', 1, 545000.00),     # 1 * 545000 = 545000 -> Tổng = 5135000
	]	
	sql_cthd = "INSERT INTO ChiTietHoaDon ([Mã hóa đơn], [Mã sản phẩm], [Đơn vị tính], [Số lượng], [Đơn giá]) VALUES (?, ?, ?, ?, ?)"
	cursor.executemany(sql_cthd, chi_tiet_list)

	print("Done!")

# Thêm dữ liệu mẫu vào database
insert_TestData()






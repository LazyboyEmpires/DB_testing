

USE [QLGiay]
GO

CREATE TABLE [dbo].[Account](
	[UserName] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[PassWord] [nvarchar](1000) NOT NULL,
	[Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) VALUES (N'Lazyboy', N'1', N'1962026656160185351301320480154111117132155', 1)
INSERT [dbo].[Account] ([UserName], [DisplayName], [PassWord], [Type]) VALUES (N'Hentaiman', N'2', N'1962026656160185351301320480154111117132155', 0)
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RowSneaker](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[status] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RowSneaker] ON
INSERT [dbo].[RowSneaker] ([id], [name], [status]) VALUES (1, N'Row 1', N'Empty')
INSERT [dbo].[RowSneaker] ([id], [name], [status]) VALUES (2, N'Row 2', N'Empty')
INSERT [dbo].[RowSneaker] ([id], [name], [status]) VALUES (3, N'Row 3', N'Empty')
INSERT [dbo].[RowSneaker] ([id], [name], [status]) VALUES (4, N'Row 4', N'Empty')
INSERT [dbo].[RowSneaker] ([id], [name], [status]) VALUES (5, N'Row 5', N'Empty')
INSERT [dbo].[RowSneaker] ([id], [name], [status]) VALUES (6, N'Row 6', N'Empty')
INSERT [dbo].[RowSneaker] ([id], [name], [status]) VALUES (7, N'Row 7', N'Empty')
INSERT [dbo].[RowSneaker] ([id], [name], [status]) VALUES (8, N'Row 8', N'Empty')
INSERT [dbo].[RowSneaker] ([id], [name], [status]) VALUES (9, N'Row 9', N'Empty')
INSERT [dbo].[RowSneaker] ([id], [name], [status]) VALUES (10, N'Row 10', N'Empty')
INSERT [dbo].[RowSneaker] ([id], [name], [status]) VALUES (11, N'Row 11', N'Empty')
SET IDENTITY_INSERT [dbo].[RowSneaker] OFF
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000) AS BEGIN IF @strInput IS NULL RETURN @strInput IF @strInput = '' RETURN @strInput DECLARE @RT NVARCHAR(4000) DECLARE @SIGN_CHARS NCHAR(136) DECLARE @UNSIGN_CHARS NCHAR (136) SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' DECLARE @COUNTER int DECLARE @COUNTER1 int SET @COUNTER = 1 WHILE (@COUNTER <=LEN(@strInput)) BEGIN SET @COUNTER1 = 1 WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) BEGIN IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) BEGIN IF @COUNTER=1 SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) ELSE SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) BREAK END SET @COUNTER1 = @COUNTER1 +1 END SET @COUNTER = @COUNTER +1 END SET @strInput = replace(@strInput,' ','-') RETURN @strInput END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SneakerCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[SneakerCategory] ON
INSERT [dbo].[SneakerCategory] ([id], [name]) VALUES (1, N'Nike')
INSERT [dbo].[SneakerCategory] ([id], [name]) VALUES (2, N'Adidas')
INSERT [dbo].[SneakerCategory] ([id], [name]) VALUES (3, N'New Balance')
INSERT [dbo].[SneakerCategory] ([id], [name]) VALUES (4, N'ASICS')
INSERT [dbo].[SneakerCategory] ([id], [name]) VALUES (5, N'Fila')
SET IDENTITY_INSERT [dbo].[SneakerCategory] OFF
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_UpdateAccount]
@userName NVARCHAR(100), @displayName NVARCHAR(100), @password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0
	
	SELECT @isRightPass = COUNT(*) FROM dbo.Account WHERE USERName = @userName AND PassWord = @password
	
	IF (@isRightPass = 1)
	BEGIN
		IF (@newPassword = NULL OR @newPassword = '')
		BEGIN
			UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName
		END		
		ELSE
			UPDATE dbo.Account SET DisplayName = @displayName, PassWord = @newPassword WHERE UserName = @userName
	end
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_Login]
@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName AND PassWord = @passWord
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetAccountByUserName]
@userName nvarchar(100)
AS 
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sneaker](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[idCategory] [int] NOT NULL,
	[price] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Sneaker] ON
INSERT [dbo].[Sneaker] ([id], [name], [idCategory], [price]) VALUES (1, N'Air Huarache', 1, 88)
INSERT [dbo].[Sneaker] ([id], [name], [idCategory], [price]) VALUES (2, N'Nike Flyknit', 1, 59)
INSERT [dbo].[Sneaker] ([id], [name], [idCategory], [price]) VALUES (3, N'Yeezy boot 350 V2', 2, 100)
INSERT [dbo].[Sneaker] ([id], [name], [idCategory], [price]) VALUES (4, N'New Balance 850 Lifestyle Running', 3, 120)
INSERT [dbo].[Sneaker] ([id], [name], [idCategory], [price]) VALUES (5, N'ASICS tiger', 4, 80)
INSERT [dbo].[Sneaker] ([id], [name], [idCategory], [price]) VALUES (6, N'Unisex Fila Filaray Tapey Tape', 5, 180)
INSERT [dbo].[Sneaker] ([id], [name], [idCategory], [price]) VALUES (7, N'Unisex Fila Oakmont Tr', 5, 220)
SET IDENTITY_INSERT [dbo].[Sneaker] OFF
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DateCheckIn] [date] NOT NULL,
	[DateCheckOut] [date] NULL,
	[idRow] [int] NOT NULL,
	[status] [int] NOT NULL,
	[discount] [int] NULL,
	[totalPrice] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetRowList]
AS SELECT * FROM dbo.RowSneaker
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetNumBillByDate]
@checkIn date, @checkOut date
AS 
BEGIN
	SELECT COUNT(*)
	FROM dbo.Bill AS b,dbo.RowSneaker AS t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.idRow
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetListBillByDateForReport]
@checkIn date, @checkOut date
AS 
BEGIN
	SELECT t.name, b.totalPrice, DateCheckIn, DateCheckOut, discount
	FROM dbo.Bill AS b,dbo.RowSneaker AS t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.idRow
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetListBillByDateAndPage]
@checkIn date, @checkOut date, @page int
AS 
BEGIN
	DECLARE @pageRows INT = 10
	DECLARE @selectRows INT = @pageRows
	DECLARE @exceptRows INT = (@page - 1) * @pageRows
	
	;WITH BillShow AS( SELECT b.ID, t.name AS [Row Name], b.totalPrice AS [Total], DateCheckIn AS [Date Checkin], DateCheckOut AS [DateCheckout], discount AS [Discount]
	FROM dbo.Bill AS b,dbo.RowSneaker AS t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.idRow)
	
	SELECT TOP (@selectRows) * FROM BillShow WHERE id NOT IN (SELECT TOP (@exceptRows) id FROM BillShow)
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetListBillByDate]
@checkIn date, @checkOut date
AS 
BEGIN
	SELECT t.name AS [Row Name], b.totalPrice AS [Total], DateCheckIn AS [Date Checkin], DateCheckOut AS [Date Checkout], discount AS [Discount]
	FROM dbo.Bill AS b,dbo.RowSneaker AS t
	WHERE DateCheckIn >= @checkIn AND DateCheckOut <= @checkOut AND b.status = 1
	AND t.id = b.idRow
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idBill] [int] NOT NULL,
	[idSneaker] [int] NOT NULL,
	[count] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_InsertBill]
@idRow INT
AS
BEGIN
	INSERT dbo.Bill 
	        ( DateCheckIn ,
	          DateCheckOut ,
	          idRow ,
	          status,
	          discount
	        )
	VALUES  ( GETDATE() , 
	          NULL , 
	          @idRow , 
	          0,  
	          0
	        )
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_SwitchRow]
@idRow1 INT, @idRow2 int
AS BEGIN

	DECLARE @idFirstBill int
	DECLARE @idSeconrdBill INT
	
	DECLARE @isFirstRowEmty INT = 1
	DECLARE @isSecondRowEmty INT = 1
	
	
	SELECT @idSeconrdBill = id FROM dbo.Bill WHERE idRow = @idRow2 AND status = 0
	SELECT @idFirstBill = id FROM dbo.Bill WHERE idRow = @idRow1 AND status = 0
	
	PRINT @idFirstBill
	PRINT @idSeconrdBill
	PRINT '-----------'
	
	IF (@idFirstBill IS NULL)
	BEGIN
		PRINT '0000001'
		INSERT dbo.Bill
		        ( DateCheckIn ,
		          DateCheckOut ,
		          idRow ,
		          status
		        )
		VALUES  ( GETDATE() , 
		          NULL , 
		          @idRow1 , 
		          0  
		        )
		        
		SELECT @idFirstBill = MAX(id) FROM dbo.Bill WHERE idRow = @idRow1 AND status = 0
		
	END
	
	SELECT @isFirstRowEmty = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idFirstBill
	
	PRINT @idFirstBill
	PRINT @idSeconrdBill
	PRINT '-----------'
	
	IF (@idSeconrdBill IS NULL)
	BEGIN
		PRINT '0000002'
		INSERT dbo.Bill
		        ( DateCheckIn ,
		          DateCheckOut ,
		          idRow ,
		          status
		        )
		VALUES  ( GETDATE() , 
		          NULL , 
		          @idRow2 , 
		          0  
		        )
		SELECT @idSeconrdBill = MAX(id) FROM dbo.Bill WHERE idRow = @idRow2 AND status = 0
		
	END
	
	SELECT @isSecondRowEmty = COUNT(*) FROM dbo.BillInfo WHERE idBill = @idSeconrdBill
	
	PRINT @idFirstBill
	PRINT @idSeconrdBill
	PRINT '-----------'

	SELECT id INTO IDBillInfoRow FROM dbo.BillInfo WHERE idBill = @idSeconrdBill
	
	UPDATE dbo.BillInfo SET idBill = @idSeconrdBill WHERE idBill = @idFirstBill
	
	UPDATE dbo.BillInfo SET idBill = @idFirstBill WHERE id IN (SELECT * FROM IDBillInfoRow)
	
	DROP Table IDBillInfoRow
	
	IF (@isFirstRowEmty = 0)
		UPDATE dbo.RowSneaker SET status = N'Empty' WHERE id = @idRow2
		
	IF (@isSecondRowEmty= 0)
		UPDATE dbo.RowSneaker SET status = N'Empty' WHERE id = @idRow1
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_InsertBillInfo]
@idBill INT, @idSneaker INT, @count INT
AS
BEGIN

	DECLARE @isExitsBillInfo INT
	DECLARE @SneakerCount INT = 1
	
	SELECT @isExitsBillInfo = id, @SneakerCount = b.count 
	FROM dbo.BillInfo AS b 
	WHERE idBill = @idBill AND idSneaker = @idSneaker

	IF (@isExitsBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @SneakerCount + @count
		IF (@newCount > 0)
			UPDATE dbo.BillInfo	SET count = @SneakerCount + @count WHERE idSneaker = @idSneaker
		ELSE
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idSneaker = @idSneaker
	END
	ELSE
	BEGIN
		INSERT	dbo.BillInfo
        ( idBill, idSneaker, count )
		VALUES  ( @idBill, 
          @idSneaker, 
          @count  
          )
	END
END
GO
ALTER Table [dbo].[Account] ADD  DEFAULT (N'Vanh') FOR [DisplayName]
GO
ALTER Table [dbo].[Account] ADD  DEFAULT ((0)) FOR [PassWord]
GO
ALTER Table [dbo].[Account] ADD  DEFAULT ((0)) FOR [Type]
GO
ALTER Table [dbo].[RowSneaker] ADD  DEFAULT (N'Row Does not Have Name') FOR [name]
GO
ALTER Table [dbo].[RowSneaker] ADD  DEFAULT (N'Empty') FOR [status]
GO
ALTER Table [dbo].[SneakerCategory] ADD  DEFAULT (N'No Name') FOR [name]
GO
ALTER Table [dbo].[Sneaker] ADD  DEFAULT (N'No Name') FOR [name]
GO
ALTER Table [dbo].[Sneaker] ADD  DEFAULT ((0)) FOR [price]
GO
ALTER Table [dbo].[Bill] ADD  DEFAULT (getdate()) FOR [DateCheckIn]
GO
ALTER Table [dbo].[Bill] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER Table [dbo].[BillInfo] ADD  DEFAULT ((0)) FOR [count]
GO
ALTER Table [dbo].[Sneaker]  WITH CHECK ADD FOREIGN KEY([idCategory])
REFERENCES [dbo].[SneakerCategory] ([id])
GO
ALTER Table [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([idRow])
REFERENCES [dbo].[RowSneaker] ([id])
GO
ALTER Table [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idBill])
REFERENCES [dbo].[Bill] ([id])
GO
ALTER Table [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idSneaker])
REFERENCES [dbo].[Sneaker] ([id])
GO

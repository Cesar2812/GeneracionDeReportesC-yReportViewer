create database AB_Inventory_DB;
go

use AB_Inventory_DB;
go

CREATE TABLE [dbo].[Test_Invoice_Detail](
	[Detail_ID] [int] IDENTITY(1,1) NOT NULL,
	[MasterID] [int] NULL,
	[ItemName] [varchar](50) NULL,
	[ItemPrice] [numeric](18, 2) NULL,
	[ItemQtty] [numeric](18, 0) NULL,
	[ItemTotal] [numeric](18, 2) NULL,
 CONSTRAINT [PK_Test_Invoice_Detail] PRIMARY KEY CLUSTERED 
(
	[Detail_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Test_Invoice_Master]    Script Date: 11/08/2022 4:54:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test_Invoice_Master](
	[InvoiceID] [int] IDENTITY(1000,1) NOT NULL,
	[InvoiceDate] [datetime] NULL,
	[Sub_Total] [numeric](18, 2) NULL,
	[Discount] [numeric](18, 2) NULL,
	[Net_Amount] [numeric](18, 2) NULL,
	[Paid_Amount] [numeric](18, 2) NULL,
	[Balance]  AS ([Paid_Amount]-[Net_Amount]),
 CONSTRAINT [PK_Test_Invoice_Master] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

--===========

CREATE view [dbo].[View_All_Bill_Test]
as
select m.*,d.* from Test_Invoice_Master m inner join Test_Invoice_Detail d
on m.InvoiceID=d.MasterID 
GO
ALTER TABLE [dbo].[Test_Invoice_Detail]  WITH CHECK ADD  CONSTRAINT [FK_Test_Invoice_Detail_Test_Invoice_Master] FOREIGN KEY([MasterID])
REFERENCES [dbo].[Test_Invoice_Master] ([InvoiceID])
GO
ALTER TABLE [dbo].[Test_Invoice_Detail] CHECK CONSTRAINT [FK_Test_Invoice_Detail_Test_Invoice_Master]
GO

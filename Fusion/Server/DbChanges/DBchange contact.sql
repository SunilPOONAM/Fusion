ALTER PROCEDURE [dbo].[Proc_POLineItemsAdd]  
@POHeaderId bigint,    
--@LineItem int,    
@Description nvarchar(250) = NULL,  
@Amount money = NULL ,    
@Date datetime = NULL,    
@Quantity decimal(18, 0) = NULL,    
@ValueType bit  = NULL,   
@TaskType VARCHAR(50) = NULL,  
@Division VARCHAR(50) = NULL   
AS  
BEGIN  
  
CREATE TABLE [dbo].[Contacts](
	[ContactId] [int] IDENTITY(1,1) NOT NULL,
	[CustID] [varchar](20) NOT NULL,
	[First_Name] [varchar](150) NULL,
	[Last_Name] [varchar](150) NULL,
	[Primary_Job_Site] [varchar](150) NULL,
	[Report_to] [varchar](150) NULL,
	[WorkPhone] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Email] [varchar](200) NULL,
	[Role] [varchar](150) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
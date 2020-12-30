CREATE TABLE [dbo].[NumberTable] (
    [Number]        TINYINT      NOT NULL,
    [Frequency]     INT          NULL,
	[Frequency %] VARCHAR (50) NULL,
    [Last Delay]    INT          NULL,
    [Maximum Delay] INT          NULL,
    [Average Delay] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([Number] ASC)
);


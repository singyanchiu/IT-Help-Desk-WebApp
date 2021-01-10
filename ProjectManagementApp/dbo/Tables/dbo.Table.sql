CREATE TABLE [dbo].[Table] (
    [Id]          INT            NOT NULL,
    [ProjectName] NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (256) NULL,
    [UserId] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


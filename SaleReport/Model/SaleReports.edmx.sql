
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/21/2021 11:29:41
-- Generated from EDMX file: C:\Users\Vladislav Glazkin\source\repos\SaleReportManager\SaleReport\Model\SaleReports.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SaleReports];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ClientRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Records] DROP CONSTRAINT [FK_ClientRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Records] DROP CONSTRAINT [FK_ProductRecord];
GO
IF OBJECT_ID(N'[dbo].[FK_ManagerFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Files] DROP CONSTRAINT [FK_ManagerFile];
GO
IF OBJECT_ID(N'[dbo].[FK_FileRecord]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Records] DROP CONSTRAINT [FK_FileRecord];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Files]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Files];
GO
IF OBJECT_ID(N'[dbo].[Records]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Records];
GO
IF OBJECT_ID(N'[dbo].[Managers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Managers];
GO
IF OBJECT_ID(N'[dbo].[Clients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Clients];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Files'
CREATE TABLE [dbo].[Files] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Manager_Id] int  NOT NULL
);
GO

-- Creating table 'Records'
CREATE TABLE [dbo].[Records] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Date] datetime  NOT NULL,
    [Sum] decimal(18,0)  NOT NULL,
    [Client_Id] int  NOT NULL,
    [Product_Id] int  NOT NULL,
    [File_Id] int  NOT NULL
);
GO

-- Creating table 'Managers'
CREATE TABLE [dbo].[Managers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Clients'
CREATE TABLE [dbo].[Clients] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [PK_Files]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [PK_Records]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Managers'
ALTER TABLE [dbo].[Managers]
ADD CONSTRAINT [PK_Managers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Clients'
ALTER TABLE [dbo].[Clients]
ADD CONSTRAINT [PK_Clients]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Client_Id] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [FK_ClientRecord]
    FOREIGN KEY ([Client_Id])
    REFERENCES [dbo].[Clients]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientRecord'
CREATE INDEX [IX_FK_ClientRecord]
ON [dbo].[Records]
    ([Client_Id]);
GO

-- Creating foreign key on [Product_Id] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [FK_ProductRecord]
    FOREIGN KEY ([Product_Id])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductRecord'
CREATE INDEX [IX_FK_ProductRecord]
ON [dbo].[Records]
    ([Product_Id]);
GO

-- Creating foreign key on [Manager_Id] in table 'Files'
ALTER TABLE [dbo].[Files]
ADD CONSTRAINT [FK_ManagerFile]
    FOREIGN KEY ([Manager_Id])
    REFERENCES [dbo].[Managers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManagerFile'
CREATE INDEX [IX_FK_ManagerFile]
ON [dbo].[Files]
    ([Manager_Id]);
GO

-- Creating foreign key on [File_Id] in table 'Records'
ALTER TABLE [dbo].[Records]
ADD CONSTRAINT [FK_FileRecord]
    FOREIGN KEY ([File_Id])
    REFERENCES [dbo].[Files]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileRecord'
CREATE INDEX [IX_FK_FileRecord]
ON [dbo].[Records]
    ([File_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
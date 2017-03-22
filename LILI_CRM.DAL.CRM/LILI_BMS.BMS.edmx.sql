
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/01/2014 02:53:03
-- Generated from EDMX file: E:\LILI_BMS\Source\LILI_BMS.DAL.BMS\LILI_BMS.BMS.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LILI_BMS];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BMS_tblUserWiseMenuAssign_BMS_tblMenuList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BMS_tblUserWiseMenuAssign] DROP CONSTRAINT [FK_BMS_tblUserWiseMenuAssign_BMS_tblMenuList];
GO
IF OBJECT_ID(N'[dbo].[FK_BMS_tblUserWiseMenuAssign_BMS_tblUserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BMS_tblUserWiseMenuAssign] DROP CONSTRAINT [FK_BMS_tblUserWiseMenuAssign_BMS_tblUserInfo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[BMS_tblCFBreakdownComponent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BMS_tblCFBreakdownComponent];
GO
IF OBJECT_ID(N'[dbo].[BMS_tblCFFlavor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BMS_tblCFFlavor];
GO
IF OBJECT_ID(N'[dbo].[BMS_tblCFPackSize]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BMS_tblCFPackSize];
GO
IF OBJECT_ID(N'[dbo].[BMS_tblCFRejectionComponent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BMS_tblCFRejectionComponent];
GO
IF OBJECT_ID(N'[dbo].[BMS_tblCFShift]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BMS_tblCFShift];
GO
IF OBJECT_ID(N'[dbo].[BMS_tblCFUnitPlant]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BMS_tblCFUnitPlant];
GO
IF OBJECT_ID(N'[dbo].[BMS_tblMenuList]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BMS_tblMenuList];
GO
IF OBJECT_ID(N'[dbo].[BMS_tblOrganizationInformation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BMS_tblOrganizationInformation];
GO
IF OBJECT_ID(N'[dbo].[BMS_tblUserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BMS_tblUserInfo];
GO
IF OBJECT_ID(N'[dbo].[BMS_tblUserWiseMenuAssign]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BMS_tblUserWiseMenuAssign];
GO
IF OBJECT_ID(N'[LILI_BMSModelStoreContainer].[CommonConfigType]', 'U') IS NOT NULL
    DROP TABLE [LILI_BMSModelStoreContainer].[CommonConfigType];
GO
IF OBJECT_ID(N'[dbo].[tblEmployeeInformation]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tblEmployeeInformation];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'BMS_tblOrganizationInformation'
CREATE TABLE [dbo].[BMS_tblOrganizationInformation] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [OrganizationName] varchar(250)  NOT NULL,
    [Address] varchar(500)  NULL,
    [Phone] varchar(50)  NULL,
    [Fax] varchar(50)  NULL,
    [EMail] varchar(50)  NULL,
    [Description] varchar(500)  NULL,
    [IsActive] bit  NULL,
    [IUser] varchar(50)  NOT NULL,
    [EUser] varchar(50)  NULL,
    [IDate] datetime  NOT NULL,
    [EDate] datetime  NULL
);
GO

-- Creating table 'BMS_tblMenuList'
CREATE TABLE [dbo].[BMS_tblMenuList] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ModuleName] varchar(50)  NOT NULL,
    [MenuName] varchar(100)  NOT NULL,
    [ParentMenuName] varchar(100)  NULL,
    [PageUrl] varchar(250)  NULL,
    [ControllerName] varchar(250)  NULL,
    [ActionName] varchar(250)  NULL
);
GO

-- Creating table 'BMS_tblUserWiseMenuAssign'
CREATE TABLE [dbo].[BMS_tblUserWiseMenuAssign] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [MenuId] int  NOT NULL,
    [IUser] varchar(50)  NOT NULL,
    [EUser] varchar(50)  NULL,
    [IDate] datetime  NOT NULL,
    [EDate] datetime  NULL
);
GO

-- Creating table 'tblEmployeeInformation'
CREATE TABLE [dbo].[tblEmployeeInformation] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmpId] varchar(50)  NOT NULL,
    [EmpName] varchar(100)  NOT NULL,
    [Designation] varchar(100)  NULL,
    [Department] varchar(100)  NULL,
    [Store] varchar(100)  NULL,
    [JoiningDate] datetime  NULL,
    [Remarks] varchar(500)  NULL,
    [IsActive] bit  NULL,
    [IUser] varchar(50)  NOT NULL,
    [EUser] varchar(50)  NULL,
    [IDate] datetime  NOT NULL,
    [EDate] datetime  NULL
);
GO

-- Creating table 'BMS_tblUserInfo'
CREATE TABLE [dbo].[BMS_tblUserInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] varchar(50)  NOT NULL,
    [UserPassword] varchar(50)  NOT NULL,
    [EmpId] int  NULL,
    [IsActive] bit  NULL,
    [IUser] varchar(50)  NOT NULL,
    [EUser] varchar(50)  NULL,
    [IDate] datetime  NOT NULL,
    [EDate] datetime  NULL
);
GO

-- Creating table 'BMS_tblCFBreakdownComponent'
CREATE TABLE [dbo].[BMS_tblCFBreakdownComponent] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] varchar(50)  NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [SortOrder] int  NULL,
    [Description] varchar(100)  NOT NULL,
    [IsActive] bit  NULL,
    [IUser] varchar(50)  NOT NULL,
    [EUser] varchar(50)  NULL,
    [IDate] datetime  NOT NULL,
    [EDate] datetime  NULL
);
GO

-- Creating table 'BMS_tblCFFlavor'
CREATE TABLE [dbo].[BMS_tblCFFlavor] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] varchar(50)  NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [SortOrder] int  NULL,
    [Description] varchar(100)  NOT NULL,
    [IsActive] bit  NULL,
    [IUser] varchar(50)  NOT NULL,
    [EUser] varchar(50)  NULL,
    [IDate] datetime  NOT NULL,
    [EDate] datetime  NULL
);
GO

-- Creating table 'BMS_tblCFPackSize'
CREATE TABLE [dbo].[BMS_tblCFPackSize] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] varchar(50)  NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [SortOrder] int  NULL,
    [Description] varchar(100)  NOT NULL,
    [IsActive] bit  NULL,
    [IUser] varchar(50)  NOT NULL,
    [EUser] varchar(50)  NULL,
    [IDate] datetime  NOT NULL,
    [EDate] datetime  NULL
);
GO

-- Creating table 'BMS_tblCFRejectionComponent'
CREATE TABLE [dbo].[BMS_tblCFRejectionComponent] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] varchar(50)  NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [SortOrder] int  NULL,
    [Description] varchar(100)  NOT NULL,
    [IsActive] bit  NULL,
    [IUser] varchar(50)  NOT NULL,
    [EUser] varchar(50)  NULL,
    [IDate] datetime  NOT NULL,
    [EDate] datetime  NULL
);
GO

-- Creating table 'BMS_tblCFShift'
CREATE TABLE [dbo].[BMS_tblCFShift] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] varchar(50)  NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [SortOrder] int  NULL,
    [Description] varchar(100)  NOT NULL,
    [IsActive] bit  NULL,
    [IUser] varchar(50)  NOT NULL,
    [EUser] varchar(50)  NULL,
    [IDate] datetime  NOT NULL,
    [EDate] datetime  NULL
);
GO

-- Creating table 'BMS_tblCFUnitPlant'
CREATE TABLE [dbo].[BMS_tblCFUnitPlant] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] varchar(50)  NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [SortOrder] int  NULL,
    [Description] varchar(100)  NOT NULL,
    [IsActive] bit  NULL,
    [IUser] varchar(50)  NOT NULL,
    [EUser] varchar(50)  NULL,
    [IDate] datetime  NOT NULL,
    [EDate] datetime  NULL
);
GO

-- Creating table 'CommonConfigType'
CREATE TABLE [dbo].[CommonConfigType] (
    [TableName] varchar(50)  NOT NULL,
    [DisplayName] varchar(50)  NOT NULL,
    [SortOrder] int  NULL,
    [ModuleId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'BMS_tblOrganizationInformation'
ALTER TABLE [dbo].[BMS_tblOrganizationInformation]
ADD CONSTRAINT [PK_BMS_tblOrganizationInformation]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BMS_tblMenuList'
ALTER TABLE [dbo].[BMS_tblMenuList]
ADD CONSTRAINT [PK_BMS_tblMenuList]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BMS_tblUserWiseMenuAssign'
ALTER TABLE [dbo].[BMS_tblUserWiseMenuAssign]
ADD CONSTRAINT [PK_BMS_tblUserWiseMenuAssign]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tblEmployeeInformation'
ALTER TABLE [dbo].[tblEmployeeInformation]
ADD CONSTRAINT [PK_tblEmployeeInformation]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BMS_tblUserInfo'
ALTER TABLE [dbo].[BMS_tblUserInfo]
ADD CONSTRAINT [PK_BMS_tblUserInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BMS_tblCFBreakdownComponent'
ALTER TABLE [dbo].[BMS_tblCFBreakdownComponent]
ADD CONSTRAINT [PK_BMS_tblCFBreakdownComponent]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BMS_tblCFFlavor'
ALTER TABLE [dbo].[BMS_tblCFFlavor]
ADD CONSTRAINT [PK_BMS_tblCFFlavor]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BMS_tblCFPackSize'
ALTER TABLE [dbo].[BMS_tblCFPackSize]
ADD CONSTRAINT [PK_BMS_tblCFPackSize]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BMS_tblCFRejectionComponent'
ALTER TABLE [dbo].[BMS_tblCFRejectionComponent]
ADD CONSTRAINT [PK_BMS_tblCFRejectionComponent]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BMS_tblCFShift'
ALTER TABLE [dbo].[BMS_tblCFShift]
ADD CONSTRAINT [PK_BMS_tblCFShift]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BMS_tblCFUnitPlant'
ALTER TABLE [dbo].[BMS_tblCFUnitPlant]
ADD CONSTRAINT [PK_BMS_tblCFUnitPlant]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [TableName], [DisplayName], [ModuleId] in table 'CommonConfigType'
ALTER TABLE [dbo].[CommonConfigType]
ADD CONSTRAINT [PK_CommonConfigType]
    PRIMARY KEY CLUSTERED ([TableName], [DisplayName], [ModuleId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MenuId] in table 'BMS_tblUserWiseMenuAssign'
ALTER TABLE [dbo].[BMS_tblUserWiseMenuAssign]
ADD CONSTRAINT [FK_BMS_tblUserWiseMenuAssign_BMS_tblMenuList]
    FOREIGN KEY ([MenuId])
    REFERENCES [dbo].[BMS_tblMenuList]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BMS_tblUserWiseMenuAssign_BMS_tblMenuList'
CREATE INDEX [IX_FK_BMS_tblUserWiseMenuAssign_BMS_tblMenuList]
ON [dbo].[BMS_tblUserWiseMenuAssign]
    ([MenuId]);
GO

-- Creating foreign key on [UserId] in table 'BMS_tblUserWiseMenuAssign'
ALTER TABLE [dbo].[BMS_tblUserWiseMenuAssign]
ADD CONSTRAINT [FK_BMS_tblUserWiseMenuAssign_BMS_tblUserInfo]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[BMS_tblUserInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BMS_tblUserWiseMenuAssign_BMS_tblUserInfo'
CREATE INDEX [IX_FK_BMS_tblUserWiseMenuAssign_BMS_tblUserInfo]
ON [dbo].[BMS_tblUserWiseMenuAssign]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
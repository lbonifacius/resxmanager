SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cultures]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Cultures](
	[CultureID] [int] IDENTITY(1,1) NOT NULL,
	[CultureName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CultureID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Translations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Translations](
	[TranslationID] [bigint] IDENTITY(1,1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TranslationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TranslationTexts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TranslationTexts](
	[TranslationTextID] [bigint] IDENTITY(1,1) NOT NULL,
	[TranslationID] [bigint] NOT NULL,
	[CultureID] [int] NOT NULL,
	[Text] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[TranslationTextID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO


IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TranslationTexts_Cultures]') AND parent_object_id = OBJECT_ID(N'[dbo].[TranslationTexts]'))
ALTER TABLE [dbo].[TranslationTexts]  WITH CHECK ADD  CONSTRAINT [FK_TranslationTexts_Cultures] FOREIGN KEY([CultureID])
REFERENCES [dbo].[Cultures] ([CultureID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TranslationTexts_Cultures]') AND parent_object_id = OBJECT_ID(N'[dbo].[TranslationTexts]'))
ALTER TABLE [dbo].[TranslationTexts] CHECK CONSTRAINT [FK_TranslationTexts_Cultures]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TranslationTexts_Translations]') AND parent_object_id = OBJECT_ID(N'[dbo].[TranslationTexts]'))
ALTER TABLE [dbo].[TranslationTexts]  WITH CHECK ADD  CONSTRAINT [FK_TranslationTexts_Translations] FOREIGN KEY([TranslationID])
REFERENCES [dbo].[Translations] ([TranslationID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TranslationTexts_Translations]') AND parent_object_id = OBJECT_ID(N'[dbo].[TranslationTexts]'))
ALTER TABLE [dbo].[TranslationTexts] CHECK CONSTRAINT [FK_TranslationTexts_Translations]
GO

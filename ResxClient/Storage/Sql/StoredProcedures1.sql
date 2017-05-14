IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SearchTranslation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SearchTranslation]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCulture]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetCulture]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FindTranslations]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FindTranslations]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FindTranslations]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[FindTranslations]
	@text nvarchar(MAX),
	@culture nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * from TranslationTexts t inner join Cultures c
		on t.CultureID = c.CultureID 
		where c.CultureName = @culture and t.Text = @text
END
' 
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetCulture]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetCulture]
	@culture nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS(SELECT TOP 1 * from Cultures where CultureName = @culture)
	BEGIN
		INSERT INTO Cultures (CultureName) VALUES (@culture) 
	END

	SELECT * from Cultures where CultureName = @culture
END' 
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SearchTranslation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SearchTranslation] 
	-- Add the parameters for the stored procedure here
	@text nvarchar(MAX),
	@srcculture nvarchar(50),
	@tgtculture nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;

   select ttt.TranslationTextID, ttt.TranslationID, ttt.CultureID, ttt.Text, ttt.CreatedDate, ttt.ModifiedDate
	from TranslationTexts ttt inner join Cultures cc on ttt.CultureID = cc.CultureID
	where ttt.TranslationID in (
		select tt.TranslationID from TranslationTexts tt 
		inner join Cultures c on c.CultureID = tt.CultureID
		where c.CultureName = @srcculture and tt.Text = @text)
	and cc.CultureName = @tgtculture
END
' 
END
GO
# Automatic translation of resource files
This function of RESX Manager might not look like what you expect, nevertheless this function will help to reduce translation costs. Indeed, modern corporate translation management systems work like this.

The simple idea is to store already translated texts into a database and for every new project look up missing texts before translateing it by hand. By this procedure, translation quality is guaranteed to be much better than letting Google Translate do the job. 

## Setup database
# Click **Translations** -> **Configure database**
# Check Server and Database for correctness. If not, close RESX Manager, open ResxManager.exe.config in any text editor and change the setting "TranslationStorage". Open RESX Manager again.
# Click **Configure database**
# Close the dialog

## How to store translations in database?
Open the solution which contains the translations you like to store. Then click **Translations** -> **Store all translations to database**.

## How to reuse translations?
# Open the solution that needs to be translated.
# Click translate in the toolbar. RESX Manager searches in the database for any text in the source language and stores the corresponding results in the target language.
You might as well translate only selected languages by expanding the language node which is the source language of the translation. Then open the context menu by clicking right on the target language. **Auto translate** will do the job. 

## Further topics
* [Best practices](Best-practices)
* [Exclude Projects, Directories, Files or Entries](Exclude-Projects,-Directories,-Files-or-Entries)
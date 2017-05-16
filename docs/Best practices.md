# Best practices

## How to get started with translation storage?
1. Open RESX Manager
1. Configure database to store translations (see [Automatic translation of resource files](Automatic-translation-of-resource-files))
1. Open any existing solution containing good translations.
1. Check culture mappings of resource files. Mappings are displayed after resource file name. In case a culture has not been detected correctly, right-click on the resource file and select the correct culture from the dropdown list.
1. Store translations by **Translations** -> **Store all translations**
Repeat steps 3-5 for every solution that contains good texts.

## How to translate new texts?
1. Open the solution in RESX Manager.
1. Check language mappings as above.
1. Click **Translate** to find existing translations in order to reduce translations costs.
1. Export missing entries to Excel, by clicking **Export** and checking **Export missing entries only** (see [Export and Import of resource files to Excel](Export-and-Import-of-resource-files-to-Excel)).
1. Translate the Excel file.
1. Import the translated Excel file.
1. Save changes to resource files (**File** -> **Save resource files**, or icon)
1. Store new translations to database (**Translations** -> **Store all translations to database**)

**Further Topics**
* [Automatic translation of resource files](Automatic-translation-of-resource-files)
* [Export and Import of resource files to Excel](Export-and-Import-of-resource-files-to-Excel)
* [Exclude Projects, Directories, Files or Entries](Exclude-Projects,-Directories,-Files-or-Entries)

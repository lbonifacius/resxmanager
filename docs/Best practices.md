# Best practices

## How to get started with translation storage?
# Open RESX Manager
# Configure database to store translations (see [Automatic translation of resource files](Automatic-translation-of-resource-files))
# Open any existing solution containing good translations.
# Check culture mappings of resource files. Mappings are displayed after resource file name. In case a culture has not been detected correctly, right-click on the resource file and select the correct culture from the dropdown list.
# Store translations by **Translations** -> **Store all translations**
Repeat steps 3-5 for every solution that contains good texts.

## How to translate new texts?
# Open the solution in RESX Manager.
# Check language mappings as above.
# Click **Translate** to find existing translations in order to reduce translations costs.
# Export missing entries to Excel, by clicking **Export** and checking **Export missing entries only** (see [Export and Import of resource files to Excel](Export-and-Import-of-resource-files-to-Excel)).
# Translate the Excel file.
# Import the translated Excel file.
# Save changes to resource files (**File** -> **Save resource files**, or icon)
# Store new translations to database (**Translations** -> **Store all translations to database**)

**Further Topics**
* [Automatic translation of resource files](Automatic-translation-of-resource-files)
* [Export and Import of resource files to Excel](Export-and-Import-of-resource-files-to-Excel)
* [Exclude Projects, Directories, Files or Entries](Exclude-Projects,-Directories,-Files-or-Entries)
## Export of resource files to Excel

1. Open your Visual Studio solution by clicking **Open** and selecting the required file. RESX Manager searches for all resource files included in your projects and automatically tries to detect the particular cultures (by parsing the filename (.resx) or reading internal properties (.wxl)). If some cultures couldn't be discovered, the invariant culture would be assigned. You may specify another culture by clicking on the resource file in the tree view with the right mouse button and choosing a culture from the drop down list.
1. Click **Export**, choose an appropriate folder and press OK. The Excel spreadsheet will be created. One worksheet per project, one column per culture. Please do not change the worksheet's names as these are the primary keys for import. 

## Import of translated Excel spreadsheets

1. Click **Import**. Choose the Excel file containing the translations.
1. Click **File** -> **Save resources**. All resource texts will be saved to the original file, if existing before. In case of new translations, a new resource file will be created.
1. Open the solution in Visual Studio and add possibly new resource files to their projects. 
In case you are using the translation storage in order to reuse translations, don't forget to store translations to the database by clicking **Translations** -> **Store all translations to database**.


**Further topics**
* [How to add new languages](How-to-add-new-languages)
* [Automatic translation of resource files](Automatic-translation-of-resource-files)
* [Best practices](Best-practices)
* [Exclude Projects, Directories, Files or Entries](Exclude-Projects,-Directories,-Files-or-Entries)

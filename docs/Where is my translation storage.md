# Where is my translation storage?
When you are using the translation storage to reuse translations, all translations are stored into a database, called TranslationStorage by default. Its location in the file system depends on your configuration. To see the configuration, open RESX manager and click **Translation** -> **Setup database**.

If Server equals "(localdb)", the file is typically located in C:\Users\{"[Current User](Current-User)"}\. 

If Server equals ".\SQLEXPRESS" or "(local)", then RESX manager is connnected to the locally installed SQL Server. To find out more, open SQL Server Management Studio and check out the database properties.

**Further Topics**
* [Automatic translation of resource files](Automatic-translation-of-resource-files)
# How to integrate with Team Foundation Server

Thanks to [kellyselden](https://www.codeplex.com/site/users/view/kellyselden) for providing this function.

Integration with Team Foundation Server (TFS) is easy. Check the path of TF.exe on your local computer and enter it in the configuration file (PathToTFExe). By default, this is set to C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\tf.exe.

If RESX Manager detects changes to files that are under source control while storing changes to *.RESX file, you will be prompted to checkout the effected files. Then, you may checkin those changes, if you like to.

## Further topics
* [Automatic translation of resource files](AutomaticTranslation.md)
* [Exclude Projects, Directories, Files or Entries](ExcludeProjectsDirectoriesFilesEntries.md)

# Exclude Projects, Directories, Files or Entries from Translation

Thanks to [pqsys](https://www.codeplex.com/site/users/view/pqsys) for the idea.

Using the interface of RESX Manager, you can select projects and files to export to Excel. Nevertheless, it might be usefull to completely exclude files, folders or resource entries from translation.

By writing simple configuration files you can exclude specific projects, folders, resource files or resource entries from translation. Effected objects will not be loaded and thus not exported to Excel.

To exclude for instance all files called "StringsNoTranslate.resxm" in the entire solution (including all projects), just create a configuration file called "SolutionName.sln.resxm" in the solution directory file with the following content. 

```
 <solution>
    <files>
      <exclude Pattern="StringsNoTranslate.resx"/>
    </files>
 </solution>
```

Objects are matched using a search pattern and an operator that are applied to the names. There are different operators: 

Operator | Describtion
---------| -----------
Like | Matches everything that contains the pattern (case insensitive). (Default)
Equals | Matches exact strings (case insensitive).
Regex | Uses Regular Expressions to select matching strings.

Several exclude-nodes will be connected as logical OR. Project configurations are called "ProjectName.resxm". Please see the following examples containing all available options.

**Sample Configuration for solutions (SolutionName.sln.resxm)**
```
 <solution>
    <projects>
      <exclude Operator="Equals" Pattern="InvariantProject"/>
      <exclude Operator="Equals" Pattern="InvariantProject2"/>
    </projects>
    <directories>
      <exclude Operator="Like" Pattern="hidden_dir"/>
    </directories>
    <files>
      <exclude Operator="Equals" Pattern="StringsNoTranslate.resx"/>
    </files>
    <groups>
      <exclude Operator="Regex" Pattern="Test[0-9](0-9)"/>
    </groups>
 </solution>
```

**Sample Configuration for projects (ProjectName.resxm)**
```
 <solution>
    <directories>
      <exclude Operator="Like" Pattern="hidden_dir"/>
    </directories>
    <files>
      <exclude Operator="Equals" Pattern="StringsNoTranslate.resx"/>
    </files>
    <groups>
      <exclude Operator="Regex" Pattern="Test[0-9](0-9)"/>
    </groups>
 </solution>
```

## Further topics
* [Automatic translation of resource files](Automatic-translation-of-resource-files)
* [How to use with Team Foundation Server](How-to-use-with-Team-Foundation-Server)
* [Best practices](Best-practices)

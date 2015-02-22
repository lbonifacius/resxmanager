using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace ResourceManager.Core
{
    public class VSSolutionFileParser
    {
        public VSSolution Solution
        {
            get;
            private set;
        }

        public VSSolutionFileParser(string filepath, VSSolution solution)
        {
            Solution = solution;

            using(var streamReader = File.OpenText(filepath))
            {
                while (!streamReader.EndOfStream)
                {
                    string s = streamReader.ReadLine();

                    if (s.StartsWith("Project"))
                        RegisterProject(s);
                }
            }
        }
        private void RegisterProject(string line)
        { 
            MatchCollection matches = Regex.Matches(line, "\"(.*?)\"");

            if (matches[2] != null && File.Exists(Path.Combine(Solution.SolutionDirectory.FullName, matches[2].Groups[1].Value))
                && !Solution.SkipProject(matches[1].Groups[1].Value))
            {
                var project = new VSProject(Solution, matches[2].Groups[1].Value, matches[1].Groups[1].Value);
                Solution.Projects.Add(matches[1].Groups[1].Value, project);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace ResourceManager.Core
{
    public class VSSolutionFileParser
    {
        private VSSolution solution;        	

        public VSSolutionFileParser(string filepath, VSSolution solution)
        {
            this.solution = solution;

            using(StreamReader streamReader = File.OpenText(filepath))
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

            if (matches[2] != null && File.Exists(Path.Combine(solution.SolutionDirectory.FullName, matches[2].Groups[1].Value)))
            {
                VSProject project = new VSProject(solution, matches[2].Groups[1].Value, matches[1].Groups[1].Value);
                solution.Projects.Add(matches[1].Groups[1].Value, project);
            }
        }
    }
}

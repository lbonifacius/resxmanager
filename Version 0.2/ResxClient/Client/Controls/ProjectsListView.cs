using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResourceManager.Core;

namespace ResourceManager.Client.Controls
{
    public class ProjectsListView : ListBox
    {
        private VSSolution solution = null;
        public VSSolution Solution
        {
            set
            {
                solution = value;

                loadData();
            }
        }
        private void loadData()
        {
            foreach (var project in solution.Projects.Values)
            {
                this.Items.Add(new ProjectListViewEntry(project));
            }
        }

        public void LoadProjects(VSSolution s, IEnumerable<VSProject> projects)
        {
            solution = s;

            int i = 0;
            foreach (var project in solution.Projects.Values)
            {
                var item = new ProjectListViewEntry(project);
                this.Items.Add(item);

                if (projects.Contains(project))
                    this.SetSelected(i, true);

                i++;
            }
        }

        public List<VSProject> GetSelectedProjects()
        {
            var list = new List<VSProject>();
            
            foreach(var item in this.SelectedItems)
            {
                list.Add(((ProjectListViewEntry)item).Project);
            }
            return list;
        }
    }

    public class ProjectListViewEntry
    {
        public VSProject Project
        { 
            get; private set; 
        }

        public ProjectListViewEntry(VSProject project)
        {
            this.Project = project;
        }

        public override string ToString()
        {
            return Project.Name;
        }
    }
}

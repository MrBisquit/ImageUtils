using ImageUtils.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUtils.Project
{
    public class Project
    {
        public string Name { get; set; } = "";
    }

    public static class ProjectManager
    {
        public static Project? OpenProject = null;

        public static void CreateTempProject()
        {
            OpenProject = new Project();
            OpenProject.Name = "$TEMP-" + Guid.NewGuid().ToString();

            Globals.mainWindow.Title = $"ImageUtils - Project: {OpenProject.Name}";

            LayerManager.Layers.Clear();
        }
    }
}

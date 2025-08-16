using CodeEditor2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodeEditor2.Controller;

namespace pluginDrawIo.Data
{
    public class DrawIoXmlFile : CodeEditor2.Data.File
    {
        public static DrawIoXmlFile Create(string relativePath, Project project)
        {
            string name;
            if (relativePath.Contains(System.IO.Path.DirectorySeparatorChar))
            {
                name = relativePath.Substring(relativePath.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
            }
            else
            {
                name = relativePath;
            }
            DrawIoXmlFile chatLogFile = new DrawIoXmlFile()
            {
                Project = project,
                RelativePath = relativePath,
                Name = name
            };

            return chatLogFile;
        }

        protected override CodeEditor2.NavigatePanel.NavigatePanelNode CreateNode()
        {
            NavigatePanel.DrawIoXmlNode node = new NavigatePanel.DrawIoXmlNode(this);
            return node;
        }
    }
}

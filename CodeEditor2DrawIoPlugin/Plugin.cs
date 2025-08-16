using Avalonia.Controls;
using CodeEditor2.FileTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodeEditor2.Controller;

namespace pluginDrawIo
{
    public class Plugin : CodeEditor2Plugin.IPlugin
    {
        public static string StaticID = "DrawIo";
        public string Id { get { return StaticID; } }

        public bool Register()
        {
            // register filetypes
            {
                FileTypes.DrawIoXmlFile fileType = new FileTypes.DrawIoXmlFile();
                CodeEditor2.Global.FileTypes.Add(fileType.ID, fileType);
            }

            return true;
        }

        private void projectCreated(CodeEditor2.Data.Project project)
        {
            project.ProjectProperties.Add(Id, new ProjectProperty(project));
        }

        private static TabItem previewTab;
        public bool Initialize()
        {
            {
                MenuItem menuItem = CodeEditor2.Controller.Menu.Tool;
                //MenuItem newMenuItem = CodeEditor2.Global.CreateMenuItem(
                //    "Create Snapshot",
                //    "menuItem_CreateSnapShot",
                //    "CodeEditor2/Assets/Icons/play.svg",
                //    Avalonia.Media.Colors.Red
                //    );
                //menuItem.Items.Add(newMenuItem);
                //newMenuItem.Click += MenuItem_CreateSnapShot_Click;
            }

            DrawIoPreviewControl drawIoPreviewControl = new DrawIoPreviewControl();
            previewTab = new TabItem()
            {
                Header = "DrawIO",
                Name = "DrawIo",
                FontSize = 12,
            //    //                Icon = new Avalonia.Media.Imaging.Bitmap("CodeEditor2AiPlugin/Assets/Icons/chat.svg"),

                Content = drawIoPreviewControl
            };

            CodeEditor2.Controller.Tabs.AddItem(previewTab);

            //ContextMenu contextMenu = Controller.NavigatePanel.GetContextMenu();
            //{
            //    //MenuItem menuItem_RunSimulation = CodeEditor2.Global.CreateMenuItem("Run Simulation", "menuItem_RunSimulation","play",Avalonia.Media.Colors.Red);
            //    //contextMenu.Items.Add(menuItem_RunSimulation);
            //    //menuItem_RunSimulation.Click += MenuItem_RunSimulation_Click;
            //}
            // register project property form tab
            //            CodeEditor.Tools.ProjectPropertyForm.FormCreated += Tools.ProjectPropertyTab.ProjectPropertyFromCreated;

//            NavigatePanel.NavigatePanelMenu.Register();

            return true;
        }
        internal static Avalonia.Controls.TabItem? chatTab;
//        internal static pluginAi.Views.ChatControl? chatControl;

    }
}
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginDrawIo.NavigatePanel
{
    public class DrawIoXmlNode : CodeEditor2.NavigatePanel.FileNode
    {
        public DrawIoXmlNode(Data.DrawIoXmlFile drawIoXmlFile) : base(drawIoXmlFile)
        {

        }

        public virtual Data.DrawIoXmlFile? DrawIoXmlFile
        {
            get { return Item as Data.DrawIoXmlFile; }
        }

        public override async void OnSelected()
        {
            try
            {
                await UpdateAsync();
            }
            catch
            {
                if (System.Diagnostics.Debugger.IsAttached) System.Diagnostics.Debugger.Break();
            }
        }

        public override Task UpdateAsync()
        {
            if (DrawIoXmlFile == null)
            {
                return Task.CompletedTask;
            }
            UpdateVisual();
            return Task.CompletedTask;
        }

        public override void UpdateVisual()
        {
            if (Dispatcher.UIThread.CheckAccess())
            {
                _updateVisual();
            }
            else
            {
                Dispatcher.UIThread.Post(() =>
                {
                    try
                    {
                        _updateVisual();
                    }catch(Exception ex)
                    {
                        CodeEditor2.Controller.AppendLog("#Exception " + ex.Message, Avalonia.Media.Colors.Red);
                    }
                });
            }
        }
        public void _updateVisual()
        {
            string text = "-";
            if (FileItem != null) text = FileItem.Name;
            Text = text;

            List<CodeEditor2.NavigatePanel.NavigatePanelNode> newNodes = new List<CodeEditor2.NavigatePanel.NavigatePanelNode>();



            List<CodeEditor2.NavigatePanel.NavigatePanelNode> removeNodes = new List<CodeEditor2.NavigatePanel.NavigatePanelNode>();
            lock (Nodes)
            {
                foreach (CodeEditor2.NavigatePanel.NavigatePanelNode node in Nodes)
                {
                    if (!newNodes.Contains(node))
                    {
                        removeNodes.Add(node);
                    }
                }
                foreach (CodeEditor2.NavigatePanel.NavigatePanelNode node in removeNodes)
                {
                    Nodes.Remove(node);
                    node.Dispose();
                }

                foreach (CodeEditor2.NavigatePanel.NavigatePanelNode node in newNodes)
                {
                    if (Nodes.Contains(node)) continue;

                    int index = newNodes.IndexOf(node);
                    Nodes.Insert(index, node);
                }
            }

            if (DrawIoXmlFile == null) return;

            Image = GetIcon(DrawIoXmlFile);

        }
        public static IImage? GetIcon(Data.DrawIoXmlFile drawIoXmlFile)
        {
            // Icon badge will update only in UI thread
            if (System.Threading.Thread.CurrentThread.Name != "UI")
            {
                throw new Exception();
            }

            List<AjkAvaloniaLibs.Libs.Icons.OverrideIcon> overrideIcons = new List<AjkAvaloniaLibs.Libs.Icons.OverrideIcon>();

            return AjkAvaloniaLibs.Libs.Icons.GetSvgBitmap(
                "CodeEditor2/Assets/Icons/image.svg",
                Avalonia.Media.Color.FromArgb(100, 200, 240, 240),
                overrideIcons
                );
        }

        public static new Action<ContextMenu>? CustomizeSpecificNodeContextMenu;
        protected override Action<ContextMenu>? customizeSpecificNodeContextMenu => CustomizeSpecificNodeContextMenu;


    }
}
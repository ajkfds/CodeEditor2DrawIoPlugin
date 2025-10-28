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
            //if (pluginAi.Plugin.chatControl == null) throw new Exception();
            //if (pluginAi.Plugin.chatTab == null) throw new Exception();

            //if (ChatLogFile == null) return;


            //Plugin.chatControl.LogFilePath = ChatLogFile.AbsolutePath;
            //Plugin.chatControl.LoadLogFile();
            //CodeEditor2.Controller.Tabs.SelectTab(Plugin.chatTab);

            // activate navigate panel context menu
            //var menu = CodeEditor2.Controller.NavigatePanel.GetContextMenuStrip();
            //if (menu.Items.ContainsKey("openWithExploererTsmi")) menu.Items["openWithExploererTsmi"].Visible = true;
            //if (menu.Items.ContainsKey("icarusVerilogTsmi")) menu.Items["icarusVerilogTsmi"].Visible = true;
            //if (menu.Items.ContainsKey("VerilogDebugTsmi")) menu.Items["VerilogDebugTsmi"].Visible = true;

            //            System.Diagnostics.Debug.Print("## VerilogFileNode.OnSelected");

            //if (TextFile == null)
            //{
            //    if (NodeSelected != null) NodeSelected();
            //    Update();
            //    return;
            //}

            //if (!CodeEditor2.Global.StopBackGroundParse)
            //{
            //    if (TextFile.ParseValid && !TextFile.ReparseRequested)
            //    {
            //        // skip parse
            //    }
            //    else
            //    {
            //        CodeEditor2.Global.StopBackGroundParse = true;
            //        await parseHierarchy();
            //        CodeEditor2.Global.StopBackGroundParse = false;
            //    }
            //}

            //CodeEditor2.Controller.CodeEditor.SetTextFile(TextFile, true);
            //if (NodeSelected != null) NodeSelected();
            Update();
        }

        public override void Update()
        {
            if (DrawIoXmlFile == null)
            {
                return;
            }
            UpdateVisual();
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
    }
}
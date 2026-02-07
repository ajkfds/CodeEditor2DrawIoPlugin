using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace pluginDrawIo.FileTypes
{
    public class DrawIoXmlFile : CodeEditor2.FileTypes.FileType
    {
        public static string TypeID = "DrawIoXmlFile";
        public override string ID { get { return TypeID; } }

        public override bool IsThisFileType(string relativeFilePath, CodeEditor2.Data.Project project)
        {
            if (
                relativeFilePath.ToLower().EndsWith(".drawio")||
                relativeFilePath.ToLower().EndsWith(".drawio.svg")
            )
            {
                return true;
            }
            return false;
        }

        public override async Task<CodeEditor2.Data.File> CreateFile(string relativeFilePath, CodeEditor2.Data.Project project)
        {
            return await Data.DrawIoXmlFile.CreateAsync(relativeFilePath, project);
        }
    }
}

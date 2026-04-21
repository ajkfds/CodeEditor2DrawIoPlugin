using CodeEditor2.Data;
using System.Text.Json;

namespace pluginDrawIo
{

    public class ProjectProperty : CodeEditor2.Data.ProjectProperty
    {
        //public ProjectProperty(CodeEditor2.Data.Project project)
        //{
        //    this.project = project;
        //}
        public ProjectProperty(Project project, ProjectProperty.Setup setup) : base(project, setup)
        {

        }
        public ProjectProperty(Project project) : base(project)
        {

        }

        public override Setup CreateSetup()
        {
            return new Setup(this);
        }

        public override CodeEditor2.Data.ProjectProperty.Setup? CreateSetup(JsonElement jsonElement, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize(jsonElement, typeof(Setup), options) as CodeEditor2.Data.ProjectProperty.Setup;
        }
        public static ProjectProperty.Setup? DeserializeSetup(JsonElement jsonElement, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize(jsonElement, typeof(ProjectProperty.Setup), options) as ProjectProperty.Setup;
        }
        public new class Setup : CodeEditor2.Data.ProjectProperty.Setup
        {
            public Setup() { }
            public Setup(ProjectProperty projectProperty)
            {

            }
            public override string ID { get; set; } = Plugin.StaticID;
            public string test { get; set; } = "verilogTest";
            public override void Write(
                Utf8JsonWriter writer,
                JsonSerializerOptions options)
            {
                JsonSerializer.Serialize(writer, this, typeof(Setup), options);
            }


        }



    }
}

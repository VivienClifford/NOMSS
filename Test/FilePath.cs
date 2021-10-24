using Service.Helper;
using Xunit;

namespace Test
{
    public class FilePath
    {
        [Fact] 
        public void File_Location_Is_Relative_Path()
        {
            var relativePath = JsonDeserializer.GetRelativePath().Replace("NOMSS\\", "NOMSS");

            string[] subfolders = relativePath.Split("\\");

            Assert.True((subfolders[^1] == "NOMSS"));
        }
    }
}

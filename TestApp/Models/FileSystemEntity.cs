namespace TestApp
{
    public class FileSystemEntity
    {
        public FileSystemEntity()
        {
        }

        public FileSystemEntity(string name, long length)
        {
            Name = name;
            Length = length;
        }

        public string Name { get; set; }

        public long Length { get; set; }
    }
}
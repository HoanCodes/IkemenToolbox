using IkemenToolbox.Enums;

namespace IkemenToolbox.Models
{
    public class Tip
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public SystemType Type { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public Tip(string key, string description, string title = null, SystemType type = SystemType.None, string imagePath = null)
        {
            Key = key;
            Description = description;

            Title = title;
            Type = type;
            ImagePath = imagePath;
        }
    }
}

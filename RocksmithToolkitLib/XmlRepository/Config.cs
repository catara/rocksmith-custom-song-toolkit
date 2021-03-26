using System.Xml.Serialization;

namespace RocksmithToolkitLib.XmlRepository
{
    public class Config
    {
        [XmlAttribute]
        public string Key { get; set; }

        [XmlAttribute]
        public string Value { get; set; }

        [XmlAttribute]
        public string Description { get; set; } //bcapi expanding Configs
        [XmlAttribute]
        public string DisplayName { get; set; } //bcapi expanding Configs
        [XmlAttribute]
        public string DisplayGroup { get; set; } //bcapi expanding Configs
        [XmlAttribute]
        public string DisplayPosition { get; set; } //bcapi expanding Configs
    }
}

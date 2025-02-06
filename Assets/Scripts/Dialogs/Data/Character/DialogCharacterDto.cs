using System;

namespace Dialogs.Data.Character
{
    [Serializable]
    public class DialogCharacterDto
    {
        public string Name { get; set; }
        public string PngPath { get; set; }
        public string NameColor { get; set; }
        public string TextColor { get; set; }
        public string BgColor { get; set; }
    }
}
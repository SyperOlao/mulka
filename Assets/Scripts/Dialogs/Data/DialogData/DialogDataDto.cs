using System;
using System.Collections.Generic;

namespace Dialogs.Data.SpeechText
{
    [Serializable]
    public class DialogDataDto
    {
        public string subject;
        public Queue<string> text;
    }
}
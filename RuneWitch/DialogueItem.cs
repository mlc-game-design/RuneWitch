using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneWitch
{
    public class DialogueItem
    {
        public List<string> Dialogues { get; protected set; }
        private float CharacterTimer { get; set; }
        private string CurrentText { get; set; }

    }
}

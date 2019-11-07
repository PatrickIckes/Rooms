using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BeatSloth : Quest
{
    public int NextScene;
    public BeatSloth()
    {
        qr = new List<QuestRequirement>()
            {
                new QuestRequirement("Quest", "Defeat Sloth", 1)
            };
        NextScene = 2;
    }
}

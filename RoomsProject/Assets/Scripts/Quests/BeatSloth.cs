using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BeatSloth : Quest
{

    public static int CurrentScene = 2;
    public BeatSloth()
    {

        qr = new List<QuestRequirement>()
            {
                new QuestRequirement("Quest", "Sloth Reward", 1)
            };
        NextScene = 0;
    }
}

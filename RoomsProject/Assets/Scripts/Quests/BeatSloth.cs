using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BeatSloth : Quest
{
    public const int CurrentScene = (int)Scenes.SlothBossRoom;
    public BeatSloth()
    {
        qr = new List<QuestRequirement>()
            {
                new QuestRequirement("Quest", "Sloth Reward", 1)
            };
        NextScene = (int)Scenes.HubWorld;
    }
}

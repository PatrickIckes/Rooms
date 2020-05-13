using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class BeatPride : Quest
{
    public const int CurrentScene = (int)Scenes.PrideBossFight;
    public BeatPride()
    {
        qr = new List<QuestRequirement>()
            {
                new QuestRequirement("Quest", "Pride Reward", 1)
            };
        NextScene = (int)Scenes.HubWorld;
    }
}

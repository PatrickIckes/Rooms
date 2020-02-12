using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Quests
{
    class BeatEnvy : Quest
    {
        public const int CurrentScene = (int)Scenes.EnvyBossRoom;
        public BeatEnvy()
        {
            qr = new List<QuestRequirement>()
            {
                new QuestRequirement("Quest", "Envy's Mask", 1)
            };
            NextScene = (int)Scenes.HubWorld;
        }
    }
}


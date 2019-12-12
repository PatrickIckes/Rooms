using System.Collections.Generic;
using UnityEngine;

class HallwayQuest : Quest {
    DoorKnobPieces dkp;
    public const int CurrentScene = (int)Scenes.SlothHallway;
    public HallwayQuest() {
        qr = new List<QuestRequirement>()
        {
            new QuestRequirement("Quest", "Doorknob Pieces", 3)
        };
        NextScene = (int)Scenes.SlothBossRoom;
    }
}

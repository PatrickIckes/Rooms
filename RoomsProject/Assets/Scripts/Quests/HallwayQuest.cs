using System.Collections.Generic;
using UnityEngine;

class HallwayQuest : Quest {
    DoorKnobPieces dkp;
    public int NextScene;
    public HallwayQuest() {
        qr = new List<QuestRequirement>()
        {
            new QuestRequirement("Quest", "Doorknob Pieces", 3)
        };
        NextScene = 2;
    }
}

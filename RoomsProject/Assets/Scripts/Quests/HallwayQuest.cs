using System.Collections.Generic;
using UnityEngine;

class HallwayQuest : Quest {
    DoorKnobPieces dkp;
    public int NextScene;
    public static int CurrentScene = 1;
    public HallwayQuest() {
        qr = new List<QuestRequirement>()
        {
            new QuestRequirement("Quest", "Doorknob Pieces", 3)
        };
        NextScene = BeatSloth.CurrentScene;
    }
}

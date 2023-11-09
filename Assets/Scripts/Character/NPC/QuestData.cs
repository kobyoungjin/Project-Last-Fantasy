using System.Collections;
using System.Collections.Generic;

public class QuestData
{
    public string questName; //퀘스트 명
    public int[] npcId; // 퀘스트에 관련된 NPC id 모음


    public QuestData() { }

    //생성자
    public QuestData(string name, int[] npc)
    {
        questName = name;
        npcId = npc;
    }
}

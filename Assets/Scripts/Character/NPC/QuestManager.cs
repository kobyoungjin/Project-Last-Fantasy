using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    Dictionary<int, QuestData> questList;
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        //생성자 이용 (string name, int[] npcid)
        questList.Add(10, new QuestData("고블린 퇴치", new int[] { 1000, 2000 }));
    }
    public int GetQuestTalkIndex(int id) // Npc Id를 받아 퀘스트 번호를 반환하는 함수 
    {
        return questId;
    }
}

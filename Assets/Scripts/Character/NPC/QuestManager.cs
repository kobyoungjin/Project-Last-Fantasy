using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    Dictionary<int, QuestData> questList;

    private int trollKilled;
    private int maxKill = 3;
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        //생성자 이용 (string name, int[] npcid)
        questList.Add(10, new QuestData("고블린 퇴치    " + trollKilled + " / " + maxKill,
                                        new int[] { 1000, 2000 }));

        questList.Add(20, new QuestData("던전의 주인 처지",
                                        new int[] { 5000, 2000 }));

        questList.Add(30, new QuestData("퀘스트 완료",
                                        new int[] { 0 }));
    }
    public int GetQuestTalkIndex(int id) // Npc Id를 받아 퀘스트 번호를 반환하는 함수 
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        if (questActionIndex == questList[questId].npcId.Length)
            NextQuest();

        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    
    public void KilledTroll()
    {
        trollKilled += 1;
    }
}

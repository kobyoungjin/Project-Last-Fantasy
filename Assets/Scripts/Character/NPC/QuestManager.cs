using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex;
    Dictionary<int, QuestData> questList;

    public int trollKilled = 0;
    private int maxKill = 3;

    GameObject questBody;
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    private void Start()
    {
        questBody = GameObject.Find("EtcCanvas").transform.GetChild(3).GetChild(1).gameObject;
    }

    void GenerateData()
    {
        //생성자 이용 (string name, int[] npcid)
        questList.Add(20, new QuestData("한스와 대화하기",
                                        new int[] { 1000, 2000 }));

        questList.Add(30, new QuestData("트롤 퇴치    " + trollKilled + " / " + maxKill,
                                        new int[] { 2000, 3000 }));

        questList.Add(40, new QuestData("던전의 주인 처치하기",
                                        new int[] { 1000, 3000 }));

        questList.Add(50, new QuestData("퀘스트 완료",
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

    public void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    
    public void KilledTroll()
    {
        trollKilled += 1;
        if(questBody.transform.childCount != 0 && trollKilled < maxKill)
        {
            questBody.GetComponentInChildren<Text>().text = "트롤 퇴치    " + trollKilled + " / " + maxKill;
        }
    }
}

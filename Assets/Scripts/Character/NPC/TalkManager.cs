using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }
    void GenerateData()
    {
        talkData.Add(1000, new string[] { "어서오게.",
                                            "어." });

        talkData.Add(10 + 1000, new string[] { "ㄴㅇㄴㅇㅇ.", 
                                                "ㄴㅇㄴㅇㄴㅇ." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        
        if (!talkData.ContainsKey(id))
        {
            if(!talkData.ContainsKey(id - id % 10))
            {   // 퀘스트 맨 처음 대사마자 없을 때.
                // 기본 대사를 가지고 온다.
                return GetTalk(id - id % 100, talkIndex);
            }
            else
            {   // 해당 퀘스트 진행 순서 대사가 없을 때.
                // 퀘스트 맨 처음 대사를 가지고 온다.
                return GetTalk(id - id % 10, talkIndex);
            }
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}

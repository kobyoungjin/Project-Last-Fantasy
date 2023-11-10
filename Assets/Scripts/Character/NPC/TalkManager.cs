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
        talkData.Add(1000, new string[] { "嬢辞神惟.",
                                            "嬢." });

        talkData.Add(10 + 1000, new string[] { "いしいしし.", 
                                                "いしいしいし." });
    }

    public string GetTalk(int id, int talkIndex)
    {
        
        if (!talkData.ContainsKey(id))
        {
            if(!talkData.ContainsKey(id - id % 10))
            {   // 締什闘 固 坦製 企紫原切 蒸聖 凶.
                // 奄沙 企紫研 亜走壱 紳陥.
                return GetTalk(id - id % 100, talkIndex);
            }
            else
            {   // 背雁 締什闘 遭楳 授辞 企紫亜 蒸聖 凶.
                // 締什闘 固 坦製 企紫研 亜走壱 紳陥.
                return GetTalk(id - id % 10, talkIndex);
            }
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}

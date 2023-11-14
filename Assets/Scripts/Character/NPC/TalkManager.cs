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
        talkData.Add(1000, new string[] { "여기는 마을 헤르안입니다."
                                            });

        talkData.Add(10 + 1000, new string[] { "잘오셨습니다. 모험가님",
                                            "저는 이 마을에서 촌장을 하고 있는 스티안이라고 합니다.",
                                            "최근 마을 근처에 있는 동굴에서 마을 주민들이 알 수 없는 소리가 종종 들린다고 합니다.",
                                            "자세한 이야기는 행상인 미르가 자세히 설명해줄겁니다."});

        talkData.Add(11 + 2000, new string[] { "어서오슈 ",
                                            "어떤일로 찾아오셨나?",
                                            "동굴?",
                                            "마을 근처에 있기는 하지",
                                            "거기는 가까이 안가는편이 좋을걸세",
                                            "그 동굴에서 주민들이 계속 알 수 없는 소리가 종종 들린다고 한다네.",
                                            "그리고 오랫동안 방치되어서 무슨일이 생길수도 있고...",
                                            "자네만 괜찮다면 조사 해줄수 있겠나?"
                                            });

        talkData.Add(12 + 1000, new string[] { "모험가님 부디 조심하시기 바랍니다." });
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

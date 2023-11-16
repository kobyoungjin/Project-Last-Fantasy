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

        talkData.Add(10 + 1000, new string[] { "잘오셨습니다. 모험가님 :0",
                                            "저는 이 마을에서 촌장을 하고 있는 스티안이라고 합니다만. :1",
                                            "최근 마을 근처에 있는 동굴에서 마을 주민들이 알 수 없는 소리가 종종 들린다고 하네. :2",
                                            "자세한 이야기는 문지기 한스가 자세히 설명해줄걸세. :3"});

        talkData.Add(20 + 2000, new string[] { "안녕하십니까 모험가님! :0",
                                            "어떤일로 찾아오셨나요?",
                                            "동굴에 관한일로 찾아오셨군요. :1",
                                            "마을 근처에 있기는 합니다만... :2",
                                            "그 전에 부탁이 있습니다. :3",
                                            "요즘 몬스터들이 자주 사람들을 습격합니다. :4",
                                            "아주 골치덩어리입니다 :5",
                                            "모험가님만 괜찮다면 주변에 돌아다니는 트롤들을 퇴치해주시기바랍니다. :6",
                                            "번거롭게 하여 죄송합니다. :7"
                                            });

        talkData.Add(30 + 2000, new string[] { "오셨군요! :0",
                                            "정말 감사드립니다! :1",
                                            "문제를 해결해주셨으니 자세히 알려드리겠습니다. :2",
                                            "최근 주민들이 그 동굴에서 계속 알 수 없는 소리가 자주 들린다고 들었습니다. :3",
                                            "무언가 있는게 틀림없습니다. :4",
                                            "그 곳은 매우 위험합니다. :5",
                                            "오랫동안 방치되어서 무슨일이 생길수도 있고... :6",
                                            "하지만 모험가님만 괜찮다면 조사 해주실수 있으십니까? :7",
                                            "모험가님 부디 조심하시기 바랍니다. :8"
                                             });

        talkData.Add(40 + 2000, new string[] { "정말 감사하네.",
                                            "당신 덕분에 곤란한 일이 사라졌네.",
                                            "오늘은 편하게 지낼수 있도록 해주겠네."
                                             });
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

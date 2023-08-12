using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    //protected override void Init() // 상속 받은 Awake() 안에서 실행됨. "GameScene"씬 초기화
    //{
    //    base.Init(); // 📜BaseScene의 Init()

    //    SceneType = Define.Scene.Game; // 📜GameScene의 씬 종류는 GameScene

    //    Managers.UI.ShowSceneUI<UI_Inven>(); // 인벤토리 UI 생성

    //    for (int i = 0; i < 5; i++) // 📜GameScene의 씬 초기화시 UnityChan 프리팹을 5개 생성한다! 
    //        Managers.Resource.Instantiate("UnityChan");
    //}

    public override void Clear()
    {

    }
}

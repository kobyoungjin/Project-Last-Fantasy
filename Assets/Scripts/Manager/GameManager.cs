using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour//InheritSingleton<GameManager>
{
    Player entity;

    public static bool IsLooping { set; get; } = true;

    //protected override void Awake()
    //{
    //    base.Awake();
        
    //    var objs = FindObjectsOfType<GameManager>();
    //    if (objs.Length == 1)  // GameManager타입의 개수가 1개일때만 
    //        DontDestroyOnLoad(this.gameObject);
    //    else  // 아니면 삭제
    //        Destroy(this.gameObject);


    //    entity = FindObjectOfType<Player>().GetComponent<Player>();
    //    entity.Init(entity.name);

    //    return;
    //}

    void Awake()
    {
        entity = FindObjectOfType<Player>().GetComponent<Player>();
        //entity.Init(entity.name);
        
    }
    
    void Update()
    {
        if (!IsLooping) return;

        
        //entity.Updated();
    }

    public static void Stop(BaseGameEntity entity)
    {
        IsLooping = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InheritSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance != null)
                return instance;

            //  해당 타입의 싱글톤 오브젝트를 찾지 못하면 시도
            instance = FindObjectOfType<T>();

            if (instance != null)
                return instance;

            //  그래도 없으면 만듬
            CreateInstance();


            return instance;
        }

        private set { return; }  //  절대 외부에서 임의로 set 하지 않음.
    }

    private static T CreateInstance()
    {
        GameObject Obj = new GameObject(typeof(T).ToString());  //  대상 클래스 이름으로 게임오브젝트 생성
        instance = Obj.AddComponent<T>();

        return instance;
    }

    protected virtual void Awake()
    {
        instance = FindObjectOfType<T>();
    }

    //  클래스 파괴 시 여기서 파괴하지 않고 자식 클래스가 직접 파괴자를 호출하도록 가상화
    virtual protected void OnApplicationQuit() { }
    virtual protected void OnDestroy() { }
}

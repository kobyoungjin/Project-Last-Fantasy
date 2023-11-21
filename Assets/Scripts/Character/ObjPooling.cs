using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPooling : MonoBehaviour
{
    //[SerializeField] private GameObject objectPrefeb;
    Queue<GameObject> ObjectPool = new Queue<GameObject>(); //오브젝트를 담을 큐
    public static ObjPooling instance = null;

    [SerializeField] private GameObject m1;
    [SerializeField] private GameObject m2;
    [SerializeField] private GameObject m3;
    [SerializeField] private GameObject m4;
    [SerializeField] private GameObject m5;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            ObjectPool.Enqueue(m1);
            ObjectPool.Enqueue(m2);
            ObjectPool.Enqueue(m3);
            ObjectPool.Enqueue(m4);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    GameObject CreateObject() //초기 OR 오브젝트 풀에 남은 오브젝트가 부족할 때, 오브젝트를 생성하기위해 호출되는 함수
    {
        GameObject newObj = Instantiate(m1, instance.transform);
        newObj.gameObject.SetActive(false);

        return newObj;
    }
    public GameObject GetObject() //오프젝트가 필요할 때 다른 스크립트에서 호출되는 함수
    {
        if (ObjectPool.Count > 0) //현재 큐에 남아있는 오브젝트가 있다면,
        {
            GameObject objectInPool = ObjectPool.Dequeue();

            objectInPool.gameObject.SetActive(true);
            objectInPool.transform.SetParent(null);
            return objectInPool;
        }
        else //큐에 남아있는 오브젝트가 없을 때 새로 만들어서 사용
        {
            GameObject objectInPool = CreateObject();

            objectInPool.gameObject.SetActive(true);
            objectInPool.transform.SetParent(null);
            return objectInPool;
        }
    }
    public void ReturnObjectToQueue(GameObject obj) //사용이 완료 된 오브젝트를 다시 큐에 넣을때 호출 파라미터->비활성화 할 오브젝트
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.ObjectPool.Enqueue(obj); //다시 큐에 넣음
    }
}

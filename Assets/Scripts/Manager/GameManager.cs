using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour//InheritSingleton<GameManager>
{
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

    void Start()
    {
 
    }

    void Update()
    {
        
        //entity.Updated();
    }

    public void SetText(GameObject obj)
    {
        GameObject textObj = new GameObject("text");
        
        textObj.AddComponent<TextMeshProUGUI>();
        textObj.GetComponent<TextMeshProUGUI>().text = obj.name;
        RectTransform rectTrans = textObj.GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(obj.name.Length * 18, 40);
        textObj.GetComponent<TextMeshProUGUI>().fontSize = 36;
        textObj.GetComponent<TextMeshProUGUI>().color = Color.black;
        textObj.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Midline;

        Transform parent = obj.transform.parent;
        textObj.transform.parent = null;
        textObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        textObj.transform.parent = parent;

        textObj.transform.SetParent(obj.transform);

        textObj.AddComponent<FloatingText>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour//InheritSingleton<GameManager>
{
    //public static bool IsLooping { set; get; } = true;

    static GameManager instace;
    static GameManager Instance { get { Init(); return instace; } }

    InputManager input = new InputManager();
    public static InputManager Input { get { return Instance.input; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        input.OnUpdate();
    }

    static void Init()
    {
        if (instace == null)
        {
            GameObject obj = GameObject.Find("GameManager");
            if (obj == null)
            {
                obj = new GameObject { name = "GameManager" };
                obj.AddComponent<GameManager>();
            }

            DontDestroyOnLoad(obj);
            instace = obj.GetComponent<GameManager>();
        }
    }

    public void SetText(GameObject obj)
    {
        GameObject textObj = new GameObject("text");
        
        textObj.AddComponent<TextMeshProUGUI>();
        textObj.GetComponent<TextMeshProUGUI>().text = obj.name;
        RectTransform rectTrans = textObj.GetComponent<RectTransform>();
        rectTrans.sizeDelta = new Vector2(obj.name.Length * 20, 40);
        if(obj.name.Length < 6) rectTrans.sizeDelta = new Vector2(obj.name.Length * 30, 40);
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

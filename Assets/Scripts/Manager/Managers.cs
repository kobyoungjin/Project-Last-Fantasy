using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers instance;
    static Managers Instance { get { Init(); return instance; } }

    InputManager input = new InputManager();
    ResourceManager resource = new ResourceManager();
    public static InputManager Input { get { return Instance.input; } }
    public static ResourceManager Resource { get { return Instance.resource; } }

    UIManager ui = new UIManager();
    public static UIManager UI { get { return Instance.ui; } }

    SceneManagerEx scene = new SceneManagerEx();
    public static SceneManagerEx Scene { get { return Instance.scene; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        input.OnUpdate();
    }

    static void Init()
    {
        if (instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            instance = go.GetComponent<Managers>();
            //instance.pool.Init();
        }
    }

    public static void Clear()
    {
        Input.Clear();
    }
}

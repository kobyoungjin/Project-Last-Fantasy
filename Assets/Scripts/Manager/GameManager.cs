using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour//InheritSingleton<GameManager>
{
    //public static bool IsLooping { set; get; } = true;

    static GameManager instace;
    static GameManager Instance { get { Init(); return instace; } }

    InputManager input = new InputManager();
    public static InputManager Input { get { return Instance.input; } }

    private Troll troll;

    public MouseManager mouseManager;
    public TalkManager talkManager;
    public QuestManager questManager;
    public GameObject talkPanel;
    public Text talkText;
    public Text talkName;
    public GameObject obj;
    public bool isAction;
    public int talkIndex;

    GameObject mainCamera;
    GameObject dialogueCamera;

    void Start()
    {
        Init();

        troll = GameObject.Find("Troll/Troll_model").GetComponent<Troll>();
        talkManager = GetComponent<TalkManager>();
        questManager = GetComponent<QuestManager>();
        mouseManager = GetComponent<MouseManager>();
        talkPanel = GameObject.Find("TalkCanvas").transform.GetChild(0).gameObject;
        talkText = talkPanel.transform.GetChild(0).GetComponent<Text>();
        talkName = talkPanel.transform.GetChild(1).GetComponent<Text>();
        mainCamera = GameObject.Find("Camera").transform.GetChild(0).gameObject;
        dialogueCamera = GameObject.Find("Camera").transform.GetChild(1).gameObject;

        Debug.Log(questManager.CheckQuest());
    }

    void Update()
    {
        input.OnUpdate();
    }

    public void Action(GameObject obj)
    {
        this.obj = obj;
        NPC npc = obj.GetComponent<NPC>();

        //Debug.Log(obj.name);

        Talk(npc.id, npc.isNpc, npc.name);

        talkPanel.SetActive(isAction);
        SetDialogue(obj.transform);
    }

    void Talk(int id, bool isNpc, string name)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }
             
        talkText.text = talkData;
        talkName.text = name;
        isAction = true;
        talkIndex++;
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
        if (obj.name.Length < 6) rectTrans.sizeDelta = new Vector2(obj.name.Length * 30, 40);
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
    public void ChangeCamera(GameObject main, GameObject sub)
    {
        main.SetActive(false);
        sub.SetActive(true);
    }

    public void SetDialogue(Transform obj)
    {
        DialogueCamera dialogueCameraScript = dialogueCamera.GetComponent<DialogueCamera>();
        ChangeCamera(mainCamera, dialogueCamera);
        dialogueCameraScript.SetDiaLogTargetObject(obj);
    }

    public Troll GetTrollScript()
    {
        return troll;
    }
}

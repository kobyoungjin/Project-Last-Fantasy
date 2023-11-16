using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour//InheritSingleton<GameManager>
{
    //public static bool IsLooping { set; get; } = true;

    static GameManager instace;
    static GameManager Instance { get { Init(); return instace; } }

    InputManager input = new InputManager();
    public static InputManager Input { get { return Instance.input; } }

    private Troll troll;

    private MouseManager mouseManager;
    private TalkManager talkManager;
    private QuestManager questManager;
    private AnimationManager animationManager;
    public GameObject talkPanel;
    GameObject questBody;
    public Text talkText;
    public Text talkName;
    public GameObject obj;
    public bool isAction;

    public int talkIndex;

    public GameObject mainCamera;
    public GameObject dialogueCamera;

    private GameObject questUI;

    void Start()
    {
        Init();

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Main")
        {
            talkManager = GetComponent<TalkManager>();
            talkPanel = GameObject.Find("EtcCanvas").transform.GetChild(1).gameObject;
            talkText = talkPanel.transform.GetChild(0).GetComponent<Text>();
            talkName = talkPanel.transform.GetChild(1).GetComponent<Text>();
            dialogueCamera = GameObject.Find("Camera").transform.GetChild(1).gameObject;
        }

        troll = GameObject.Find("트롤/Troll_model").GetComponent<Troll>();
        questManager = GetComponent<QuestManager>();
        mouseManager = GetComponent<MouseManager>();
        animationManager = GetComponent<AnimationManager>();
        mainCamera = GameObject.Find("Camera").transform.GetChild(0).gameObject;
        questUI = Resources.Load<GameObject>("Prefabs/UI/QuestUI");
        questBody = GameObject.Find("EtcCanvas").transform.GetChild(3).GetChild(1).gameObject;
    }

    void Update()
    {
        input.OnUpdate();
    }

    public void Action(GameObject obj)
    {
        this.obj = obj;
        NPC npc = obj.GetComponent<NPC>();

        SetDialogue(obj.transform); // 카메라
        Talk(npc.id, npc.isNpc, npc.name);

        talkPanel.SetActive(isAction);
        if (talkPanel.activeSelf == false && !isAction)
            talkPanel.transform.parent.GetChild(3).gameObject.SetActive(true);

        talkIndex++;
    }

    void Talk(int id, bool isNpc, string name)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);
        Debug.Log("퀘스트 인덱스: " + questTalkIndex + "id: "+ id);

        if (talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            ChangeCamera(dialogueCamera, mainCamera);
            questManager.NextQuest();
            if(questBody.transform.childCount != 0)
                Destroy(questBody.transform.GetChild(0).gameObject);
            GameObject instace = Instantiate(questUI, questBody.transform);
            instace.GetComponent<Text>().text = questManager.CheckQuest(id);
            return;
        }
        if(isNpc)
        {
            talkText.text = talkData.Split(':')[0];
        }
        else
        {
            talkText.text = talkData;
        }
        
        if (name == "body")
            name = "한스";
        talkName.text = name;
        isAction = true;
        return;
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

    public AnimationManager GetAnimationManager()
    {
        return animationManager;
    }

    public QuestManager GetQuestManager()
    {
        return questManager;
    }

    public void RejectUI()
    {
        isAction = false;
        talkPanel.transform.parent.gameObject.SetActive(false);
    }

    public void AcceptUI(GameObject gameObject)
    {
        isAction = false;
        if (talkPanel.transform.parent.gameObject.activeSelf)
            talkPanel.transform.parent.gameObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
        
        animationManager.SetFadeScene("Dungeon", 2.0f);
    }
}

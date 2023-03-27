using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    InputManager inputManager;
    
    GameObject mainCamera;
    GameObject dialogueCamera;
    GUIStyle style = new GUIStyle();
    bool isOpen = false;

    void Start()
    {
        mainCamera = GameObject.Find("Camera").transform.GetChild(0).gameObject;
        dialogueCamera = GameObject.Find("Camera").transform.GetChild(1).gameObject;
        inputManager = GameObject.FindObjectOfType<InputManager>().GetComponent<InputManager>();

        style.fontSize = 30;
    }

    void Update()
    {
        if(inputManager.QuitInput)
        {
            ChangeCamera(dialogueCamera, mainCamera);
        }

        if (Input.GetKeyDown(KeyCode.D))
            isOpen = false;
        if (Input.GetKeyDown(KeyCode.F))
            isOpen = true;
    }

    public void ChangeCamera(GameObject main, GameObject sub)
    {
        main.SetActive(false);
        sub.SetActive(true);
    }

    public void SetDialogue(Transform obj) 
    {
        DialogueCamera dialogueCameraScript = FindObjectOfType<DialogueCamera>().GetComponent<DialogueCamera>();
        ChangeCamera(mainCamera, dialogueCamera);
        dialogueCameraScript.SetDiaLogTargetObject(obj);
    }

    public void SetOnGUI(string type)
    {
        //if (type == null) return;

        //if (type == "windowText")
        //{
        //    int windowId = 0;
        //    Rect windowRect = new Rect(10, 10, 300, 100)
        //}
    }

    private void OnGUI()
    {
        if (isOpen)
        {
            int windowId = 0;
            Rect windowRect = new Rect(100, Screen.height - 350, Screen.width - 100 * 2, 300);
            
            windowRect = GUI.Window(windowId, windowRect, WindowFunction, "window");
           
        }
        
    }
    void WindowFunction(int window_id)
    {
        GUI.DragWindow();
        GUI.Label(new Rect(10, 30, 100, 40), "Hello Window");
    }
}

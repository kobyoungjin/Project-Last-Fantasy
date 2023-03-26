using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    InputManager inputManager;
    
    GameObject mainCamera;
    GameObject dialogueCamera;

    void Start()
    {
        mainCamera = GameObject.Find("Camera").transform.GetChild(0).gameObject;
        dialogueCamera = GameObject.Find("Camera").transform.GetChild(1).gameObject;
        inputManager = GameObject.FindObjectOfType<InputManager>().GetComponent<InputManager>();
    }

    void Update()
    {
        if(inputManager.QuitInput)
        {
            ChangeCamera(dialogueCamera, mainCamera);
        }
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
}

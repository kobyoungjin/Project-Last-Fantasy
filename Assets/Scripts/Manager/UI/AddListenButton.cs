using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AddListenButton : MonoBehaviour
{
    private Button btn;
    GameManager gameManager;

    void Start()
    {
        btn = GetComponent<Button>();

        if (btn.gameObject.name == "NewGame")
        {
            btn.onClick.AddListener(() => SceneManager.LoadScene("Heian"));
            
            return;
        }


        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();              

        btn.onClick.AddListener(() => AcceptUI());
    }


    public void AcceptUI()
    {
        this.transform.parent.gameObject.SetActive(false);
        //DontDestroyOnLoad(gameObject);

        gameManager.GetComponent<AnimationManager>().SetFadeScene("Heian", 2.0f);
    }

}

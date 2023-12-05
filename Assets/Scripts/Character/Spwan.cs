using FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spwan : MonoBehaviour
{
    private Transform spwanPos;
    private GameObject player;

    private GameObject Minimap;
    GameManager gameManager;
    GameObject etcCanvas;

    private Transform clearPos;
    private Transform startPos;

    private void Awake()
    {
        GameObject temp = GameObject.Find("DontDest");
        for (int i = 0; i < temp.transform.childCount; i++)
        {
            temp.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Dungeon")
        {
            spwanPos = GameObject.Find("SpwanPos").transform;
            player.transform.position = spwanPos.transform.position;
        }
        else if (scene.name == "Heian")
        {
            GameObject temp = GameObject.Find("DontDest");
            for (int i = 0; i < temp.transform.childCount; i++)
            {
                temp.transform.GetChild(i).gameObject.SetActive(true);
            }
            player.GetComponent<Player>().enabled = true;
            player.GetComponent<CapsuleCollider>().enabled = true;
            clearPos = GameObject.Find("ClearPos").transform;
            startPos = GameObject.Find("StartPos").transform;


            etcCanvas = GameObject.Find("EtcCanvas").gameObject;
            etcCanvas.SetActive(true);

            if (gameManager.isClear)
            {
                
                player.transform.position = clearPos.position;
            }
            else if (gameManager.isFirst)
            {
                gameManager.isFirst = false;

                player.transform.position = startPos.position;
            }

        }

        Minimap = GameObject.Find("Camera").transform.GetChild(2).gameObject;
        
        

        Minimap.GetComponent<Camera>().orthographicSize = 10;
    }
    public GameObject GetPlayer()
    {
        return player;
    }
}
    

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spwan : MonoBehaviour
{
    private Transform spwanPos;
    private GameObject player;

    private GameObject Minimap;
    GameManager gameManager;
    GameObject etcCanvas;

    private Transform clearPos;

    private void Start()
    {
        Minimap = GameObject.Find("Camera").transform.GetChild(2).gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

        Minimap.GetComponent<Camera>().orthographicSize = 10;

        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Dungeon")
        {
            spwanPos = GameObject.Find("SpwanPos").transform;
            player.transform.position = spwanPos.transform.position;
        }
        else
        {
            clearPos = GameObject.Find("ClearPos").transform;
        }

        

        etcCanvas = GameObject.Find("EtcCanvas").gameObject;
        etcCanvas.SetActive(true);

        if(gameManager.isClear)
        {
            Destroy(player);
            player.transform.position = clearPos.position;
        }
            
    }

    public GameObject GetPlayer()
    {
        return player;
    }
}

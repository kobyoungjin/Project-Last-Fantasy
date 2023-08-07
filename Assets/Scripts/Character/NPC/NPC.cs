using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    GameManager gameManager;
    bool isExit = false;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();

        //gameManager.SetText(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

//    private void OnMouseExit()
//    {
//        isExit = true;
//    }

//    private void OnMouseEnter()
//    {
//        isExit = false;
//    }

//    public bool GetMouseExit()
//    {
//        return isExit;
//    }

//    public void SetMouseExit(bool act)
//    {
//        isExit = act;
//    }
}
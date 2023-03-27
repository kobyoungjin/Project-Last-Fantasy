using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    Renderer renderer;
    Shader shader;
    GameManager gameManager;
    bool isExit = false;
    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();
        renderer.sharedMaterial.SetFloat("_OutLineWidth", 0.001f);
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();

        gameManager.SetText(this.gameObject);
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
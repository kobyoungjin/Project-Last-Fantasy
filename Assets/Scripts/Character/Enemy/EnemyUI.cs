using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();

        gameManager.SetText(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState;

public class Player : MonoBehaviour
{
    enum State
    {
        Attack,
        AbilityAttack,
        Running,
        Idle,
        CombatIdle,
    }

    private Camera camera;
    private InputManager inputManager;
    private GameManager gameManager;
    private RaycastHit click;

    Vector3 destination;

    float speed = 4.0f;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
        inputManager = GetComponent<InputManager>();
       // gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    void Update()
    { 
    }

    // 마우스 좌표 설정함수
    public void SetPosition(Vector3 pos)
    {
        destination = pos;
    }

    public Vector3 GetPosition()
    {
        return destination;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public InputManager GetInput()
    {
        return inputManager;
    }

    public Camera GetMainCamera()
    {
        return camera;
    }
}

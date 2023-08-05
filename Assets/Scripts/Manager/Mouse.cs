using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    int mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster);

    PlayerState state;
    Vector3 destPos;

    GameObject lockTarget;

    enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    CursorType cursorType = CursorType.None;

    InputManager inputManager;
    NPCDialogue npcDialogue;
    Renderer renderers;
    Transform selectedTarget;
    RaycastHit hit;

    Texture2D attackIcon;
    Texture2D handIcon;

    

    void Start()
    {
        //npcDialogue = GameObject.FindObjectOfType<NPCDialogue>().GetComponent<NPCDialogue>();
        attackIcon = Managers.Resource.Load<Texture2D>("Cursors/Cursor 64/Cursor_Attack");
        handIcon = Managers.Resource.Load<Texture2D>("Cursors/Cursor 64/Cursor_Basic2");

        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    void Update()
    {
        Raycast();
        UpdateMouseCursor();
    }

    void Raycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);
        int layer = 1 << LayerMask.NameToLayer("NPC");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            Transform obj = hit.transform;
            SelectTarget(obj);

            //if(inputManager.MoveInput)
            //{
            //    npcDialogue.SetDialogue(obj);
            //}

        }
        else
        {
            ClearTarget();
        }
    }

    // outline 강조해주는 함수
    void AddOutline(Transform obj)
    {
        if (obj == null) return;

        renderers = obj.GetComponent<Renderer>();
        renderers.sharedMaterial.SetFloat("_OutLineWidth", 0.03f);
    }

    // outline 풀어주는 함수
    void RemoveOutline(Renderer renderer)
    {
        if (renderer != null)
        {
            renderer.sharedMaterial.SetFloat("_OutLineWidth", 0.001f);
        }
    }

    // 타켓해제
    void ClearTarget()
    {
        if (selectedTarget == null) return;

        selectedTarget = null;
        RemoveOutline(renderers);
    }

    // 타켓 선택
    void SelectTarget(Transform obj)
    {
        if (obj == null) return;

        ClearTarget();
        selectedTarget = obj;
        AddOutline(obj);
    }

    void UpdateMouseCursor()
    {
        if (state == PlayerState.die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(attackIcon, new Vector2(attackIcon.width / 5, 0), CursorMode.Auto);
                    cursorType = CursorType.Attack;
                }
            }
            else
            {
                if (cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(handIcon, new Vector2(handIcon.width / 3, 0), CursorMode.Auto);
                    cursorType = CursorType.Hand;
                }
            }
        }
    }

    void OnMouseEvent(Define.MouseEvent evt)
    {
        if (state == PlayerState.die)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, mask);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        destPos = hit.point;
                        state = PlayerState.running;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        {
                            lockTarget = hit.collider.gameObject;
                        }
                        else
                        {
                            lockTarget = null;
                        }
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (lockTarget != null)
                        destPos = lockTarget.transform.position;
                    else if (raycastHit)
                        destPos = hit.point;
                }
                break;
            case Define.MouseEvent.PointerUp:
                lockTarget = null;
                break;
        }
    }
    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Click)
            return;

        if (state == PlayerState.die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, mask))
        {
            destPos = hit.point;
            state = PlayerState.running;

            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                Debug.Log("Monster");
            }
            else
            {
                Debug.Log("Ground");
            }
        }
    }
}

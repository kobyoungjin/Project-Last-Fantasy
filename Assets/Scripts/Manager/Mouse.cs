using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    int mask = (1 << (int)Define.Layer.Ground | 1 << (int)Define.Layer.Monster | 1 << (int)Define.Layer.NPC);

    PlayerState state;
    Vector3 destPos;

    GameObject lockTarget;
    
    enum CursorType
    {
        None,
        Attack,
        Hand,
        Default,
    }

    CursorType cursorType = CursorType.None;

    InputManager inputManager;
    NPCDialogue npcDialogue;
    Renderer renderers;
    Transform selectedTarget;

    public Texture2D attackIcon;
    public Texture2D handIcon;
    public Texture2D defaultIcon;
      
    void Start()
    {
        //npcDialogue = GameObject.FindObjectOfType<NPCDialogue>().GetComponent<NPCDialogue>();
        attackIcon = Managers.Resource.Load<Texture2D>("Cursors/Used/Attack");
        handIcon = Managers.Resource.Load<Texture2D>("Cursors/Used/Hand");
        defaultIcon = Managers.Resource.Load<Texture2D>("Cursors/Used/Default");

        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    void Update()
    {
        UpdateCursorAndOutLine();
    }

    // outline 강조해주는 함수
    void AddOutline(Transform obj, float width, Color color)
    {
        if (obj == null) return;

        renderers = obj.GetComponent<Renderer>();
        renderers.sharedMaterial.SetFloat("_OutLineWidth", width);
        renderers.sharedMaterial.SetColor("_OutLineColor", color);
    }

    // outline 풀어주는 함수
    void RemoveOutline(Renderer renderer)
    {
        if (renderer != null)
        {
            renderer.sharedMaterial.SetFloat("_OutLineWidth", 0);
        }
    }

    // 타켓해제
    void ClearTarget()
    {
        if (selectedTarget == null) return;

        RemoveOutline(renderers);
        selectedTarget = null;        
    }

    // 타켓 선택
    void SelectTarget(Transform obj, float width, Color color)
    {
        if (obj == null) return;

        ClearTarget();
        selectedTarget = obj;
        AddOutline(obj, width, color);
    }

    void UpdateCursorAndOutLine()
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

                    SelectTarget(hit.transform, 0.0003f, Color.red);
                }
            }
            else if(hit.collider.gameObject.layer == (int)Define.Layer.NPC)
            {
                if (cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(handIcon, new Vector2(handIcon.width / 3, 0), CursorMode.Auto);
                    cursorType = CursorType.Hand;

                    SelectTarget(hit.transform, 0.02f, Color.yellow);
                }
            }
            else
            {
                if (cursorType != CursorType.Default)
                {
                    Cursor.SetCursor(defaultIcon, new Vector2(defaultIcon.width / 3, 0), CursorMode.Auto);
                    cursorType = CursorType.Default;

                    ClearTarget();
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
                Debug.Log("layer 8 Monster");
            }
            else if (hit.collider.gameObject.layer == (int)Define.Layer.NPC)
            {
                Debug.Log("layer 7 NPC");
            }
        }
    }
}

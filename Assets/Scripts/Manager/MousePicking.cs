using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePicking : MonoBehaviour
{
    InputManager inputManager;
    NPCDialogue npcDialogue;
    Renderer renderers;
    RaycastHit hit;
    Transform selected;
    NPC npc;
    bool result;
    bool isSelected = false;
    private void Start()
    {
        npc = GameObject.FindObjectOfType<NPC>().GetComponent<NPC>();
        //npcDialogue = GameObject.FindObjectOfType<NPCDialogue>().GetComponent<NPCDialogue>();
    }

    void Update()
    {
        result = Raycast();
        if (!result)
        {
            Debug.Log("MousePicking Update 오류");
            return;
        }

        Debug.Log(isSelected);
    }

    bool Raycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);
        int layer = 1 << LayerMask.NameToLayer("NPC");

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
        {
            Transform obj = hit.transform;

            if (isSelected)
            {
                isSelected = false;
                result = ClearTarget();
                if (!result)
                {
                    Debug.Log("타켓을 해제하지 못했습니다.");
                    return false;
                }
            }

            result = SelectTarget(obj);
            if (!result)
            {
                Debug.Log("대상을 선택하지 못했습니다.");
                return false;
            }
            isSelected = true;
        }
        else
        {
            isSelected = false;
        }

        return true;
    }

    // outline 강조해주는 함수
    bool AddOutline(Transform obj)
    {
        if (obj == null)
        {
            Debug.Log("obj error");
            return false;
        }

        renderers = obj.GetComponent<Renderer>();
        renderers.sharedMaterial.SetFloat("_OutLineWidth", 0.03f);
        return true;
    }

    // outline 풀어주는 함수
    bool RemoveOutline(Renderer renderer)
    {
        if (renderer == null)
        {
            Debug.Log("renderer할 대상이 없습니다");
            return false;
        }

        renderer.sharedMaterial.SetFloat("_OutLineWidth", 0.001f);

        return true;
    }

    // 타켓해제
    public bool ClearTarget()
    {
        if (selected == null)
        {
            Debug.Log("해제할 타겟이 없습니다.");
            return false;
        }

        result = RemoveOutline(renderers);
        if (!result)
        {
            Debug.Log("강조 아웃라인을 제거하지 못했습니다.");
            return false;
        }
        selected = null;

        return true;
    }

    // 타켓 선택
    bool SelectTarget(Transform obj)
    {
        if (obj == null)
        {
            Debug.Log("obj error");
            return false;
        }
        
        selected = obj;

        result = AddOutline(obj);
        if (!result)
        {
            Debug.Log("대상을 강조하지 못했습니다");
            return false;
        }


        return true;
    }

}

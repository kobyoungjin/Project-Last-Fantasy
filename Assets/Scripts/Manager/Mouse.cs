using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    InputManager inputManager;
    NPCDialogue npcDialogue;
    Renderer renderers;
    Transform selectedTarget;
    RaycastHit hit;

    private void Start()
    {
        //npcDialogue = GameObject.FindObjectOfType<NPCDialogue>().GetComponent<NPCDialogue>();
    }

    void Update()
    {
        Raycast();
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

}

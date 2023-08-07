using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class UI_EventHandler : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

    private RectTransform rectTransform;
    private Camera camera_ui;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        camera_ui = GameObject.Find("Camera_UI").GetComponent<Camera>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData);
    }
    private void Update()
    {
        Vector3 viewPos = camera_ui.WorldToViewportPoint(transform.position);

        if (viewPos.x < 0f) viewPos.x = 0f;
        if (viewPos.y < 0f) viewPos.y = 0f;
        if (viewPos.x > 1f) viewPos.x = 1f;
        if (viewPos.y > 1f) viewPos.y = 1f;

        transform.position = camera_ui.ViewportToWorldPoint(viewPos);
    }
}

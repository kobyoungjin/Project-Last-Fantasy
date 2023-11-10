using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar, Interaction
    }

    Status status;
    public virtual void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        status = transform.parent.GetComponent<Status>();
    }
    void Start()
    {
        Init();
    }

    void Update()
    {
        Transform parent = gameObject.transform.parent;
        if (transform.parent.name == "Troll_model")
            transform.position = parent.position + Vector3.up * (transform.parent.GetChild(2).GetComponent<Collider>().bounds.size.y);
        else
            transform.position = parent.position + Vector3.up * parent.GetComponent<Collider>().bounds.size.y;

        transform.rotation = Camera.main.transform.rotation;  // 빌보드

        float ratio = status.Hp / (float)status.MaxHp;
        if(ratio <= 0)
        {
            ratio = 0;
            Destroy(gameObject);
        }
        SetHpRatio(ratio);  // 슬라이더 값 매 프레임마다 갱신
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameEntity : MonoBehaviour
{
    private static int m_iNextVaiID = 0;

    private int id;
    public int ID
    {
        set
        {
            id = value;
            m_iNextVaiID++;
        }
        get => id;
    }

    private string entityName;
    private string personalColor;

    public virtual void Init(string name)
    {
        ID = m_iNextVaiID;
        entityName = name;
    }

    //public abstract void Updated();
}

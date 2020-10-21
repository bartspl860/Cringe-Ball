using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireHandler : MonoBehaviour {

    public Player[] p;
    [SerializeField] private SpriteRenderer fire;
    
    void Update()
    {
        if (p[0].boost || p[1].boost)
        {
            fire.enabled = true;
        }
        else
        {
            fire.enabled = false;
        }

        if (p[0].boost && p[1].boost)
        {
            fire.color = new Color(194f/255f, 1f, 0f);
        }

        if (p[0].boost && !p[1].boost)
        {
            fire.color = new Color(1f, 60f/255f, 0f);
        }
        if (p[1].boost && !p[0].boost)
        {
            fire.color = new Color(1f/255f, 234f/255f, 245f/255f);
        }
    }
}

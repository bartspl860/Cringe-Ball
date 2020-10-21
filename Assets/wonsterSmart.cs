using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wonsterSmart : MonoBehaviour
{
    public Player[] p;
    public BallisCounting bic;

    void Start()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Player");
        GameObject tempBall = GameObject.FindGameObjectWithTag("ball");

        bic = tempBall.GetComponent<BallisCounting>();

        for(int i = 0; i < temp.Length; i++)
        {
            p[i] = temp[i].GetComponent<Player>();
        }        
    }

    void FixedUpdate()
    {
        for(int i = 0; i < p.Length; i++)
        {
            if (Vector2.Distance(p[i].transform.position, transform.position) < 1f)
            {
                p[i].boost = true;
                bic.doIenumerator();
                Destroy(this.gameObject);
            }
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallisCounting : MonoBehaviour
{
    [SerializeField]
    private TMP_Text score;
    private float score_red = 0;
    private float score_blue = 0;
    [SerializeField]
    private Rigidbody2D rb2d_ball;    
    
    [SerializeField]
    private PhysicsMaterial2D odbicie;

    public Player p;
    public Player p1;

    [SerializeField]
    private Canvas menu_cv;
    [SerializeField]
    private Canvas settings_menu;
    
    public GameObject alsoTry;

    private bool onlyOnce = true;    

    [SerializeField]
    private Transform fireball;

    [SerializeField]
    private GameObject wonster;
    [SerializeField]
    private Vector3[] wonsterSpawnPoints;
    
    private IEnumerator wait()
    {        
        yield return new WaitForSeconds(1);
        wasGoal();
    }
    IEnumerator alsoTryWait()
    {
        float a = 0f;
        for(int i = 0; i < 10; i++)
        {            
            alsoTry.transform.localScale = new Vector3(1.5f-a, 1.5f-a, 1f);
            yield return new WaitForSecondsRealtime(0.05f);
            a += 0.05f;
        }
        for (int i = 0; i < 10; i++)
        {
            alsoTry.transform.localScale = new Vector3(1.5f - a, 1.5f - a, 1f);
            yield return new WaitForSecondsRealtime(0.05f);
            a -= 0.05f;
        }
        StartCoroutine(alsoTryWait());
    }
    void Start()
    {
        float x = -5f;
        float y = -3f;
        int countX = 0;
        int countY = 0;

        for(int i = 0; i < wonsterSpawnPoints.Length; i++)
        {
            wonsterSpawnPoints[i] = new Vector3(x+countX, y+countY, 3f);
            countX++;
            if(countX > 10)
            {
                countX = 0;
                countY++;
            }
        }
        GameObject temp = Instantiate(wonster);
        temp.transform.position = wonsterSpawnPoints[5];

        menu_cv.enabled = true;
        Time.timeScale = 0f;
        StartCoroutine(alsoTryWait());        
        
    }

    private IEnumerator randSpawnPoinForWonster()
    {
        yield return new WaitForSeconds(15f);        
        int rand = Random.Range(0, 76);
        GameObject temp = Instantiate(wonster);
        temp.transform.position = wonsterSpawnPoints[rand];
    }
    public void doIenumerator()
    {
        StartCoroutine(randSpawnPoinForWonster());
    }
    void FixedUpdate()
    {        
        Vector3 dir = rb2d_ball.velocity.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x);
        angle *= Mathf.Rad2Deg;        

        fireball.localEulerAngles = new Vector3(0f, 0f, angle);
        
        if (Input.GetKeyDown("escape")){

            menu_cv.enabled = true;
            Time.timeScale = 0f;
        }
        score.text = "<color=red>red</color> " + score_red + " : " + score_blue + " <color=green>green</color>";
        if (transform.position.x <= -7.115f)
        {
            odbicie.bounciness = 0.1f;
            StartCoroutine(wait());
            if (onlyOnce)
            {
                score_blue++;
                onlyOnce = false;
            }                
        }
        else if (transform.position.x >= 7.115f)
        {
            odbicie.bounciness = 0.1f;
            StartCoroutine(wait());
            if (onlyOnce)
            {
                score_red++;
                onlyOnce = false;
            }
        }
    }
    public void toggleMenu()
    {        
        Time.timeScale = 1f;
        menu_cv.enabled = false;                
    }
    public void exit()
    {
        Application.Quit();
    }
    void wasGoal()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        rb2d_ball.velocity = Vector2.zero;
        p.rb2d.velocity = Vector2.zero;
        p1.rb2d.velocity = Vector2.zero;
        p.wasGoal = true;
        p1.wasGoal = true;
        onlyOnce = true;
        odbicie.bounciness = 0.6f;
    }
    public void reset()
    {
        wasGoal();
        toggleMenu();
        score_red = 0;
        score_blue = 0;
        onlyOnce = true;
    }
    
    public void show_settings()
    {
        menu_cv.enabled = false;
        settings_menu.enabled = true;
    }

    public void hide_settings()
    {
        menu_cv.enabled = true;
        settings_menu.enabled = false;
    }
}

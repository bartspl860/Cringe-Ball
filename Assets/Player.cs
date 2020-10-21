using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode kick;

    [SerializeField]
    private Transform t_player;
    
    public Rigidbody2D rb2d;
    [SerializeField]
    private Collider2D c2d;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private Vector3 defaultPosition;

    private float speed = 800f;

    [SerializeField]
    private Sprite normal;
    [SerializeField]
    private Sprite kick_sprite;

    [SerializeField]
    private GameObject ball;
    
    private Collider2D c2d_ball;    
    private Rigidbody2D rb2d_ball;
    private Transform t_ball;

    public bool wasGoal = false;

    public bool boost = false;

    [SerializeField]
    private float shotEfficiency;
    [SerializeField]
    private float shotEfficiency_afterBoost;        


    void Start()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("ball");
        c2d_ball = ball.GetComponent<CircleCollider2D>();
        rb2d_ball = ball.GetComponent<Rigidbody2D>();
        t_ball = ball.GetComponent<Transform>();                
    }
    private IEnumerator shortBoost()
    {        
        yield return new WaitForSeconds(5);
        boost = false;
    }
    void FixedUpdate()
    {        
        if (Input.GetKey(up)){ 
            rb2d.AddForce(transform.up * Time.deltaTime * speed); 
        }
        if (Input.GetKey(left))
        {
            rb2d.AddForce(transform.right * Time.deltaTime * -speed);
        }
        if (Input.GetKey(down))
        {
            rb2d.AddForce(transform.up * Time.deltaTime * -speed);
        }
        if (Input.GetKey(right))
        {
            rb2d.AddForce(transform.right * Time.deltaTime * speed);
        }

        Vector2 direction = (t_ball.position - transform.position).normalized;        

        if (Input.GetKey(kick))
        {
            sr.sprite = kick_sprite;
            speed = 400f;

            if (Vector2.Distance(t_player.position, t_ball.position) < 1f)//c2d.IsTouching(c2d_ball)
            {
                if (!boost)
                {
                    rb2d_ball.AddForce(direction * shotEfficiency);
                }                    
                else
                {
                    rb2d_ball.AddForce(direction * shotEfficiency_afterBoost);
                }
                    
            }   
        }
        else
        {
            speed = 800f;
            sr.sprite = normal;
        }
        if (boost)
        {            
            StartCoroutine(shortBoost());
        }                    
        if (wasGoal)
        {            
            t_player.position = defaultPosition;
            wasGoal = false;
        }        
    }
}

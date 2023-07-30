using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;
using static Cinemachine.CinemachineTargetGroup;

public class PlayerMovement : MonoBehaviour
{
    
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator ani;
    [SerializeField] private float speed;
    private float driX;

    [SerializeField] private LayerMask jumbableGround;
    private enum MovementState { idle, running, jumping, falling };

    [SerializeField] private AudioSource jumpSoundEffect;

    //nhảy 2 lần
    public Transform ViTri_Nhay;
    [SerializeField] private float jump = 12f;
    private bool nhay;
    private bool nhayDoi;
    public LayerMask san;

    [SerializeField] private GameObject thongBao;
    [SerializeField] private GameObject menu;


    public ParticleSystem Bui;

    /*private float horizontalInput, forwardInput;*/


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();



    }

    // Update is called once per frame
    void Update()
    {
        driX = Input.GetAxisRaw("Horizontal");
        /*forwardInput = Input.GetAxis("Vertical");*/

        rb.velocity = new Vector2(driX * speed,rb.velocity.y  );


        nhay = Physics2D.OverlapCircle(ViTri_Nhay.position, 0.5f, san);

        if (nhay && !Input.GetKey(KeyCode.Space))
        {
            nhayDoi = false;

        }
        else {
            CreateBui();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (nhay || nhayDoi)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
                nhayDoi = !nhayDoi;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            thongBao.SetActive(false);
        }
        // ấn giữ TAB để hiện map
        if (Input.GetKey(KeyCode.Tab))
        {
            menu.SetActive(true);
            Time.timeScale = 0;

        }
        else 
        {
            menu.SetActive(false);
            Time.timeScale = 1;

        }




        UpdateAnimation();
    }




    void UpdateAnimation()
    {
        MovementState state;

        if (driX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;

        }
        else if (driX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;

        }
        else
        {
            state = MovementState.idle;
                
        }

        if(rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        ani.SetInteger("state",(int) state);

    }

    private bool IsGrounded()
    {
       return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumbableGround);
    }



    public void CreateBui()
    {
        Bui.Play();
    }

}

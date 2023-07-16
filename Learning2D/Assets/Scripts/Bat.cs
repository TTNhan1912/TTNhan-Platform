using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    private Animator ani;
    [SerializeField] private float bat = 23f;
    private Rigidbody2D rb;
    private bool nhay;
    private bool nhayDoi;
    public Transform ViTri_Nhay;
    public LayerMask san;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        nhay = Physics2D.OverlapCircle(ViTri_Nhay.position, 0.2f, san);


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bat"))
        {
            Jumpppp();
        }
        
    }



    private void Jumpppp()
    {
        if (nhay || nhayDoi)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, bat);
            nhayDoi = !nhayDoi;
        }
    }
}

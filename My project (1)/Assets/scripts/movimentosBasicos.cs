using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimentosBasicos : MonoBehaviour
{
    private Rigidbody2D lcGamers;
    private Animator anim;
    public float velocidade;
    public float forcaPulo;
    private float inputMovimentoHorizontal;




    void Start()
    {
        lcGamers = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inputMovimentoHorizontal = Input.GetAxisRaw("Horizontal");

        lcGamers.velocity = new Vector2(inputMovimentoHorizontal * velocidade, lcGamers.velocity.y);

        
        anim.SetInteger("walk", (int)inputMovimentoHorizontal);
        if (Input.GetButtonDown("Jump"))
        {
            lcGamers.AddForce(new Vector2(0, forcaPulo));
        }
    }
}
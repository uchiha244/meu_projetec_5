using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class comandosBasicos : MonoBehaviour

{
    public int velocidade; //defina a velocidade do movimentação
    private Rigidbody2D rbPlayer; // criar variavel para receber os componentes de fisica
    public float forcaPulo; //define a força do pulo

    private Animator anim;

    private SpriteRenderer spriteRb;

    private int imputMovimento;

    private int quantidadeMoedas;

    private int quantidadeMoedas2;



    public bool sensor; //sensor para verificar se está colidindo com o chão
    public Transform posicaoSensor; // Posição onde o será posicionado
    public LayerMask layerChao; // camada de interação
    public bool verificaDirecao;
    public float velocidadeTiro;

    public TextMeshProUGUI textoCoin;

    // CODIGO PARA ATIRAR;
    public GameObject projetil; //primeiro criar uma variavel para instanciar o projetil na cna

    public Transform localDisparo;


    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>(); //DEFINIDO COMPONENTE DA PENSORNAGEM
        anim = GetComponent<Animator>();
        spriteRb = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float movimentoX = Input.GetAxisRaw("Horizontal");


        rbPlayer.velocity = new Vector2(movimentoX * velocidade, rbPlayer.velocity.y);

        if (Input.GetButtonDown("Jump") && sensor == true) // movimento de pulo atravez
        {
            rbPlayer.AddForce(new Vector2(0, forcaPulo)); //Movimento do salto/força;
        }

        anim.SetInteger("walk", (int)movimentoX); //Atribui o valor da variavel ao parâmentro do animator
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("attack");
            GameObject temp = Instantiate(projetil);

            temp.transform.position = localDisparo.transform.position;

            temp.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(8, 0);

            Destroy(temp.gameObject, 2);

        }
        if (movimentoX > 0)
        {
            spriteRb.flipX = false; //aperta a seta da direita atribui o valor de falso a variavel flip  no Spriterender
        } else if (movimentoX < 0)
        {
            spriteRb.flipX = true;
        }
        anim.SetBool("jump", sensor);

        if (imputMovimento > 0 && verificaDirecao == true)
        {
            flip();
        }
        else if (imputMovimento < 0 && verificaDirecao == false)
        {
            flip();
        }
    }


    private void FixedUpdate()
    {
        //Cria um sensor em posição definida com raio e layer tb definidas
        sensor = Physics2D.OverlapCircle(posicaoSensor.position, 0.2f, layerChao);
    }
    //Troca a direção que o personagem está olhando  
    public void flip()
    {
        verificaDirecao = !verificaDirecao;

        float x = transform.localScale.x * -1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        velocidadeTiro *= -1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "coin")
        {
            quantidadeMoedas += 1;
            

            textoCoin.text = quantidadeMoedas.ToString();

            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "megacoin")
        {
            quantidadeMoedas += 5;

            textoCoin.text = quantidadeMoedas.ToString();

            Destroy(collision.gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;
    
    public int coin;
    public int heart;
    // public int speed;
    public int score;


    public GameManager manager;

    public AudioSource rockSound;
    public AudioSource heartSound;
    public AudioSource coinSound;
    

    // public int score;

    public int maxCoin;
    public int maxHeart;

    float hAxis;
    float vAxis;

    bool rDown;
    bool isDamage;
    bool isDead ;


    Vector3 moveVec;

    Animator anim;


    GameObject nearObject;

    


    void Awake() //초기화
    {
        anim = GetComponentInChildren<Animator>();
        Debug.Log(PlayerPrefs.GetInt("MaxScore"));
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
    }

    void GetInput(){
        hAxis = Input.GetAxisRaw("Horizontal"); // GetAxis = Axis 값을 정수로 반환
        vAxis = Input.GetAxisRaw("Vertical"); // Horizontal, Vertical = Input Manager 에서 관리
    }

    void Move(){
        if(!isDead) {
            moveVec = new Vector3(hAxis,0,vAxis).normalized;

        transform.position += moveVec * speed * Time.deltaTime;
        }
        
        anim.SetBool("isWalk", moveVec != Vector3.zero);
    }

    void Turn(){
        if(!isDead) transform.LookAt(transform.position + moveVec);
    }


    void OnTriggerEnter(Collider other){
        if(other.tag=="Rock"){
            Rock rock = other.GetComponent<Rock>();
            heart -= rock.damage;
            Debug.Log(rock.damage);
            // heart -= 1;
            if(heart<=0 && !isDead) OnDie(); 
            Destroy(rock.gameObject);
            rockSound.Play();

        }

        if(other.tag=="Item"){
            Item item = other.GetComponent<Item>();
            switch(item.type){
                case Item.Type.Coin:
                    coin += item.value;
                    if(coin>maxCoin) coin = maxCoin;
                    coinSound.Play();
                    break;
                case Item.Type.Heart:
                    heart += item.value;
                    if(heart>maxHeart) heart = maxHeart;
                    heartSound.Play();
                    break;
            }
            Destroy(other.gameObject);
        }

        

    }

    void OnDie(){
        isDead = true;
        anim.SetTrigger("doDie");
        // transform.position += moveVec * 0 * Time.deltaTime;
        manager.GameOver();
    }


}

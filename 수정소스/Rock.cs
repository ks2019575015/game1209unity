using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Rigidbody rigid;
    BoxCollider boxCollider;
    // public Player player;
    // public Transform target;
    public int score;
    public int damage;
    
    private void Awake(){
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other){
        if(other.tag =="Ground"){
            Destroy(gameObject);
            
            
        }
    }

    
}

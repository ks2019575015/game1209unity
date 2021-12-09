using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //enum : 열거형 타입
    public enum Type { Coin, Heart, Speed};
    public Type type;
    public int value;

    

    void OnTriggerEnter(Collider other){
        if(other.tag =="Ground"){
            Destroy(gameObject,2);
        }
    }
}

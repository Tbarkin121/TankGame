using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    private Inventory inventory;
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("TestDrone").GetComponent<Inventory>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("TestDrone")){
            for(int i = 0; i < inventory.slots.Length; i++){
                if (inventory.isFull[i] == false){
                    inventory.isFull[i] = true;
                    break;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private float counter = 0;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        transform.Translate(0f, 0.01f, 0f);
        if(counter>1){
            Destroy(gameObject);
        }
        counter += Time.deltaTime;
    }
}

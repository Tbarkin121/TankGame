using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.UI;
public class HealthDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    private int health = 5;
    public Text healthText;
    void Update()
    {
        healthText.text = "Health : " + health;
        if(Input.GetKeyDown(KeyCode.Space)){
            health--;
        }
    }
}

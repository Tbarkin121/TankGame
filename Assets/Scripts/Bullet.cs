using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 20f;
    private float counter = 0;
    public Rigidbody2D rb;
    public GameObject damageNum;
    void Start()
    {
        rb.velocity = transform.up * speed;
    }
    void Update()
    {
        if(counter>5){
            Destroy(gameObject);
        }
        counter += Time.deltaTime;
    }
    void OnTriggerEnter2D (Collider2D hitInfo)
    {
        // Debug.Log(hitInfo.name);
        Tank enemy = hitInfo.GetComponent<Tank>();
        if(enemy != null)
        {
            enemy.TakeDamage(1);
        }
        Instantiate(damageNum, transform.position, new Quaternion(0,0,0,1));
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shell : MonoBehaviour
{
    public Animator animator;
    public string keyS1;
    public string keyS2;
    public string keyS3;
    public string keyS4;
    public string keyS5;
    float[] Angle = {0f, 0f}; //Yaw, Pitch
    float[] Velocity = {0f, 0f, 0f};
    float moveSpeed = 0.20f;
    float moveDecelerationAir = 0.01f;
    float moveDecelerationGround = 0.1f;
    bool ShellLive = false;
    bool ExplosiveLive = false;
    float Gravity = 0.12f;

    // Start is called before the first frame update
    void Start()
    {
        Angle[0] = Mathf.Deg2Rad*0;
        Angle[1] = Mathf.Deg2Rad*30 ;
        transform.Rotate(0,0, Angle[0]*180f/Mathf.PI);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Velocity[0] = 0;
        Velocity[1] = moveSpeed*Mathf.Cos(Angle[1]);
        Velocity[2] = moveSpeed*Mathf.Sin(Angle[1]);
        // ShellLive = true;
        ExplosiveLive = true;
        // animator.SetTrigger("Fire");
    }   

    // Update is called once per frame
    void Update()
    {
        bool go = false;
        go = (Input.GetKeyDown(keyS1)) ? true : go;
        go = (Input.GetKeyUp(keyS1)) ? false : go;

        // Debug.Log("Hello: " + projectile_angle);
        if(go){
            ShellLive = true;
            animator.SetTrigger("Fire");
        }
        if(ShellLive==true){
            float projectile_angle = Mathf.Atan(Velocity[2]/Velocity[1]);
            if(projectile_angle > 1f){
                animator.SetInteger("AnimLvl",1);
            }else if(projectile_angle>0.6f){
                animator.SetInteger("AnimLvl",2);
            }else if(projectile_angle>0.2f){
                animator.SetInteger("AnimLvl",3);
            }else if(projectile_angle>-0.5f){
                animator.SetInteger("AnimLvl",4);
            }else{
                animator.SetInteger("AnimLvl",5);
            }

            if(transform.position.z<0.01 && ExplosiveLive==true){
                Velocity[1] -= moveDecelerationAir*Velocity[1];
                Velocity[2] -= Gravity*Time.deltaTime; 
                transform.Translate(Velocity[0], Velocity[1], -Velocity[2]);
            }else{
                if(ExplosiveLive==true){
                    ExplosiveLive=false;
                    //Trigger Explosition Logic
                }
                transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                Velocity[1] -= moveDecelerationGround*Velocity[1];
                transform.Translate(Velocity[0], Velocity[1], 0f);
            }
            float CurrentVelocity = Mathf.Sqrt(Mathf.Pow(Velocity[0],2) + Mathf.Pow(Velocity[1],2) + Mathf.Pow(Velocity[2],2));
            if(CurrentVelocity < 0.1 && ExplosiveLive==false){
                ShellLive = false;
            }
            }else{
                animator.SetInteger("AnimLvl",0);
            }
        }
}
    

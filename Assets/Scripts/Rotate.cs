using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Animator animator;
    public string keyAimRight;
    public string keyAimLeft;
    public string keyFire;
    float rotateSpeedRight = 0f;
    float rotateSpeedLeft = 0f;
    float rotateAcceleration = 4f;
    float rotateDeceleration = 10f;
    float rotateSpeedMax = 130f;
    float gunDownTime = 10;
    float gunCurrentTime = 0;
    bool gunFire = false;
    bool aimRight = false;
    bool aimLeft = false;
    void Update()
    {
        aimRight = (Input.GetKeyDown(keyAimRight)) ? true : aimRight;
        aimRight = (Input.GetKeyUp(keyAimRight)) ? false : aimRight;
        if (aimRight)
        {
            rotateSpeedRight = (rotateSpeedRight < rotateSpeedMax) ? rotateSpeedRight + rotateAcceleration : rotateSpeedMax; } else { rotateSpeedRight = (rotateSpeedRight > 0) ? rotateSpeedRight - rotateDeceleration : 0;
        }
        transform.Rotate(0f, 0f, -1*rotateSpeedRight * Time.deltaTime);

        aimLeft = (Input.GetKeyDown(keyAimLeft)) ? true : aimLeft;
        aimLeft = (Input.GetKeyUp(keyAimLeft)) ? false : aimLeft;
        if (aimLeft)
        {
            rotateSpeedLeft = (rotateSpeedLeft < rotateSpeedMax) ? rotateSpeedLeft + rotateAcceleration : rotateSpeedMax; } else { rotateSpeedLeft = (rotateSpeedLeft > 0) ? rotateSpeedLeft - rotateDeceleration : 0;
        }
        transform.Rotate(0f, 0f, rotateSpeedLeft * Time.deltaTime);
    
        gunFire = (Input.GetKeyDown(keyFire)) ? true : gunFire;
        gunFire = (Input.GetKeyUp(keyFire)) ? false : gunFire;
        if(gunFire)
        {
            animator.SetBool("isFiring", true);
        }else{
            animator.SetBool("isFiring", false);
        }
        // if(gunFire && (gunCurrentTime == 0) )
        // {
        //     animator.SetBool("isFiring", true);
        //     gunCurrentTime = gunDownTime;
        // }
        // if(gunCurrentTime>0)
        // {
        //     gunCurrentTime--;
        // }
        // if(gunCurrentTime<0)
        // {
        //     gunCurrentTime = 0;
        // }
        // if(gunCurrentTime==0)
        // {
        //     animator.SetBool("isfiring", false);
        // }
    }
}

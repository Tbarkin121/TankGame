using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStateCommunication : MonoBehaviour
{
    public Animator animator;
    
    public string keyForward;
    public string keyAltUp;
    public string keyAltDown;
    public string keyRotateRight;
    public string keyRotateLeft;
    public float ForwardTravel = 4;
    public float StartingHeight = 0f;
    public float StartingX = 0f;
    public float StartingY = 0f;
    bool altUp = false;
    bool altDown = false;
    bool rotateRight = false;
    bool rotateLeft = false;
    bool goForward = false;
    private Vector3 start_pos;
    private Vector3 end_pos;
    private float start_rot;
    private float end_rot;
    private float turn_counter = 0f;
    private float turn_duration = 2f;

    public enum moveList
    {
        alt_up, alt_down, turn_right, turn_left, forward
    };
    moveList current_move;
    moveList selected_move;
    void Start()
    {
        transform.position = new Vector3(transform.position[0], transform.position[1], StartingHeight);
        start_pos = transform.position;
        end_pos = start_pos;
    }
    // Update is called once per frame
    void Update()
    {
        turn_counter += Time.deltaTime;

        rotateLeft = (Input.GetKeyDown(keyRotateLeft)) ? true : rotateLeft;
        rotateLeft = (Input.GetKeyUp(keyRotateLeft)) ? false : rotateLeft;
        if(rotateLeft)
        {
            selected_move = moveList.turn_left;
        }
        rotateRight = (Input.GetKeyDown(keyRotateRight)) ? true : rotateRight;
        rotateRight = (Input.GetKeyUp(keyRotateRight)) ? false : rotateRight;
        if(rotateRight)
        {
            selected_move = moveList.turn_right;
        }
        altUp = (Input.GetKeyDown(keyAltUp)) ? true : altUp;
        altUp = (Input.GetKeyUp(keyAltUp)) ? false : altUp;
        if(altUp)
        {
            selected_move = moveList.alt_up;
        }
        altDown = (Input.GetKeyDown(keyAltDown)) ? true : altDown;
        altDown = (Input.GetKeyUp(keyAltDown)) ? false : altDown;
        if(altDown)
        {
            selected_move = moveList.alt_down;
        }
        goForward = (Input.GetKeyDown(keyForward)) ? true : goForward;
        goForward = (Input.GetKeyUp(keyForward)) ? false : goForward;
        if(goForward)
        {
            selected_move = moveList.forward;
        }
        
        if(start_rot != end_rot){
            float rot  = Mathf.Lerp(start_rot, end_rot, turn_counter/turn_duration);
            float xpos = 0f;
            float ypos = 0f;
            if(start_rot>end_rot){
                xpos = start_pos[0] + (1 - Mathf.Cos(Mathf.Deg2Rad*(rot-start_rot)))*Mathf.Cos(Mathf.Deg2Rad*start_rot) + Mathf.Sin(Mathf.Deg2Rad*(rot-start_rot))*Mathf.Sin(Mathf.Deg2Rad*start_rot);
                ypos = start_pos[1] - Mathf.Sin(Mathf.Deg2Rad*(rot-start_rot))*Mathf.Cos(Mathf.Deg2Rad*start_rot) + (1 - Mathf.Cos(Mathf.Deg2Rad*(rot-start_rot)))*Mathf.Sin(Mathf.Deg2Rad*start_rot);
            }
            if(end_rot>start_rot){
                xpos = start_pos[0] - (1 - Mathf.Cos(Mathf.Deg2Rad*(rot-start_rot)))*Mathf.Cos(Mathf.Deg2Rad*start_rot) - Mathf.Sin(Mathf.Deg2Rad*(rot-start_rot))*Mathf.Sin(Mathf.Deg2Rad*start_rot);
                ypos = start_pos[1] + Mathf.Sin(Mathf.Deg2Rad*(rot-start_rot))*Mathf.Cos(Mathf.Deg2Rad*start_rot) - (1 - Mathf.Cos(Mathf.Deg2Rad*(rot-start_rot)))*Mathf.Sin(Mathf.Deg2Rad*start_rot);
            }
            
            
            float zpos = Mathf.Lerp(start_pos[2], end_pos[2], turn_counter/turn_duration);
            transform.eulerAngles = new Vector3(0, 0, rot); 
            transform.position = new Vector3(xpos, ypos, zpos);
        }else{
            float xpos = Mathf.Lerp(start_pos[0], end_pos[0], turn_counter/turn_duration);
            float ypos = Mathf.Lerp(start_pos[1], end_pos[1], turn_counter/turn_duration);
            float zpos = Mathf.Lerp(start_pos[2], end_pos[2], turn_counter/turn_duration);
            transform.position = new Vector3(xpos, ypos, zpos);
        }
        

    }

    public void ExecuteActions()
    {
        current_move = selected_move;
        // start_pos = transform.position;
        start_pos=end_pos;
        start_rot = transform.eulerAngles[2];
        end_pos = start_pos;
        end_rot = start_rot;
        turn_counter = 0;
        switch(current_move)
        {
            case moveList.forward:
                end_pos[0] -= ForwardTravel*Mathf.Sin(Mathf.Deg2Rad*start_rot);
                end_pos[1] += ForwardTravel*Mathf.Cos(Mathf.Deg2Rad*start_rot);
                animator.SetInteger("PitchAngle",0);
                animator.SetInteger("TurnAngle",0);
                break;
            case moveList.alt_up:
                end_pos[0] -= Mathf.Sin(Mathf.Deg2Rad*start_rot);
                end_pos[1] += Mathf.Cos(Mathf.Deg2Rad*start_rot);
                end_pos[2] += 1;
                animator.SetInteger("PitchAngle",-2);
                animator.SetInteger("TurnAngle",0);
                break;
            case moveList.alt_down:
                end_pos[0] -= Mathf.Sin(Mathf.Deg2Rad*start_rot);
                end_pos[1] += Mathf.Cos(Mathf.Deg2Rad*start_rot);
                end_pos[2] -= 1;
                animator.SetInteger("PitchAngle",2);
                animator.SetInteger("TurnAngle",0);
                break;
            case moveList.turn_left:
                end_rot -= 90f;
                end_pos[0] += Mathf.Cos(Mathf.Deg2Rad*start_rot)-Mathf.Sin(Mathf.Deg2Rad*start_rot);
                end_pos[1] += Mathf.Cos(Mathf.Deg2Rad*start_rot)+Mathf.Sin(Mathf.Deg2Rad*start_rot);
                animator.SetInteger("PitchAngle",0);
                animator.SetInteger("TurnAngle",-1);
                break;
            case moveList.turn_right:
                end_rot += 90f;
                end_pos[0] -= Mathf.Cos(Mathf.Deg2Rad*start_rot)+Mathf.Sin(Mathf.Deg2Rad*start_rot);
                end_pos[1] += Mathf.Cos(Mathf.Deg2Rad*start_rot)-Mathf.Sin(Mathf.Deg2Rad*start_rot);
                animator.SetInteger("PitchAngle",0);
                animator.SetInteger("TurnAngle",1);
                break;
        }
    }
}
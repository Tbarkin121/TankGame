/*  .-------.     .--.                   
    |_     _|--.--|  |.----..---.
      |   | |  |  |  |  ^__|   _|
      |___| |__   |__|_____|__| 
             __|  |     
            |_____|
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTimer : MonoBehaviour
{
    public float TurnDuration = 5f; //Seconds
    public float totalSegments = 8.0f;
    public GameObject ProgressBar;
    public GameObject CurrentTurnIndicator;
    private bool timeReset = true;
    public UnityEngine.Events.UnityEvent unitgroup_light_trigger;
    public UnityEngine.Events.UnityEvent unitgroup_medium_trigger;
    public UnityEngine.Events.UnityEvent unitgroup_heavy_trigger;
    public UnityEngine.Events.UnityEvent unitgroup_super_trigger;
    

    public void TransformPanel()
    {
        if(ProgressBar != null)
        {
            RectTransform panelRectTransform = ProgressBar.GetComponent<RectTransform>();
            panelRectTransform.sizeDelta = new Vector2(1000, 25);
        }
    }

    public void FadePanel()
    {
        RectTransform panelRectTransform = ProgressBar.GetComponent<RectTransform>();
        RectTransform turnRectTransform = CurrentTurnIndicator.GetComponent<RectTransform>();
        panelRectTransform.sizeDelta = new Vector2(0, 25);
        // Toggle the end value depending on the faded state
        StartCoroutine(TurnCoordinator(panelRectTransform, turnRectTransform, panelRectTransform.sizeDelta[0], 1600));

   }
    public IEnumerator TurnCoordinator(RectTransform bar, RectTransform group, float start, float end)
    {
        float counter = 0f;
        float current_group = 0f;
        float previous_group = -1f;
        while(counter<TurnDuration)
        {
            counter += Time.deltaTime;
            current_group = Mathf.Floor(Mathf.Lerp(start, end, counter / TurnDuration)/(end/totalSegments));

            bar.sizeDelta = new Vector2(Mathf.Lerp(start, end, counter / TurnDuration), 25); 
            // group.sizeDelta = new Vector2((current_group+1)*(end/totalSegments), 100);
            group.sizeDelta = new Vector2(1*(end/totalSegments), 100);
            group.anchoredPosition = new Vector2((current_group)*(end/totalSegments),0);
            
            if(previous_group != current_group){
                previous_group = current_group;

                switch(current_group)
                {
                    case 0:
                        unitgroup_light_trigger.Invoke();
                        unitgroup_medium_trigger.Invoke();
                        unitgroup_heavy_trigger.Invoke();
                        unitgroup_super_trigger.Invoke();
                        break;
                    case 1:
                        unitgroup_light_trigger.Invoke();
                        break;
                    case 2:
                        unitgroup_light_trigger.Invoke();
                        unitgroup_medium_trigger.Invoke();
                        break;
                    case 3:
                        unitgroup_light_trigger.Invoke();
                        break;
                    case 4:
                        unitgroup_light_trigger.Invoke();
                        unitgroup_medium_trigger.Invoke();
                        unitgroup_heavy_trigger.Invoke();
                        break;
                    case 5:
                        unitgroup_light_trigger.Invoke();
                        break;
                    case 6:
                        unitgroup_light_trigger.Invoke();
                        unitgroup_medium_trigger.Invoke();
                        break;
                    case 7:
                        unitgroup_light_trigger.Invoke();
                        break;
                
                }
            }
            
            
            
            yield return null;
        }
        timeReset = true;
    }


    // Update is called once per frame
    void Update()
    {
        if(timeReset)
        {
            timeReset=false;
            RectTransform panelRectTransform = ProgressBar.GetComponent<RectTransform>();
            RectTransform turnRectTransform = CurrentTurnIndicator.GetComponent<RectTransform>();
            panelRectTransform.sizeDelta = new Vector2(0, 25);
            // Toggle the end value depending on the faded state
            StartCoroutine(TurnCoordinator(panelRectTransform, turnRectTransform, panelRectTransform.sizeDelta[0], 1600));
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            
            //unitgroup_light_trigger.add //Adds listeners
        }
        
    }
}



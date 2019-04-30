using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TurnScript : MonoBehaviour
{
    public SteamVR_Action_Vector2 touchpadxy;
    public SteamVR_Action_Boolean trigger;

    private SteamVR_Behaviour_Pose m_Pose = null;
    // Start is called before the first frame update
    void Start()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction;
        if (trigger.GetStateUp(m_Pose.inputSource))
        {
            direction = touchpadxy.GetAxis(m_Pose.inputSource);
            Debug.Log(direction);
        }
         
    }
}

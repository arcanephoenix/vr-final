using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TeleportScript : MonoBehaviour
{
    public GameObject m_Pointer;
    public GameObject m_Pointer2;
    public SteamVR_Action_Boolean m_TeleportAction;
    public SteamVR_Action_Boolean m_Interact;
    public Material matTouched;
    public Material matInteractable;


    private SteamVR_Behaviour_Pose m_Pose = null;
    //private SteamVR_Input_Sources inputSourceR = SteamVR_Input_Sources.RightHand;
    private bool hasPosition = false;
    private bool isTeleporting = false;
    private float fadeTime = 0.5f;

    private void Start()
    {
        Debug.Log("Pose is set");
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        Debug.Log(m_Pose.ToString());
        //m_TeleportAction = SteamVR_Input.GetBooleanAction("GrabPinch");
    }

    // Update is called once per frame
    private void Update()
    {
        // Pointer
        hasPosition = UpdatePointer();
        m_Pointer.SetActive(hasPosition);

        // Teleport
        if(m_TeleportAction.GetStateUp(m_Pose.inputSource))
        {
            Debug.Log("button is pressed");
            TryTeleport();
        }

    }

    private void TryTeleport()
    {
        // check if bools are false 
        if (!hasPosition || isTeleporting) return;

        // get camerarig and headposition
        Transform cameraRig = SteamVR_Render.Top().origin;
        Vector3 headPosition = SteamVR_Render.Top().head.position;

        // translation
        Vector3 groundPosition = new Vector3(headPosition.x, cameraRig.position.y, headPosition.z);
        Vector3 translation = m_Pointer.transform.position - groundPosition;

        // movement
        StartCoroutine(MoveRig(cameraRig, translation));
    }

    private IEnumerator MoveRig(Transform cameraRig, Vector3 translation)
    {
        // flag
        isTeleporting = true;
        // start fade
        SteamVR_Fade.Start(Color.black, fadeTime, true);
        // translate
        yield return new WaitForSeconds(fadeTime); // wait for the fade
        cameraRig.position += translation;

        // unfade
        SteamVR_Fade.Start(Color.clear, fadeTime, true);

        // unflag
        isTeleporting = false;
    }

    private bool UpdatePointer()
    {
        // Ray from controller
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Floor");
        LayerMask mask2 = LayerMask.GetMask("Interactable");
        

        //if hit ground
        if(Physics.Raycast(ray, out hit ,mask))
        {

            m_Pointer.transform.position = new Vector3(hit.point.x, 0f,hit.point.z);
            //m_Pointer.transform.position = hit.point;
            // if hit object
            if (Physics.Raycast(ray, out hit))
            {
                //m_Pointer2.transform.position = hit.point;
                if(hit.collider.gameObject.tag == "interactable")
                {
                    hit.collider.gameObject.GetComponent<MeshRenderer>().material = matTouched;
                    if (m_Interact.GetStateUp(m_Pose.inputSource)) {
                        hit.collider.gameObject.GetComponent<AudioSource>().Play();
                    }
                }
                //else
                //{
                //    hit.collider.gameObject.GetComponent<MeshRenderer>().material = matInteractable;
                //}
            }
                return true;
        }

        //if not hit
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTemple : MonoBehaviour
{
    // TODO: change from triggers to position check
    GameObject[] inside;
    GameObject[] outside;
    GameObject[] outside1;
    GameObject[] cars;
    private void Start()
    {
        inside = GameObject.FindGameObjectsWithTag("intemple");
        outside = GameObject.FindGameObjectsWithTag("outside");
        outside1 = GameObject.FindGameObjectsWithTag("instation");
        cars = GameObject.FindGameObjectsWithTag("traffic");
        foreach (GameObject obj in inside)
        {
            obj.GetComponent<AudioLowPassFilter>().cutoffFrequency = 1000;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //lpf = other.gameObject.GetComponent<AudioLowPassFilter>();
        foreach (GameObject gameObject in inside)
        {
            gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = 5000;
        }
        foreach (GameObject gameObject in cars)
        {
            gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = 1000;
        }
        foreach (GameObject obj in outside)
        {
            obj.GetComponent<AudioLowPassFilter>().cutoffFrequency = 1000;
        }
        foreach (GameObject obj in outside1)
        {
            obj.GetComponent<AudioLowPassFilter>().cutoffFrequency = 1000;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject gameObject in outside)
        {
            gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = 5000;
        }
        foreach (GameObject gameObject in cars)
        {
            gameObject.GetComponent<AudioLowPassFilter>().cutoffFrequency = 5000;
        }
        foreach (GameObject obj in outside1)
        {
            obj.GetComponent<AudioLowPassFilter>().cutoffFrequency = 1000;
        }
        foreach (GameObject obj in inside)
        {
            obj.GetComponent<AudioLowPassFilter>().cutoffFrequency = 1000;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FireFmodAudioEvent : MonoBehaviour
{
    public void FireAudioEvent(string eventPath)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventPath);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AudioClipSO",menuName = "Scriptable Object / AudioClipRef")]
public class AudioClipRefSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] delivery_fail;
    public AudioClip[] delivery_success;
    public AudioClip[] footstep01;
    public AudioClip[] object_drop;
    public AudioClip[] object_pickup;
    public AudioClip[] pan_sizzle_loop;
    public AudioClip[] trash;
    public AudioClip[] warning;

}

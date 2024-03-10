using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefSO audioClipRefSO;
    private void Start()
    {
        TrashCounter.Instance.OnTrashKitchentObject += Instance_OnTrashKitchentObject;
        CuttingCounter.OnAnyCut += Instance_OnCutting;
        DeliveryManager.Instance.OnDevilerySuccess += Instance_OnDevilerySuccess;
        DeliveryManager.Instance.OnDevileryFail += Instance_OnDevileryFail;
    }

    private void Instance_OnDevileryFail(object sender, System.EventArgs e)
    {
        DeliveryManager cuttingCounter = sender as DeliveryManager;
        Playsound(audioClipRefSO.delivery_fail[1], cuttingCounter.transform.position);
    }

    private void Instance_OnDevilerySuccess(object sender, DeliveryManager.OnDevilerySuccessEventArgs e)
    {
        DeliveryManager cuttingCounter = sender as DeliveryManager;
        Playsound(audioClipRefSO.delivery_success[0], cuttingCounter.transform.position);
    }

    private void Instance_OnCutting(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        Playsound(audioClipRefSO.chop[0], cuttingCounter.transform.position);
    }

    private void Instance_OnTrashKitchentObject(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        Playsound(audioClipRefSO.trash[0], trashCounter.transform.position);
    }

    private void Playsound(AudioClip audioClip,Vector3 position,float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}

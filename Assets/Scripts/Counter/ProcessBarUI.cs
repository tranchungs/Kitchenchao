using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessBarUI : MonoBehaviour
{
    [SerializeField] private Image BarImage;
    private IHasProgress hasProgress;
    [SerializeField] private GameObject gameObjectContainer;
    private void Start()
    {
        hasProgress = gameObjectContainer.GetComponent<IHasProgress>();
        hasProgress.OnProcessChanged += HasProgress_OnProcessChanged;
        BarImage.fillAmount = 0f;
        Hide();
    }


    private void HasProgress_OnProcessChanged(object sender, IHasProgress.OnProcessChangedEventArg e)
    {
        BarImage.fillAmount = e.ProcessNomalized;
        if (e.ProcessNomalized == 0 || e.ProcessNomalized == 1f)
        {
            BarImage.fillAmount = 0f;
            Hide();
        }
        else
        {
            Show();
        }
    }

   
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}

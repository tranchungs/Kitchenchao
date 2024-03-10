using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisualAnimator : MonoBehaviour
{
    private const string CUT = "Cut";
    [SerializeField] private Animator animator;
    [SerializeField] private CuttingCounter cuttingCounter;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();
        cuttingCounter.OnCutting += CuttingCounter_OnCutting;
       
    }

    private void CuttingCounter_OnCutting(object sender, System.EventArgs e)
    {
        animator.SetTrigger(CUT);
    }

    

   
}

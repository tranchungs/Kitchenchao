using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisualAnimator : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] private ContainterCounter containterCounter;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        containterCounter.OnSelectedBaseCounter += ContainterCounter_OnSelectedBaseCounter;
    }

    private void ContainterCounter_OnSelectedBaseCounter(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }

    
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress 
{
    public event EventHandler<OnProcessChangedEventArg> OnProcessChanged;
    public class OnProcessChangedEventArg : EventArgs
    {
        public float ProcessNomalized;
    }

}

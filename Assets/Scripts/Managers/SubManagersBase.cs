using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SubManagersBase : SubManagersInterface
{
    public virtual void Initialize(AppManager appManager)
    {
        
    }
}

public interface SubManagersInterface 
{
    void Initialize(AppManager appManager);
}

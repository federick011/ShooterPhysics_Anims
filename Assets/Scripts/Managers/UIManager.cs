using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UIManager : SubManagersBase
{
    public GameObject canvasParent;
    public Canvas UIcanvas;

    public override void Initialize(AppManager appManager)
    {
        base.Initialize(appManager);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdButton : ScreenButton
{
    public override void Execute()
    {
        transform.parent.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleButton : ScreenButton
{
    private Toggle toggle;
    private void Start()
    {
        toggle=GetComponent<Toggle>();
    }
    public override void Execute()
    {
        toggle.isOn=!toggle.isOn;
    }
}

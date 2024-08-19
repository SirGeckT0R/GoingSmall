using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SeatsPageScreenButton : ScreenButton
{
    public delegate void SeatsButtonPressed(E_Screen screen);
    public static event SeatsButtonPressed OnSeatsButtonPressed;
    [SerializeField] private List<Toggle> toggles = new List<Toggle> ();
    public override void Execute()
    {
        //toggles = transform.parent.GetComponentsInChildren(typeof(Toggle)).OfType<Toggle>().ToList();
        foreach (Toggle toggle in toggles)
        {
            if (!toggle.isOn)
            {
                return;
            }
        }
        OnSeatsButtonPressed(ScreenToSwitch);

    }
}

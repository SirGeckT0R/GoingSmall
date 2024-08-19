using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndButton : ScreenButton
{
    public delegate void GameEnd(E_Screen screen);
    public static event GameEnd OnGameEnd;
    public override void Execute()
    {
        if (ShouldBeBlocked && !CanExecute)
        {
            return;
        }

        OnGameEnd(ScreenToSwitch);
    }
}

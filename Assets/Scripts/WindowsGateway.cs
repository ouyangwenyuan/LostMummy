using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;

/// <summary>
/// Windows specific and interop between Unity and Windows Store or Windows Phone 8
/// </summary>
public static class WindowsGateway
{
    public static Action<string, string, string> OnShareFacebook = delegate { };

    public static Action OnActionRaised = delegate { };
    public static Action OnExitGame = delegate { };
    public static Action OnGameOver = delegate { };
    public static Action OnBackToMenu = delegate { };
    public static Action OnRateGame = delegate { };


    static WindowsGateway()
    {
        //OnActionWithParametersRaised = delegate { };
        OnActionRaised = delegate { };
        OnExitGame = delegate { };
        OnGameOver = delegate { };
        OnBackToMenu = delegate { };
        OnRateGame = delegate { };
    }
}
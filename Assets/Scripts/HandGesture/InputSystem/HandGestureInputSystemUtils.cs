
using System.Collections.Generic;
using UnityEngine.UIElements;

public class HandGestureInputSystemUtils
{
    public static List<string> GetActionsFromScheme(HandGestureInputScheme scheme)
    {
        return scheme.GetActions();
    }
}

using System.Collections.Generic;
using UnityEngine.UIElements;

public class HandGestureInputSystemUtils
{
    public static List<HandGestureAction> GetActionsFromScheme(HandGestureInputScheme scheme)
    {
        return scheme.GetActions();
    }

    public static string HandGestureActionToString(HandGestureAction actionType)
    {
        switch(actionType)
        {
            case HandGestureAction.PICK: return "PICK";
            case HandGestureAction.RELEASE: return "RELEASE";
            case HandGestureAction.LIKE: return "LIKE";
        }
        return "";
    }
    public static HandGestureAction StringToHandGestureAction(string actionName)
    {
        switch (actionName)
        {
            case "PICK": return HandGestureAction.PICK;
            case "RELEASE": return HandGestureAction.RELEASE;
            case "LIKE": return HandGestureAction.LIKE;
        }
        return HandGestureAction.UNKNOWN;
    }

    public static List<HandGestureAction> StringsToHandGestureActions(List<string> actionNames) 
    {
        List<HandGestureAction> actions = new List<HandGestureAction>(actionNames.Count);
        for (int i = 0; i < actionNames.Count; i++)
        {
            actions[i] = HandGestureInputSystemUtils.StringToHandGestureAction(actionNames[i]);
        }
        return actions;
    }
}
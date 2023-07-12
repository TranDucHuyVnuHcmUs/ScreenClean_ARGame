using System;

public class GameStateUtils
{
    private static Type[] gameStateDerivedTypes =
    {
        typeof(GameCountdownState),
        typeof(GamePlayState),
        typeof(GameHandGestureActionState),
        typeof(GameLastLevelPlayState),
        typeof(GameWinState)
    };

    public static Type FindDerivedType(GameState gameState)
    {
        foreach (var type in gameStateDerivedTypes)
        {
            try {
                var output = Convert.ChangeType(gameState, type);
                return type;
            } catch
            {
                continue;
            }
        }
        return null;
    }
}
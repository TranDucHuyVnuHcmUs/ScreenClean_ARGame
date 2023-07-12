
using UnityEngine;

public interface GameStateRenderer
{
    public void RenderState(GameState state);
    public void UnrenderState(GameState state);
}

public abstract class GameStateRenderer<T> : MonoBehaviour, GameStateRenderer
    where T : GameState
{
    public abstract void RenderState(T state);

    public virtual void RenderState(GameState state)
    {
        RenderState((T)state);
    }

    public abstract void UnrenderState(T state);

    public virtual void UnrenderState(GameState state)
    {
        UnrenderState((T)state);
    }
}
using Mediapipe.Unity;

public class HandGestureInputSystem_MagnetHand_Listener :
    EventAdapter<HandGestureInputSystem, MagnetHand>
{
    public override void Listen()
    {
        HandGestureInputSystem.ListenToActionsRecognizedEvent(this.listener.ActivateBoxAnnotations);
    }
}
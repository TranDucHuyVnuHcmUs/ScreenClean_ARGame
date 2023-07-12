using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component serve as a 'flag' to mark an object as interactable (draggable) by magnet cube.
/// </summary>
public class Magnetic : MonoBehaviour
{
    public MagnetCube draggingMagnet;

    private void OnDestroy()
    {
        draggingMagnet?.DestroyDraggingObject();
    }
}

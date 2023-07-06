using UnityEngine;

[CreateAssetMenu(fileName = "GameMultiHandLandmarkListAnnotationConfig", menuName = "Game/Configs/Multi hand landmark list annotation")]
public class GameMultiHandLandmarkListAnnotationConfig : ScriptableObject
{
    public Color _leftLandmarkColor = Color.green;
    public Color _rightLandmarkColor = Color.green;

    public Color _cleanLeftHandColor = Color.blue;
    public Color _cleanRightHandColor = Color.cyan;

    public float _landmarkRadius = 15.0f;
    public Color _connectionColor = Color.white;
    public float _connectionWidth = 1.0f;
}

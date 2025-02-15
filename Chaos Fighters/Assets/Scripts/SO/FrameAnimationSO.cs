using UnityEngine;

[CreateAssetMenu(menuName = "Game/Frame Animation", fileName = "New Frame Animation")]
public class FrameAnimationSO : ScriptableObject
{
    [SerializeField] Sprite[] frames;
    [SerializeField] int framerate;

    public Sprite[] Frames => frames;
    public int Framerate => framerate;
}
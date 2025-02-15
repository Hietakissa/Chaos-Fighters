using UnityEngine;

[CreateAssetMenu(menuName = "Game/Movement Settings", fileName = "New Movement Settings")]
public class MovementSettingsSO : ScriptableObject
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float maxSpeed;

    [Header("Physics")]
    [SerializeField] float jumpForce;
    [SerializeField] float gravityForce;
    [SerializeField] float drag;

    public float MoveSpeed => moveSpeed;
    public float MaxSpeed => maxSpeed;
    public float JumpForce => jumpForce;
    public float GravityForce => gravityForce;
    public float Drag => drag;
}
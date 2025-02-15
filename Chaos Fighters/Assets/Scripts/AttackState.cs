using System;
using UnityEngine;

public class AttackState : State
{
    public override PlayerState[] ValidExitStates => new PlayerState[]{ PlayerState.Idling, PlayerState.Moving, PlayerState.Jumping };
    public override Predicate<PlayerController> EnterPredicate => (player =>
    {
        return Input.GetKeyDown(player.GetKeyCodeForKey(Key.Attack)) && player.IsGrounded;
    });
    public override bool CanExit => !attacking;

    bool attacking;

    public override void EnterState()
    {
        base.EnterState();


        attacking = true;
        player.RB.linearVelocityX = 0f;
    }

    public override void UpdateState()
    {
        base.UpdateState();


        if (animationIndex > maxAnimationIndex)
        {
            attacking = false;

            Vector2 hitPos = (Vector2)player.transform.position + player.NormalAttackHitbox.offset * player.FacingDirectionVector;
            DebugTextManager.Instance.SetVariableFor("HitPos", hitPos.ToString(), 3f, player);
            Debug.DrawRay(hitPos, Vector3.up, Color.red, 3f);
            Collider2D[] colls = Physics2D.OverlapBoxAll(hitPos, player.NormalAttackHitbox.size, 0f);
            DebugTextManager.Instance.SetVariableFor("HitCount", colls.Length.ToString(), 3f, player);
            for (int i = 0; i < colls.Length; i++)
            {
                DebugTextManager.Instance.SetVariableFor($"Hit[{i}]", colls[i].gameObject.name, 3f, player);

                if (colls[i].gameObject == player.Opponent.gameObject)
                {
                    player.Opponent.TakeDamage(GameManager.Instance.CombatSettings.BasicDamage);
                    //player.Opponent.RB.AddForce((player.Opponent.transform.position - player.transform.position).normalized * Vector2.right * 50f, ForceMode2D.Impulse);
                    player.Opponent.ForceToAdd += (player.Opponent.transform.position - player.transform.position).normalized * Vector2.right * 500f;
                }
            }
        }
        else SetAnimationFrame();
    }

    public override void FixedUpdateState()
    {
        player.HandleMovement(0);
    }
}
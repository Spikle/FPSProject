using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private float cooldown = 1f;
    public AttackState(StateType type, EnemyController controller) : base(type, controller)
    {

    }

    public override void Enter()
    {
        cooldown = 1f;
    }

    public override void Update()
    {
        if (controller.Target == null)
        {
            controller.ChangeState(StateType.Move);
            return;
        }

        if ((controller.Target.position - controller.transform.position).magnitude > 1.75f)
            controller.ChangeState(StateType.Move);

        Attack();
    }

    private void Attack()
    {
        cooldown += Time.deltaTime;
        if (cooldown > 1f)
        {
            cooldown = 0f;
            controller.Target.GetComponent<HealthController>().OnDamage(controller.Damage);
        }
    }

    public override void Exit()
    {

    }
}

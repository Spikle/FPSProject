using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveState : State
{
    private NavMeshPath path;
    private int currentPoint = 0;
    private float elapsed = 0f;

    public MoveState(StateType type, EnemyController controller) : base(type, controller)
    {

    }

    public override void Enter()
    {
        path = new NavMeshPath();
        elapsed = 1f;
    }

    public override void Update()
    {
        if (controller.Target == null)
            return;

        UpdatePath();
        Move();
    }

    private void UpdatePath()
    {
        elapsed += Time.deltaTime;
        if (elapsed > 1f)
        {
            elapsed = 0f;
            NavMesh.CalculatePath(controller.transform.position, controller.Target.position, NavMesh.AllAreas, path);
            currentPoint = 0;
        }
    }

    private void Move()
    {
        if (path.corners.Length == 0 || currentPoint >= path.corners.Length)
            return;

        Vector3 point = path.corners[currentPoint];
        Vector3 direction = point - controller.transform.position;
        direction.y = 0;

        controller.CharacterController.Move(direction.normalized * controller.Speed * Time.deltaTime);

        if (direction.magnitude <= 0.1f && currentPoint < path.corners.Length - 1)
            currentPoint++;

        if ((controller.Target.position - controller.transform.position).magnitude < 1.5f)
            controller.ChangeState(StateType.Attack);
    }


    public override void Exit()
    {

    }
}

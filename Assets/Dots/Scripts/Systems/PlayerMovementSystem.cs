using Unity.Burst;
using Unity.Entities;
using static UnityEngine.GraphicsBuffer;
using Unity.Transforms;
using Unity.Collections;
using Unity.Physics;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.InputSystem;
using Unity.Mathematics;

partial class PlayerMovementSystem : SystemBase
{


    InputSystem_Actions playerInput;

    protected override void OnCreate()
    {
        playerInput = new InputSystem_Actions();
        playerInput.Enable();
    }

    protected override void OnUpdate()
    {
        var moveInput =
            playerInput.Player.Move.ReadValue<Vector2>();

        foreach (
            (var localTransform,
            var playerMovement)
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<PlayerMovementComponent>>())
        {
            localTransform.ValueRW.Position += new float3(moveInput.x, 0, moveInput.y) * playerMovement.ValueRO.moveSpeed * SystemAPI.Time.DeltaTime;
            Debug.Log(localTransform.ValueRW.Position);
        }

    }

    protected override void OnDestroy()
    {
        playerInput.Disable();
    }
}


public struct PlayerInput : IComponentData
{
    public float2 Move;
}

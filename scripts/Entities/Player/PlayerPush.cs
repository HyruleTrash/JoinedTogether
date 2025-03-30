using Godot;
using System;
using System.Collections.Generic;

/// <summary>
/// Handles the pushing of certain objects
/// </summary>
public partial class PlayerPush : PlayerComponent
{
    [Export]
    public float PushForce = 20;

    protected override void _ProcessComponent(double delta)
    {
        // Collision logic
        for (int i = 0; i < this._playerBody.GetSlideCollisionCount(); i++)
        {
            float playerPushAxis = Input.GetAxis("LEFT", "RIGHT");
            KinematicCollision2D collision = this._playerBody.GetSlideCollision(i);
            if (
                collision.GetCollider() is Box box && // Check if the collider is a box
                collision.GetNormal().Dot(this.UP) < 0.1f && // Check if the collision is not from the top
                playerPushAxis != 0 &&// Check if the player is pushing
                box.Position.Y < this._playerBody.GlobalPosition.Y // Check if the box is below the player
            )
            {
                Vector2 pushDirection = Vector2.Right * playerPushAxis - new Vector2(0, -0.75f);
                box.ApplyCentralImpulse(pushDirection.Normalized() * this.PushForce);
            }
        }
    }
}

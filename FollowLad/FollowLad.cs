using ExileCore2;
using ExileCore2.PoEMemory.Components;
using ExileCore2.PoEMemory.MemoryObjects;
using ExileCore2.Shared.Interfaces;
using ExileCore2.Shared.Nodes;
using System;
using System.Drawing; // For Point
using System.Linq;
using System.Numerics;

namespace FollowLad
{
    public class FollowLad : BaseSettingsPlugin<FollowLadSettings>
    {
        private Entity _followTarget;
        private DateTime _nextAllowedActionTime = DateTime.Now; // Cooldown timer

        public override bool Initialise()
        {
            // Initialize plugin
            Name = "FollowLad";
            return base.Initialise();
        }

        public override void Render()
        {
            // Handle pause/unpause toggle
            if (Settings.TogglePauseHotkey.PressedOnce())
            {
                Settings.IsPaused.Value = !Settings.IsPaused.Value;
            }

            // If paused, disabled, or not ready for the next action, do nothing
            if (!Settings.Enable.Value || Settings.IsPaused.Value || !GameController.Player.IsAlive || DateTime.Now < _nextAllowedActionTime)
                return;

            try
            {
                FollowTarget();
            }
            catch (Exception)
            {
                // Handle exceptions silently
            }
        }

        private void FollowTarget()
        {
            try
            {
                _followTarget = GetFollowingTarget();
                if (_followTarget == null)
                {
                    return;
                }

                var targetPos = _followTarget.Pos;
                var myPos = GameController.Player.Pos;

                // Check distance to target
                var distanceToTarget = Vector3.Distance(myPos, targetPos);

                // If within the follow distance, skip actions
                if (distanceToTarget <= Settings.FollowDistance.Value)
                {
                    // Optional: Add a delay or log to indicate "idle" state
                    return;
                }

                // If outside the follow distance, move toward the target
                MoveToward(targetPos);

                // Set the cooldown for the next allowed action
                _nextAllowedActionTime = DateTime.Now.AddMilliseconds(Settings.ActionCooldown.Value);
            }
            catch (Exception)
            {
                // Handle exceptions silently
            }
        }


        private Entity GetFollowingTarget()
        {
            try
            {
                var leaderName = Settings.TargetPlayerName.Value.ToLower();
                return GameController.Entities
                    .Where(e => e.Type == ExileCore2.Shared.Enums.EntityType.Player)
                    .FirstOrDefault(e => e.GetComponent<Player>().PlayerName.ToLower() == leaderName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void MoveToward(Vector3 targetPos)
        {
            try
            {
                var screenPos = GameController.IngameState.Camera.WorldToScreen(targetPos);
                var screenPoint = new Point((int)screenPos.X, (int)screenPos.Y);

                Mouse.SetCursorPosition(screenPoint);
                Mouse.LeftClick(screenPoint);
            }
            catch (Exception)
            {
                // Handle exceptions silently
            }
        }
    }
}

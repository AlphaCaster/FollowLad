using ExileCore2.Shared.Interfaces;
using ExileCore2.Shared.Nodes;
using System.Windows.Forms;

namespace FollowLad
{
    public class FollowLadSettings : ISettings
    {
        public ToggleNode Enable { get; set; } = new ToggleNode(true);
        public TextNode TargetPlayerName { get; set; } = new TextNode("Leader");
        public RangeNode<int> FollowDistance { get; set; } = new RangeNode<int>(50, 10, 200);
        public HotkeyNode MovementKey { get; set; } = new HotkeyNode(Keys.T); // Default to no key set
        public HotkeyNode TogglePauseHotkey { get; set; } = new HotkeyNode(Keys.P); // Default to P
        public ToggleNode IsPaused { get; set; } = new ToggleNode(false); // Default to not paused
        public RangeNode<int> ActionCooldown { get; set; } = new RangeNode<int>(500, 550, 20000); // Cooldown in milliseconds

        public FollowLadSettings()
        {
            Enable = new ToggleNode(true);
        }
    }
}

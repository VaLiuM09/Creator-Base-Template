using VPG.Creator.Core.Behaviors;
using VPG.Creator.BaseTemplate.Behaviors;
using VPG.CreatorEditor.UI.StepInspector.Menu;

namespace VPG.CreatorEditor.BaseTemplate.UI.Behaviors
{
    public class ConfettiMenuItem : MenuItem<IBehavior>
    {
        /// <inheritdoc />
        public override string DisplayedName { get; } = "VPG/Spawn Confetti";

        /// <inheritdoc />
        public override IBehavior GetNewItem()
        {
            return new ConfettiBehavior();
        }
    }
}

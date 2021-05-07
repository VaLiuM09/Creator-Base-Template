using System.Collections.Generic;
using VPG.Creator.Core.Behaviors;
using VPG.Creator.Core.Configuration;
using VPG.Creator.Core.Configuration.Modes;

namespace VPG.Creator.BaseTemplate.Configuration
{
    public class NoHintsRuntimeConfiguration : DefaultRuntimeConfiguration
    {
        protected NoHintsRuntimeConfiguration()
        {
            IMode noHints = new Mode("No Audio Hints", new WhitelistTypeRule<IOptional>().Add<PlayAudioBehavior>());
            Modes = new BaseModeHandler(new List<IMode> { DefaultMode, noHints });
        }
    }
}
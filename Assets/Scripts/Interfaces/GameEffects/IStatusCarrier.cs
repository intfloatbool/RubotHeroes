using System.Collections.Generic;

namespace Interfaces.GameEffects
{
    public interface IStatusCarrier
    {
        List<StatusInfo> Effects { get; }
        void InitializeStatusEffects(List<StatusInfo> effects);
        void OnCarry(IStatusable statusable);
    }
}

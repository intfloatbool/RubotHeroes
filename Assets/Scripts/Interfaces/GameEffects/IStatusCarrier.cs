using System.Collections.Generic;

namespace Interfaces.GameEffects
{
    public interface IStatusCarrier
    {
        void InitializeStatusEffects(List<StatusInfo> effects);
        void OnCarry(IStatusable statusable);
    }
}

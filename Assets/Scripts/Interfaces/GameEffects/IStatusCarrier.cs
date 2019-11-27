using System.Collections.Generic;

namespace Interfaces.GameEffects
{
    public interface IStatusCarrier
    {
        void InitializeStatusEffects(List<StatusItem.StatusInfo> effects);
        void OnCarry(IStatusable statusable);
    }
}

using Interfaces.GameEffects;

namespace GameStatus.Static
{
    public static class UnitStatusHelper
    {
        public static void SendStatusToTarget(this IStatusCarrier carrier,
            IStatusable statusable)
        {
            carrier.Effects.ForEach(e => statusable.OnStatusEffect(e));
        }
    }
}




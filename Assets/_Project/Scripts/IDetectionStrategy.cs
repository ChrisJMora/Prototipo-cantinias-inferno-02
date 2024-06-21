using UnityEngine;
using Utilities;

namespace CantuniasInferno
{
    public interface IDetectionStrategy
    {
        bool Execute(Transform player, Transform detector, CountdownTimer timer);
    }
}

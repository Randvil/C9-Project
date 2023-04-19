
public class StunEffect : IStunEffect
{
    public float EndEffectTime { get; }

    public StunEffect(float endEffectTime)
    {
        EndEffectTime = endEffectTime;
    }
}

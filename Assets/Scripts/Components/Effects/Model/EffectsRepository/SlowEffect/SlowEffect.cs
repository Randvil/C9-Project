public class SlowEffect : ISlowEffect
{
    public float MovementSlow { get; }
    public float EndEffectTime { get; }

    public SlowEffect(float movementSlow, float endEffectTime)
    {
        MovementSlow = movementSlow;
        EndEffectTime = endEffectTime;
    }
}

public interface IDoTEffect : IEffect
{
    public void DealDamage();
    public Damage Damage { get; }
    public float DamagePeriod { get; }
    public float LastTickTime { get; set; }
}

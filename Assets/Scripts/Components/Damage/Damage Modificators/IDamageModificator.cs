using System;

public interface IDamageModificator : IComparable<IDamageModificator>
{
    public eDamageModificator Order { get; }
    public Damage ApplyModificator(Damage uneffectedDamage);
}

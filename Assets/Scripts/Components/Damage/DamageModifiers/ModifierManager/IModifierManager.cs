public interface IModifierManager
{
    public void AddModifier(IDamageModifier modifier);
    public void RemoveModifier(IDamageModifier modifier);
    public float ApplyModifiers(float damage);
}

public interface IForbiddableDeath
{
    public bool IsForbidden { get; }
    public void ForbidDying(object forbiddingObject);
    public void AllowDying(object forbiddingObject);
}

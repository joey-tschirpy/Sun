public abstract class InputModule : IInputable
{
    protected Laser combinedLaser;

    public ModuleType ModuleType { get; }

    public void ReceiveLaser(Laser laser)
    {

    }

    public void RemoveLaser(Laser laser)
    {

    }
}
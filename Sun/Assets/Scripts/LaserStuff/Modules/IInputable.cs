public interface IInputable : IModulable
{
    void ReceiveLaser(Laser laser);
    void RemoveLaser(Laser laser);
}

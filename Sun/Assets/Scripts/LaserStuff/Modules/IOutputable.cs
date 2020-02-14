public interface IOutputable : IModulable
{
    void SendLaser(Laser laser, LaserObject laserObject, Face face);
}
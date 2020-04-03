using System.Collections.Generic;

public enum Face
{
    Front,
    Back,
    Left,
    Right,
    Up,
    Down
}

public enum ModuleType
{
    Blank,
    ManipulationInput,
    PowerInput,
    Output,
    Information
}

public abstract class Module
{
    protected readonly int LasersPerFaceCount = System.Enum.GetValues(typeof(Direction)).Length;

    public abstract ModuleType ModuleType { get; }
    public List<Face> Faces { get; protected set; } = new List<Face>();

    public virtual void AddFace(Face face)
    {
        Faces.Add(face);
    }
}
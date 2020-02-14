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

public interface IModulable
{
    ModuleType ModuleType { get; }
}
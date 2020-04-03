public class ManipulationInputModule : InputModule
{
    public delegate Laser[] Manipulate(Laser laser);
    public Manipulate manipulateFunction;

    public override ModuleType ModuleType => ModuleType.ManipulationInput;
}
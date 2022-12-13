namespace JigLibX.Physics
{
    /// <summary>
    /// Virtual base class for all joints
    /// All joints are expected to do the following in their constructor:
    /// 1. create whatever constraints are necessary
    /// 2. register these constraints with the physics engine
    /// </summary>
    public abstract class Joint : Controller
    {
    }
}
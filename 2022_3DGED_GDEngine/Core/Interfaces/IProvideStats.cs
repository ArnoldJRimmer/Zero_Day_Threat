namespace GD.Engine
{
    /// <summary>
    /// Any class that shows stats information will implement this interface
    /// </summary>
    /// <see cref="GD.Engine.Scene"/>
    public interface IProvideStats
    {
        string GetStatistics();
    }
}
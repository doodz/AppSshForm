namespace Doods.StdFramework.Interfaces
{
    /// <summary>
    ///     Permet de savoir si le <see cref="BaseViewModel" /> est modifié ou non.
    /// </summary>
    public interface IDirty
    {
        /// <summary>
        ///     Retourne <c>True</c> s’il y a eu des modifications.
        /// </summary>
        bool IsDirty { get; set; }
    }
}

namespace Doods.StdFramework.Interfaces
{
    /// <summary>
    /// Interface that consists of determining the location of a file according to the environment of dev (ios, UWP, Android ...).
    /// </summary>
    public interface IFileHelper
    {
        /// <summary>
        /// Determines the location of the file.
        /// </summary>
        /// <param name="filename">The name of the file</param>
        /// <returns>The path.</returns>
        string GetLocalFilePath(string filename);

        /// <summary>
        /// Get the local download folder
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <returns>The path.</returns>
        string GetDownloadPath();

        void StartAppFromFile(string filePath);
    }
}

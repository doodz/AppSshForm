using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doods.StdFramework.Interfaces
{
    /// <summary>
    /// Interface qui consiste à déterminer l'emplacement d'un fichier en fonctione de l'environement de dev (ios,UWP, android ...).
    /// </summary>
    public interface IFileHelper
    {
        /// <summary>
        /// Détermine l'emplacement du fichier.
        /// </summary>
        /// <param name="filename">Le nom du fichier</param>
        /// <returns>Le path.</returns>
        string GetLocalFilePath(string filename);
    }
}

using System;
using System.IO;

namespace Transactions.IO.Files.Helpers
{
    public class FilesHelper
    {
        /// <summary>
        /// Ensures that the folder that contains the temporary files exists.
        /// </summary>
        public static void EnsureTempFolderExists()
        {
            var tempFolder = GetTempFolderPath();
            if (!Directory.Exists(tempFolder))
            {
                Directory.CreateDirectory(tempFolder);
            }
        }

        /// <summary>
        /// Returns a unique temporary file name.
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        public static string GetTempFileName(string extension)
        {
            var tempFolder = GetTempFolderPath();
            var guid = Guid.NewGuid();
            return Path.Combine(tempFolder, guid.ToString().Substring(0, 16)) + extension;
        }

        internal static string GetTempFolderPath(string folderName = "FileMgr")
        {
            return Path.Combine(Path.GetTempPath(), folderName);
        }
    }
}
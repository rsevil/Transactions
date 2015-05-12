using System.IO;
using Utils.Transactions.IO.Files.Helpers;

namespace Utils.Transactions.IO.Files.Local.Operations
{
    /// <summary>
    /// Rollbackable operation which deletes a file. An exception is not thrown if the file does not exist.
    /// </summary>
    internal sealed class DeleteFile : SingleFileOperation
    {
        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="path">The file to be deleted.</param>
        public DeleteFile(string path)
            : base(path)
        {
        }

        public override void Execute()
        {
            if (File.Exists(path))
            {
                string temp = FilesHelper.GetTempFileName(Path.GetExtension(path));
                File.Copy(path, temp);
                backupPath = temp;
            }

            File.Delete(path);
        }
    }
}
using System;
using System.IO;
using Utils.Transactions.IO.Files.Helpers;

namespace Utils.Transactions.IO.Files
{
    public abstract class FileManagerBase : TransactionManager, IFileManager
    {
        public abstract bool DirectoryExists(string path);

        public abstract bool FileExists(string path);

        public abstract void AppendAllText(string path, string contents);

        public abstract void Copy(string sourceFileName, string destFileName, bool overwrite);

        public abstract void CreateDirectory(string path);

        public abstract void Delete(string path);

        public abstract void DeleteDirectory(string path);

        public abstract void Move(string srcFileName, string destFileName);

        public abstract void Snapshot(string fileName);

        public abstract string WriteAllText(string path, string contents, bool renameIfExists = false);

        public abstract string WriteAllBytes(string path, byte[] contents, bool renameIfExists = false);

        /// <summary>Gets a temporary directory.</summary>
        /// <returns>The path to the newly created temporary directory.</returns>
        public virtual string GetTempDirectory()
        {
            return GetTempDirectory(Path.GetTempPath(), string.Empty);
        }

        /// <summary>Gets a temporary directory.</summary>
        /// <param name="parentDirectory">The parent directory.</param>
        /// <param name="prefix">The prefix of the directory name.</param>
        /// <returns>Path to the temporary directory. The temporary directory is created automatically.</returns>
        public virtual string GetTempDirectory(string parentDirectory, string prefix)
        {
            Guid g = Guid.NewGuid();
            string dirName = Path.Combine(parentDirectory, prefix + g.ToString().Substring(0, 16));

            CreateDirectory(dirName);

            return dirName;
        }

        /// <summary>Creates a temporary file name. File is not automatically created.</summary>
        /// <param name="extension">File extension (with the dot).</param>
        public virtual string GetTempFileName(string extension)
        {
            var retVal = FilesHelper.GetTempFileName(extension);

            Snapshot(retVal);

            return retVal;
        }

        /// <summary>
        /// Creates a temporary file name. File is not automatically created.
        /// </summary>
        /// <returns></returns>
        public virtual string GetTempFileName()
        {
            return GetTempFileName(".tmp");
        }
    }
}
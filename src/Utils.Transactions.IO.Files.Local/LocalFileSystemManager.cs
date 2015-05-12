using System;
using System.IO;
using Utils.Transactions.IO.Files.Helpers;
using Utils.Transactions.IO.Files.Local.Operations;

namespace Utils.Transactions.IO.Files.Local
{
    /// <summary>
    /// File Resource Manager. Allows inclusion of file system operations in transactions.
    /// http://www.chinhdo.com/20080825/transactional-file-manager/
    /// </summary>
    public class LocalFileSystemManager : FileManagerBase
    {
        /// <summary>
        /// Initializes the <see cref="LocalFileSystemManager"/> class.
        /// </summary>
        public LocalFileSystemManager()
            : base()
        {
            FilesHelper.EnsureTempFolderExists();
        }

        /// <summary>
        /// Appends the specified string the file, creating the file if it doesn't already exist.
        /// </summary>
        /// <param name="path">The file to append the string to.</param>
        /// <param name="contents">The string to append to the file.</param>
        public override void AppendAllText(string path, string contents)
        {
            ExecuteOperation(() => new AppendAllText(path, contents));
        }

        /// <summary>Copies the specified <paramref name="sourceFileName"/> to <paramref name="destFileName"/>.</summary>
        /// <param name="sourceFileName">The file to copy.</param>
        /// <param name="destFileName">The name of the destination file.</param>
        /// <param name="overwrite">true if the destination file can be overwritten, otherwise false.</param>
        public override void Copy(string sourceFileName, string destFileName, bool overwrite)
        {
            ExecuteOperation(() => new Copy(sourceFileName, destFileName, overwrite));
        }

        /// <summary>Creates all directories in the specified path.</summary>
        /// <param name="path">The directory path to create.</param>
        public override void CreateDirectory(string path)
        {
            ExecuteOperation(() => new CreateDirectory(path));
        }

        /// <summary>Deletes the specified file. An exception is not thrown if the file does not exist.</summary>
        /// <param name="path">The file to be deleted.</param>
        public override void Delete(string path)
        {
            ExecuteOperation(() => new DeleteFile(path));
        }

        /// <summary>Deletes the specified directory and all its contents. An exception is not thrown if the directory does not exist.</summary>
        /// <param name="path">The directory to be deleted.</param>
        public override void DeleteDirectory(string path)
        {
            ExecuteOperation(() => new DeleteDirectory(path));
        }

        /// <summary>Moves the specified file to a new location.</summary>
        /// <param name="srcFileName">The name of the file to move.</param>
        /// <param name="destFileName">The new path for the file.</param>
        public override void Move(string srcFileName, string destFileName)
        {
            ExecuteOperation(() => new Move(srcFileName, destFileName));
        }

        /// <summary>Take a snapshot of the specified file. The snapshot is used to rollback the file later if needed.</summary>
        /// <param name="fileName">The file to take a snapshot for.</param>
        public override void Snapshot(string fileName)
        {
            if (IsInTransaction())
                EnlistOperation(new Snapshot(fileName));
        }

        /// <summary>Creates a file, write the specified <paramref name="contents"/> to the file.</summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The string to write to the file.</param>
        /// <param name="renameIfExists">Rename the file if a file with that filename exists, otherwise overwrite.</param>
        /// <returns>The full filepath of the file</returns>
        public override string WriteAllText(string path, string contents, bool renameIfExists = false)
        {
            return ExecuteOperation(() => new WriteAllText(path, contents, renameIfExists));
        }

        /// <summary>Creates a file, write the specified <paramref name="contents"/> to the file.</summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The bytes to write to the file.</param>
        /// <param name="renameIfExists">Rename the file if a file with that filename exists, otherwise overwrite.</param>
        /// <returns>The full filepath of the file</returns>
        public override string WriteAllBytes(string path, byte[] contents, bool renameIfExists = false)
        {
            return ExecuteOperation(() => new WriteAllBytes(path, contents, renameIfExists));
        }

        /// <summary>Determines whether the specified path refers to a directory that exists on disk.</summary>
        /// <param name="path">The directory to check.</param>
        /// <returns>True if the directory exists.</returns>
        public override bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>Determines whether the specified file exists.</summary>
        /// <param name="path">The file to check.</param>
        /// <returns>True if the file exists.</returns>
        public override bool FileExists(string path)
        {
            return File.Exists(path);
        }

        private TReturn ExecuteOperation<TReturn>(Func<IRollbackableOperation<TReturn>> operation)
        {
            return IsInTransaction()
                ? EnlistOperation(operation())
                : operation().Execute();
        }

        private void ExecuteOperation(Func<IRollbackableOperation> operation)
        {
            if (IsInTransaction())
                EnlistOperation(operation());
            else
                operation().Execute();
        }
    }
}
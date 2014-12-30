using ChinhDo.Transactions.Utils;
using System.IO;

namespace ChinhDo.Transactions.FileManager.Operations
{
    /// <summary>
    /// Creates a file, and writes the specified contents to it.
    /// </summary>
    sealed class WriteAllBytes : SingleFileOperation<string>
    {
        private readonly byte[] contents;
        private readonly bool renameIfExists;

        /// <summary>
        /// Instantiates the class.
        /// </summary>
        /// <param name="path">The file to write to.</param>
        /// <param name="contents">The string to write to the file.</param>
        /// <param name="renameIfExists">Rename the file if a file with that filename exists, otherwise overwrite.</param>
        public WriteAllBytes(string path, byte[] contents, bool renameIfExists)
            : base(path)
        {
            this.contents = contents;
            this.renameIfExists = renameIfExists;
        }

        public override string Execute()
        {
            if (File.Exists(path))
            {
                if (renameIfExists)
                {
                    var dir = Path.GetDirectoryName(path);
                    var fn = Path.GetFileNameWithoutExtension(path);
                    var ext = Path.GetExtension(path);

                    var i = 0;
                    while (File.Exists(path))
                    {
                        i++;
                        path = Path.Combine(dir, string.Format("{0}_({1}){2}", fn, i, ext));
                    }
                }
                else
                {
                    string temp = FileUtils.GetTempFileName(Path.GetExtension(path));
                    File.Copy(path, temp);
                    backupPath = temp;
                }
            }

            File.WriteAllBytes(path, contents);

            return Path.GetFileName(path);
        }
    }
}
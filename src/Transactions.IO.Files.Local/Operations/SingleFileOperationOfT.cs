namespace Transactions.IO.Files.Local.Operations
{
    internal abstract class SingleFileOperation<T> : SingleFileOperationBase, IRollbackableExecutable<T>
    {
        public SingleFileOperation(string path)
            : base(path)
        {
        }

        public abstract T Execute();
    }
}
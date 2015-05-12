namespace Utils.Transactions.IO.Files.Local.Operations
{
    internal abstract class SingleFileOperation<T> : SingleFileOperationBase, IRollbackableOperation<T>
    {
        public SingleFileOperation(string path)
            : base(path)
        {
        }

        public abstract T Execute();
    }
}
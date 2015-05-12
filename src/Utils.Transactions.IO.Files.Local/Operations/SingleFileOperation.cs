namespace Utils.Transactions.IO.Files.Local.Operations
{
    internal abstract class SingleFileOperation : SingleFileOperationBase, IRollbackableOperation
    {
        public SingleFileOperation(string path)
            : base(path)
        {
        }

        public abstract void Execute();
    }
}
namespace Transactions.IO.Files.Local.Operations
{
    internal abstract class SingleFileOperation : SingleFileOperationBase, IRollbackableExecutable
    {
        public SingleFileOperation(string path)
            : base(path)
        {
        }

        public abstract void Execute();
    }
}
namespace Transactions
{
    /// <summary>
    /// Represents a rollbackable executable.
    /// </summary>
    public interface IRollbackableExecutable : IRollbackable, IExecutable { }
}
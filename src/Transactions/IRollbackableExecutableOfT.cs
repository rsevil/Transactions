namespace Transactions
{
    /// <summary>
    /// Represents a rollbackable executable that returns a value on execution.
    /// </summary>
    public interface IRollbackableExecutable<T> : IRollbackable, IExecutable<T> { }
}
namespace Utils.Transactions
{
    /// <summary>
    /// Represents an operation
    /// </summary>
    public interface IExecutable
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        void Execute();
    }
}
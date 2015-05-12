namespace Utils.Transactions
{
    /// <summary>
    /// Represents an operation
    /// </summary>
    public interface IOperation
    {
        /// <summary>
        /// Executes the operation.
        /// </summary>
        void Execute();
    }
}
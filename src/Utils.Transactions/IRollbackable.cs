namespace Utils.Transactions
{
    /// <summary>
    /// Represents an object that can rollback something
    /// </summary>
    public interface IRollbackable
    {
        /// <summary>
        /// Rolls backs, restores the original state.
        /// </summary>
        void Rollback();
    }
}
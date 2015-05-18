using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Transactions;

namespace Transactions
{
    public abstract class TransactionManager
    {
        /// <summary>
        /// Dictionary of transaction enlistment objects for the current thread.
        /// </summary>
        protected static IDictionary<string, IEnlistmentNotification> _enlistments;

        public TransactionManager()
        {
            _enlistments = new ConcurrentDictionary<string, IEnlistmentNotification>();
        }

        protected virtual bool IsInTransaction()
        {
            return GetCurrentTransaction() != null;
        }

        protected virtual Transaction GetCurrentTransaction()
        {
            return Transaction.Current;
        }

        protected virtual IEnlistmentNotification GetEnlistmentNotification()
        {
            IEnlistmentNotification enlistment = null;
            var tx = GetCurrentTransaction();
            if (!_enlistments.TryGetValue(tx.TransactionInformation.LocalIdentifier, out enlistment))
            {
                enlistment = new TransactionEnlistment(tx);
                _enlistments.Add(tx.TransactionInformation.LocalIdentifier, enlistment);
            }
            return enlistment;
        }

        protected virtual void EnlistOperation(IRollbackableExecutable operation)
        {
            ((TransactionEnlistment)GetEnlistmentNotification()).EnlistOperation(operation);
        }

        protected virtual T EnlistOperation<T>(IRollbackableExecutable<T> operation)
        {
            return ((TransactionEnlistment)GetEnlistmentNotification()).EnlistOperation(operation);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Transactions;

namespace Utils.Transactions
{
    /// <summary>Provides two-phase commits/rollbacks/etc for a single <see cref="Transaction"/>.</summary>
    internal class TransactionEnlistment : IEnlistmentNotification
    {
        protected readonly IList<IRollbackable> _journal;

        public TransactionEnlistment()
        {
            _journal = new List<IRollbackable>();
        }

        /// <summary>Initializes a new instance of the <see cref="TransactionEnlistment"/> class.</summary>
        /// <param name="tx">The Transaction.</param>
        public TransactionEnlistment(Transaction tx)
            : this()
        {
            tx.EnlistVolatile(this, EnlistmentOptions.None);
        }

        /// <summary>
        /// Enlists <paramref name="operation"/> in its journal, so it will be committed or rolled back
        /// together with the other enlisted operations.
        /// </summary>
        /// <param name="operation"></param>
        public virtual void EnlistOperation(IRollbackableExecutable operation)
        {
            operation.Execute();

            _journal.Add(operation);
        }

        /// <summary>
        /// Enlists <paramref name="operation"/> in its journal, so it will be committed or rolled back
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation"></param>
        /// <returns>The result of the operation</returns>
        public virtual T EnlistOperation<T>(IRollbackableExecutable<T> operation)
        {
            var r = operation.Execute();

            _journal.Add(operation);

            return r;
        }

        public virtual void Commit(Enlistment enlistment)
        {
            DisposeJournal();

            enlistment.Done();
        }

        public virtual void InDoubt(Enlistment enlistment)
        {
            Rollback(enlistment);
        }

        public virtual void Prepare(PreparingEnlistment preparingEnlistment)
        {
            preparingEnlistment.Prepared();
        }

        /// <summary>Notifies an enlisted object that a transaction is being rolled back (aborted).</summary>
        /// <param name="enlistment">A <see cref="T:System.Transactions.Enlistment"></see> object used to send a response to the transaction manager.</param>
        /// <remarks>This is typically called on a different thread from the transaction thread.</remarks>
        public virtual void Rollback(Enlistment enlistment)
        {
            try
            {
                // Roll back journal items in reverse order
                for (int i = _journal.Count - 1; i >= 0; i--)
                {
                    _journal[i].Rollback();
                }

                DisposeJournal();
            }
            catch (Exception ex)
            {
                throw new TransactionException("Failed to roll back.", ex);
            }

            enlistment.Done();
        }

        protected virtual void DisposeJournal()
        {
            IDisposable disposable;
            for (int i = _journal.Count - 1; i >= 0; i--)
            {
                disposable = _journal[i] as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }

                _journal.RemoveAt(i);
            }
        }
    }
}
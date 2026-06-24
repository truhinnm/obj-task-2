using System;
using System.Collections.Generic;
using System.Text;

namespace ParallelRW
{
    public static class StaticServer
    {
        private static int count = 0;

        private static readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public static int GetCount()
        {
            _lock.EnterReadLock();
            try
            {
                return count;
            }
            finally
            {
                _lock.ExitReadLock();
            }

        }

        public static void AddToCount(int value)
        {
            _lock.EnterWriteLock();
            try
            {
                count += value;
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace VI.ParallelComputing
{
    public interface IExecutor
    {
        Dictionary<string, Action> Executors { get; set; }
    }
    public interface ISession
    {
        Guid SessionID { get; set; }
        IExecutor Actions { get; set; }
    }

    public static class SessionControl
    {
        private static Dictionary<Guid, ISession> StoredSessions;

        public static ISession GetSession(Guid id)
        {
            return StoredSessions[id];
        }
    }
}

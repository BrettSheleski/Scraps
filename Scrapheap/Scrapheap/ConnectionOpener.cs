using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapheap
{
    public class ConnectionOpener : IDisposable
    {
        protected ConnectionOpener(IDbConnection connection)
        {
            this.Connection = connection;

            this.closeWhenDone = connection.State != ConnectionState.Open;
        }

        public IDbConnection Connection { get; private set; }

        private bool closeWhenDone;

        public void Dispose()
        {
            if (Connection != null && closeWhenDone)
            {
                this.Connection.Close();
            }
        }

        public static ConnectionOpener Open(IDbConnection connection)
        {
            var opener = new ConnectionOpener(connection);

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return opener;
        }

        public async static Task<ConnectionOpener> OpenAsync(DbConnection connection)
        {
            var opener = new ConnectionOpener(connection);

            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            return opener;
        }
    }
}

using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repository
{
    public class JournalMessageRepository : BaseRepository<JournalMessage>, IJournalMessageRepository
    {
        public JournalMessageRepository(IMongoDBContext context) : base(context)
        {
        }
    }
}

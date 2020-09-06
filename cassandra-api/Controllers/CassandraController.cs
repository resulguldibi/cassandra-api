using client.cassandra.core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cassandra_api.Controllers
{
    [ApiController]
    [Route("api/[action]")]
    public class CassandraController : ControllerBase
    {
        private readonly ILogger<CassandraController> _logger;
        private readonly ICassandraClientProvider _cassandraClientProvider;
        public CassandraController(ILogger<CassandraController> logger, ICassandraClientProvider cassandraClientProvider)
        {
            _logger = logger;
            _cassandraClientProvider = cassandraClientProvider;
        }


        [HttpGet]
        [ActionName("samples")]
        public async Task<IEnumerable<SampleEntity>> GetDataFromCassandra()
        {
            using (ICassandraClient client = this._cassandraClientProvider.GetCassandraClient("my_cluster", "my_keyspace"))
            {
                return client.Fetch<SampleEntity>("SELECT * FROM my_table");
            }
        }
    }

    public class SampleEntity
    {
        public string Id { get; set; }
        public int Count { get; set; }
        public string Word { get; set; }
    }
}

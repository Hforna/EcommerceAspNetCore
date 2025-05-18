using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [CollectionDefinition(nameof(CollectionTest))]
    public class CollectionTest : ICollectionFixture<ConfigureApplicationTests>
    {
    }
}

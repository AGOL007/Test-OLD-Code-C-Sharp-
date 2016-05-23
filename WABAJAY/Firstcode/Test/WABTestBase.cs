using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAPTestBase;


namespace WABTest
{
    public abstract class WABTestBase : TestBaseWAB
    {
        public override void Setup()
        {
            base.Setup();
        }

        public override void TearDown()
        {
            base.TearDown();
        }
    }
}

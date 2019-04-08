using FanucControllerLibrary.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PARRHI.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.HelperClasses.Tests
{
    [TestClass()]
    public class ForwardKinematicsTests
    {
        [TestMethod()]
        public void CaluclateForwardKinematicsTest()
        {
            Vector6 q = new Vector6(0, 0, 0, 0, 0, 0);
            ForwardKinematics fk = new ForwardKinematics();
            var p = fk.CaluclateForwardKinematics(q);
        }

        [TestMethod()]
        public void ForwardKinematicsLoadTest()
        {
            var w = System.Diagnostics.Stopwatch.StartNew();
            ForwardKinematics fk = new ForwardKinematics();
            for (int i = 0; i < 1000; i++)
            {
                Vector6 q = new Vector6(i, 2 * i % 360, i % 360, 0, 3 * i % 360, i % 360);
                var p = fk.CaluclateForwardKinematics(q);
            }
            w.Stop();
            //about 49 ms total -> 49ns per Cycle. That should be fine

        }
    }
}
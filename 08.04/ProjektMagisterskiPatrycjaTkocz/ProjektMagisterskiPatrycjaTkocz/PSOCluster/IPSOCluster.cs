using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektMagisterskiPatrycjaTkocz
{
    interface IPSOCluster
    {
        void RandomlyInitializeSwarm();
        void UpdateVelocity(Particle currentParticle);
        void UpdatePosition(Particle currentParticle);
    }
}

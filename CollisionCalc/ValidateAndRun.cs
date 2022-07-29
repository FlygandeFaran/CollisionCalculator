using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VMS.TPS.Common.Model.API;

namespace CollisionCalc
{
    public class ValidateAndRun
    {
        public static void Run(PlanSetup planSetup)
        {
            if (planSetup == null)
                MessageBox.Show("Ingen plan vald!", "Check Field Size", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else if (planSetup.GetType() != typeof(IonPlanSetup))
                MessageBox.Show("Planen är ej en protonplan!", "Check Field Size", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                CollisionCalc.MainWindow.Main((IonPlanSetup)planSetup);
        }
    }
}

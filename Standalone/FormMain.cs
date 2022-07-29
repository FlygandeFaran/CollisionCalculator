using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;

namespace Standalone
{
    public partial class FormMain : Form
    {
        private VMS.TPS.Common.Model.API.Application app;

        public FormMain()
        {
            InitializeComponent();
            Initialize();
        }
        public void Initialize()
        {
            try
            {
                app = VMS.TPS.Common.Model.API.Application.CreateApplication(null, null);
                Execute(app);
                /*listBoxCourse.SelectedIndex = 0;
                listBoxPlanSetup.SelectedIndex = 1;*/
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Execute(VMS.TPS.Common.Model.API.Application app)
        {
            //Patient currPatient;
            string patID = "194108181472";
            string coursID = "C1";
            string planID = "P1_Meningeom";
            //PatientSummary patSum = app.PatientSummaries.FirstOrDefault(p => p.Id.Equals(patID));
            Patient pat = app.OpenPatientById(patID);
            Course course = pat.Courses.FirstOrDefault(c => c.Id.Equals(coursID));
            PlanSetup planSetup = course.PlanSetups.FirstOrDefault(p => p.Id.Equals(planID));
            CollisionCalc.ValidateAndRun.Run(planSetup);
            /*foreach (var patient in app.PatientSummaries)
            {
                if (patient.Id == patID)
                {
                    currPatient = app.OpenPatientById(patient.Id);
                    foreach (var course in currPatient.Courses)
                    {
                        if (course.Id == coursID)
                        {

                        }
                            planSetup = course.PlanSetups.First(p => p.Id.Equals(planID));
                        if (planSetup != null)
                            CollisionCalc.ValidateAndRun.Run(planSetup);
                    }
                }
            }*/
        }
    }
}

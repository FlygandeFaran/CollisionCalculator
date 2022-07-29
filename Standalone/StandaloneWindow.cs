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
    public partial class StandaloneWindow : Form
    {
        private VMS.TPS.Common.Model.API.Application app;
        private Patient patient;
        private Course course;

        public StandaloneWindow()
        {
            InitializeComponent();

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
            //foreach (var patient in app.PatientSummaries)
            //    listBoxPatient.Items.Add(patient.Id);
            string patID = "194108181472";
            string coursID = "C1";
            string planID = "P1_Meningeom";
            //PatientSummary patSum = app.PatientSummaries.FirstOrDefault(p => p.Id.Equals(patID));
            Patient pat = app.OpenPatientById(patID);
            Course course = pat.Courses.FirstOrDefault(c => c.Id.Equals(coursID));
            PlanSetup planSetup = course.PlanSetups.FirstOrDefault(p => p.Id.Equals(planID));
            System.Windows.Window window = new System.Windows.Window();
            CollisionCalc.ValidateAndRun.Run(planSetup);
            ElementHost.EnableModelessKeyboardInterop(window);
            window.Show();
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender == listBoxPatient)
            {
                app.ClosePatient();
                listBoxCourse.Items.Clear();
                listBoxPlanSetup.Items.Clear();
                patient = app.OpenPatientById(listBoxPatient.SelectedItem.ToString());
                foreach (var course in patient.Courses)
                    listBoxCourse.Items.Add(course.Id);
            }
            else if (sender == listBoxCourse)
            {
                listBoxPlanSetup.Items.Clear();
                course = patient.Courses.First(c => c.Id.Equals(listBoxCourse.SelectedItem.ToString(), StringComparison.Ordinal));
                foreach (var planSetup in course.PlanSetups)
                    listBoxPlanSetup.Items.Add(planSetup.Id);
            }
            else if (sender == listBoxPlanSetup)
            {
                PlanSetup planSetup = course.PlanSetups.First(p => p.Id.Equals(listBoxPlanSetup.SelectedItem.ToString(), StringComparison.Ordinal));
                System.Windows.Window window = new System.Windows.Window();
                CollisionCalc.ValidateAndRun.Run(planSetup);
                ElementHost.EnableModelessKeyboardInterop(window);
                window.Show();
            }
        }

        private void StandaloneWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                app.ClosePatient();
                app.Dispose();
            }
            catch
            {
            }
        }

        private void textBoxId_TextChanged(object sender, EventArgs e)
        {
            try
            {
                app.ClosePatient();
                listBoxCourse.Items.Clear();
                listBoxPlanSetup.Items.Clear();
                patient = app.OpenPatientById(textBoxId.Text);
                foreach (var course in patient.Courses)
                    listBoxCourse.Items.Add(course.Id);
            }
            catch
            {

            }
        }
    }
}

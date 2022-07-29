using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using System.Windows.Forms;
using VMS.TPS;
using System.IO;
using System.Windows.Media.Media3D;


namespace CollisionCalc
{
    public partial class MainWindow : Form
    {
        Script script = new Script();
        CouchCalculator couchCalculator = new CouchCalculator();
        CSVwriter csv = new CSVwriter();
        private IonPlanSetup plan;
        private IonBeam beam;
        private string filePath;
        private string AnonymNamn;
        private Structure m_structureExternal;
        private Structure m_structureCTV;
        List<string> strOut;

        public static void Main(IonPlanSetup ionplan)
        {
            System.Windows.Forms.Application.Run(new MainWindow(ionplan));
        }
        public MainWindow(IonPlanSetup ionplan)
        {
            InitializeComponent();
            InitializeGUI();
            this.plan = ionplan;
            filePath = @"\\SKVfile01.skandion.local\Gemensamdata$\Intern\QA Patient\CollisionCheck\" + plan.Course.Patient.FirstName.Substring(0, 2) + plan.Course.Patient.LastName.Substring(0, 2) + plan.Course.Patient.Id.Substring(8, 4) + @"\";//default Hard coded path
            AnonymNamn = plan.Id;
        }
        public string FileName
        {
            get { return filePath + AnonymNamn + ".csv"; }
        }
        private void InitializeGUI()
        {
            cmbMaskList.Items.AddRange(Enum.GetNames(typeof(Masks)));
            cmbMaskList.SelectedIndex = (int)Masks.Civco;
            cmbMaskList.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void ExportButton_Click(object sender, EventArgs e)
        {
            strOut = new List<string>();
            if (File.Exists(FileName))
                MessageBox.Show("The file already exists");
            else
            {
                couchCalculator.MaskType = (Masks)cmbMaskList.SelectedIndex;
                beam = plan.IonBeams.First();
                
                bool ok = ReadInput();
                if (ok)
                {
                    strOut.Add(couchCalculator.couchCoordinates());
                    for (int i = 0; i < 9; i++)//Thomas fel, jag ville ha foreach men fick inte :P
                    {
                        if (i < plan.IonBeams.Count())
                        {
                            beam = plan.IonBeams.ElementAt(i);
                            ok = ReadInput();

                            if (ok)
                                strOut.Add(couchCalculator.ToString());
                        }
                        else
                            strOut.Add(string.Empty);
                    }
                    Directory.CreateDirectory(filePath);
                    strOut.Add("BODY");
                    AddStructureCordinates(m_structureExternal);
                    strOut.Add("CTV");
                    AddStructureCordinates(m_structureCTV);

                    csv.WriteToCSV(FileName, strOut);
                    MessageBox.Show("Done!");
                }
            }
        }
        private void AddStructureCordinates(Structure m_structure)
        {
            foreach (Point3D coordinate in m_structure.MeshGeometry.Positions)
                strOut.Add(coordinate.X.ToString() + ";" + coordinate.Y.ToString("0") + ";" + coordinate.Z.ToString("0"));
            strOut.Add("Triangles");
            foreach (int triangle in m_structure.MeshGeometry.TriangleIndices)
                strOut.Add(triangle.ToString());
            /*strOut.Add("Normals"); Om Eclipse lägger in Normals och texture coordinates i framtiden kan det användas
            foreach (Vector3D normal in m_structure.MeshGeometry.Normals)
                strOut.Add(normal.X.ToString() + ";" + normal.Y.ToString() + ";" + normal.Z.ToString());
            strOut.Add("Texture coordinates");
            foreach (System.Windows.Point coordinate in m_structure.MeshGeometry.TextureCoordinates)
                strOut.Add(coordinate.X.ToString() + ";" + coordinate.Y.ToString());*/

        }
        #region ReadInput
        public bool ReadInput()
        {
            ReadSnout();
            bool structuresOK = Readstructures();
            bool gantryAngleOK = ReadGantryAngle();
            bool snoutPosOK = ReadSnoutPos();
            bool couchRotOK = ReadCouchRot();
            bool fieldNamesOK = ReadFieldID();
            bool markerPosOK = ReadMarkerPos();
            bool isoCenterOK = ReadIsoCenter();
            bool userOriginOK = ReadUserOrigin();

            return gantryAngleOK && snoutPosOK && couchRotOK && markerPosOK && isoCenterOK && userOriginOK && fieldNamesOK && structuresOK;
        }
        private bool Readstructures()
        {
            bool ok = false;
            m_structureExternal = plan.StructureSet.Structures.FirstOrDefault(s => s.DicomType.Contains("EXTERNAL"));
            //m_structureExternal.Id = "BODY";
            m_structureCTV = plan.StructureSet.Structures.FirstOrDefault(s => s.DicomType.Contains("CTV"));
            //m_structureCTV.Id = "CTV";
            if (m_structureExternal != null && m_structureCTV != null)
                ok = true;
            else
                MessageBox.Show("Kunde inte hitta struktur med typen Body eller CTV");
            return ok;
        }
        private bool ReadGantryAngle()
        {
            bool ok = true;
            double gantryAngle = beam.IonControlPoints.First().GantryAngle;

            ok = !double.IsNaN(gantryAngle);
            if (ok)
                couchCalculator.GantryAngle = gantryAngle;
            else
                MessageBox.Show("There is a problem with the gantry angle");

            return ok;
        }
        private bool ReadSnoutPos()
        {
            bool ok = true;
            double snoutPos = beam.IonControlPoints.First().SnoutPosition;

            ok = !double.IsNaN(snoutPos);
            if (ok)
                couchCalculator.SnoutPos = snoutPos;
            else
                MessageBox.Show("There is a problem with the snout position");

            return ok;
        }
        private bool ReadFieldID()
        {
            bool ok = true;
            string fieldID = beam.Id;

            ok = !string.IsNullOrEmpty(fieldID);
            if (ok)
                couchCalculator.FieldID = fieldID;
            else
                MessageBox.Show("There is a problem with the field names");

            return ok;
        }
        private void ReadSnout()
        {
            bool rsCheck = beam.RangeShifters.Any();
            couchCalculator.HasSnout = rsCheck;
            
        }
        private bool ReadCouchRot()
        {
            bool ok = true;
            double couchRot = beam.IonControlPoints.First().PatientSupportAngle;

            ok = !double.IsNaN(couchRot);
            if (ok)
                couchCalculator.CouchRot = couchRot;
            else
                MessageBox.Show("There is a problem with the couch rotation");

            return ok;
        }
        private bool ReadMarkerPos()
        {
            bool ok = false;
            List<Structure> structures = plan.StructureSet.Structures.ToList();
            Structure marker = structures.FirstOrDefault(s => s.Id.Contains("couchMarker"));
            if (marker != null)
            {
                couchCalculator.Marker = marker;
                ok = true;
            }
            else
                MessageBox.Show("There is no structure named couchMarker in the plan");

            return ok;
        }
        private bool ReadIsoCenter()
        {
            bool ok = true;
            VVector isoCenter = beam.IsocenterPosition;
            if (ok)
                couchCalculator.ISOcenter = isoCenter;
            else
                MessageBox.Show("Could not find isocenter");
            return ok;
        }
        private bool ReadUserOrigin()
        {
            bool ok = true;
            VVector userOrigin = plan.StructureSet.Image.UserOrigin;
            if (ok)
                couchCalculator.UserOrigin = userOrigin;
            else
                MessageBox.Show("Could not find User Origin");
            return ok;
        }
        #endregion
    }
}

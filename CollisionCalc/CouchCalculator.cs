using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using System.Windows.Media.Media3D;
using System.Windows.Forms;

namespace CollisionCalc
{
    class CouchCalculator
    {
        private double gantryAngle;
        private double couchRot;
        private double snoutPos;
        private Masks maskType;
        private bool hasSnout;
        private Structure marker;
        private Structure couch;
        private VVector isoCenter;
        private VVector userOrigin;
        private string fieldID;
        
        #region Getters and Setters
        public string FieldID
        {
            get { return fieldID; }
            set { fieldID = value; }
        }
        public double GantryAngle
        {
            get { return gantryAngle; }
            set { gantryAngle = value; }
        }
        public double CouchRot
        {
            get { return couchRot; }
            set { couchRot = value; }
        }
        public double SnoutPos
        {
            get { return snoutPos / 10; }
            set { snoutPos = value; }
        }
        public Masks MaskType
        {
            get { return maskType; }
            set { maskType = value; }
        }
        public bool HasSnout
        {
            get { return hasSnout; }
            set { hasSnout = value; }
        }
        public Structure Marker
        {
            get { return marker; }
            set { marker = value; }
        }
        public Structure Couch
        {
            get { return couch; }
            set { couch = value; }
        }
        public VVector ISOcenter
        {
            get { return isoCenter; }
            set { isoCenter = value; }
        }
        public VVector UserOrigin
        {
            get { return userOrigin; }
            set { userOrigin = value; }
        }
        public double couchPosX
        {
            get { return (isoCenter.x - userOrigin.x) / 10; }
        }
        public double couchPosY
        {
            get
            {
                double yMinDistance = 0;
                double markerY = marker.CenterPoint.y;
                yMinDistance = (markerY - isoCenter.y) / 10;
                return yMinDistance;
            }
        }
        public double couchPosZ
        {
            get
            {
                double zMinDistance = 0;
                double markerZ = marker.CenterPoint.z;
                zMinDistance = (markerZ - isoCenter.z) / 10;
                return zMinDistance;
            }
        }
        public double IsoShiftX
        {
            get { return (isoCenter.x - userOrigin.x) / 10; }
        }
        public double IsoShiftY
        {
            get { return (isoCenter.z - userOrigin.z) / 10; }
        }
        public double IsoShiftZ
        {
            get { return -(isoCenter.y - userOrigin.y) / 10; }
        }
        #endregion
        public string couchCoordinates() // lägg till avstånd från iso till user origin, DICOM offset,
        {
            string strOut = string.Format("{0:0.##};{1:0.##};{2:0.##};{3:0.##};{4:0.##};{5:0.##};{6:0.##};{7:0.##};{8:0.##}", couchPosX, couchPosY, couchPosZ, IsoShiftX, IsoShiftY, IsoShiftZ, userOrigin.x, userOrigin.y, userOrigin.z);
            return strOut;
        }
        public override string ToString()
        {
            string strOut = string.Format("{0};{1:0.##};{2:0.##};{3};{4};{5};{6:0.##};{7}", fieldID, couchPosX, IsoShiftY, gantryAngle, couchRot, hasSnout, SnoutPos, maskType.ToString());
            return strOut;
        }
    }
}

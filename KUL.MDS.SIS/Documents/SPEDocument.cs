//using System;
//using System.IO;
//using System.Windows.Forms;
//using KUL.MDS.MDITemplate;
//using KUL.MDS.SIS.Forms;
//using System.Xml;
//using System.Runtime.InteropServices;

//namespace KUL.MDS.SIS.Documents
//{
//    /// <summary>
//    /// This class provides an object for loading WinSpec SPE files.
//    /// </summary>
//    [Document("WinSpec SPE", ".spe")]
//    public class SPEDocument : KUL.MDS.MDITemplate.MdiDocument
//    {
//        private KUL.MDS.Data.DataSet m_dtstData;

//        public KUL.MDS.Data.DataSet DataSet
//        {
//            get
//            {
//                return this.m_dtstData;
//            }
//            set
//            {
//                this.m_dtstData = value;
//            }
//        }

//        // Constructor.
//        public SPEDocument()
//        {
//        }

//        protected override MdiViewForm OnCreateView()
//        {
//            return new SPEViewForm();
//        }

//        protected override bool OnLoadDocument(string _sFilePath)
//        {
//            // Create a Singleton Instance of the datahandler.
//            KUL.MDS.Data.DocumentTypes Dataloader = KUL.MDS.Data.DocumentTypes.Instance;

//            // Load the data from file into a DataSet.
//            KUL.MDS.Data.DataSet CurrentData = Dataloader.OpenDocument(_sFilePath);
//            CurrentData.LoadData(new FileStream(_sFilePath, FileMode.Open));
//            this.m_dtstData = CurrentData;
//            bool _bResult = true;
//            return _bResult;
//        }

//        protected override bool OnSaveDocument(string _sFilePath)
//        {
//            return true;
//        }
//    }
//}


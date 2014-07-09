//using System;
//using System.IO;
//using System.Windows.Forms;
//using SIS.MDITemplate;
//using SIS.Forms;
//using System.Xml;
//using System.Runtime.InteropServices;

//namespace SIS.Documents
//{
//    /// <summary>
//    /// This class provides an object for loading WinSpec SPE files.
//    /// </summary>
//    [Document("WinSpec SPE", ".spe")]
//    public class SPEDocument : SIS.MDITemplate.MdiDocument
//    {
//        private SIS.Data.DataSet m_dtstData;

//        public SIS.Data.DataSet DataSet
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
//            SIS.Data.DocumentTypes Dataloader = SIS.Data.DocumentTypes.Instance;

//            // Load the data from file into a DataSet.
//            SIS.Data.DataSet CurrentData = Dataloader.OpenDocument(_sFilePath);
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


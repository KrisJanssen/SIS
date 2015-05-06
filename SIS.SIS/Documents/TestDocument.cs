//using System;
//using SIS.MDITemplate;
//using SIS.Forms;

//namespace SIS.Documents
//{
//    [Document("Test document files", ".txt")]
//    public class TestDocument : SIS.MDITemplate.MdiDocument
//    {
//        private string m_sText;

//        public TestDocument()
//        {
//            m_sText = "";
//        }

//        public string Text
//        {
//            get
//            {
//                return m_sText;
//            }

//            set
//            {
//                m_sText = value;
//            }
//        }

//        protected override MdiViewForm OnCreateView()
//        {
//            return new TestViewForm();
//        }

//        protected override bool OnLoadDocument(string sFilePath)
//        {
//            bool fResult = true;

//            try
//            {
//                System.IO.StreamReader reader = new System.IO.StreamReader(sFilePath);
//                m_sText = reader.ReadToEnd();
//                reader.Close();
//            }
//            catch (System.IO.IOException)
//            {
//                fResult = false;
//            }

//            return fResult;
//        }

//        protected override bool OnSaveDocument(string sFilePath)
//        {
//            bool fResult = true;

//            try
//            {
//                System.IO.StreamWriter writer = new System.IO.StreamWriter(sFilePath);
//                writer.Write(m_sText);
//                writer.Close();
//            }
//            catch (System.IO.IOException)
//            {
//                fResult = false;
//            }

//            return fResult;
//        }
//    }
//}


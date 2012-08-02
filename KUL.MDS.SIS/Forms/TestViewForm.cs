//using System;
//using System.Collections;
//using System.ComponentModel;
//using System.Drawing;
//using System.Windows.Forms;
//using KUL.MDS.SIS.Documents;
//using System.Reflection;

//namespace KUL.MDS.SIS.Forms
//{
//    public class TestViewForm : KUL.MDS.MDITemplate.MdiViewForm
//    {
//        private System.Windows.Forms.TextBox m_textBox;
//        private System.ComponentModel.IContainer components = null;

//        public TestViewForm()
//        {
//            // This call is required by the Windows Form Designer.
//            InitializeComponent();

//            // TODO: Add any initialization after the InitializeComponent call
//        }

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                if (components != null)
//                {
//                    components.Dispose();
//                }
//            }
//            base.Dispose(disposing);
//        }

//        #region Designer generated code
//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.m_textBox = new System.Windows.Forms.TextBox();
//            this.SuspendLayout();
//            // 
//            // m_textBox
//            // 
//            this.m_textBox.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.m_textBox.Location = new System.Drawing.Point(0, 0);
//            this.m_textBox.Multiline = true;
//            this.m_textBox.Name = "m_textBox";
//            this.m_textBox.Size = new System.Drawing.Size(292, 273);
//            this.m_textBox.TabIndex = 0;
//            // 
//            // TestViewForm
//            // 
//            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
//            this.ClientSize = new System.Drawing.Size(292, 273);
//            this.Controls.Add(this.m_textBox);
//            this.Name = "TestViewForm";
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }
//        #endregion

//        protected override void OnUpdateDocument()
//        {
//            TestDocument document = this.Document as TestDocument;
//            document.Text = m_textBox.Text;
//        }

//        protected override void OnUpdateView(object update)
//        {
//            TestDocument document = this.Document as TestDocument;
//            m_textBox.Text = document.Text;
//        }

//        protected override void OnInitialUpdate()
//        {
//            TestDocument document = this.Document as TestDocument;
//            m_textBox.Text = document.Text;
//        }
//    }
//}


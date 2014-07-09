using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIS.Documents;

namespace SIS.Forms
{
    public partial class ScanSettingsForm : Form
    {
        private object[] m_oParameters;
        private ScanSettings m_docScanDocument;

        // declare the EventHandler
        public event EventHandler UpdateParameters;

        public ScanSettingsForm(ScanSettings __scnstSettings)
        {
            this.m_docScanDocument = __scnstSettings;
            InitializeComponent();
            UpdateThis();
        }

        // Wire up the event
        protected void OnUpdateParameters()
        {
            if (UpdateParameters != null) UpdateParameters(this, new NotifyEventArgs(this.m_docScanDocument));
        }

        private void UpdateThis()
        {
            this.m_txtbxSetImageWidth.Text = this.m_docScanDocument.ImageWidthPx.ToString();
            this.m_txtbxSetImageHeight.Text = this.m_docScanDocument.ImageHeightPx.ToString();
            this.m_txtbxSetImageDepth.Text = this.m_docScanDocument.ImageDepthPx.ToString();
            this.m_txtbxSetOverScanPxX.Text = this.m_docScanDocument.XOverScanPx.ToString();
            this.m_txtbxSetOverScanPxY.Text = this.m_docScanDocument.YOverScanPx.ToString();
            this.m_txtbxSetOverScanPxZ.Text = this.m_docScanDocument.ZOverScanPx.ToString();
            this.m_txtbxSetTimePPixel.Text = this.m_docScanDocument.TimePPixel.ToString();
            this.m_txtbxSetImageWidthnm.Text = this.m_docScanDocument.XScanSizeNm.ToString();
            this.m_txtbxSetImageHeightnm.Text = this.m_docScanDocument.YScanSizeNm.ToString();
            this.m_txtbxSetImageDepthnm.Text = this.m_docScanDocument.ZScanSizeNm.ToString();
            this.m_txtbxSetInitX.Text = this.m_docScanDocument.InitXNm.ToString();
            this.m_txtbxSetInitY.Text = this.m_docScanDocument.InitYNm.ToString();
            this.m_txtbxSetInitZ.Text = this.m_docScanDocument.InitZNm.ToString();
            this.textBox1.Text = this.m_docScanDocument.Annotation;
        }

        private void m_btnAccept_Click(object sender, EventArgs e)
        {
            this.m_docScanDocument.ImageWidthPx = Convert.ToUInt16(this.m_txtbxSetImageWidth.Text.Trim());
            this.m_docScanDocument.ImageHeightPx = Convert.ToUInt16(this.m_txtbxSetImageHeight.Text.Trim());
            this.m_docScanDocument.ImageDepthPx = Convert.ToUInt16(this.m_txtbxSetImageDepth.Text.Trim());
            this.m_docScanDocument.XOverScanPx = Convert.ToUInt16(this.m_txtbxSetOverScanPxX.Text.Trim());
            this.m_docScanDocument.YOverScanPx = Convert.ToUInt16(this.m_txtbxSetOverScanPxY.Text.Trim());
            this.m_docScanDocument.ZOverScanPx = Convert.ToUInt16(this.m_txtbxSetOverScanPxZ.Text.Trim());
            this.m_docScanDocument.TimePPixel = Convert.ToSingle(this.m_txtbxSetTimePPixel.Text.Trim());
            this.m_docScanDocument.XScanSizeNm = Convert.ToSingle(this.m_txtbxSetImageWidthnm.Text.Trim());
            this.m_docScanDocument.YScanSizeNm = Convert.ToSingle(this.m_txtbxSetImageHeightnm.Text.Trim());
            this.m_docScanDocument.ZScanSizeNm = Convert.ToSingle(this.m_txtbxSetImageDepthnm.Text.Trim());
            this.m_docScanDocument.InitXNm = Convert.ToSingle(this.m_txtbxSetInitX.Text.Trim());
            this.m_docScanDocument.InitYNm = Convert.ToSingle(this.m_txtbxSetInitY.Text.Trim());
            this.m_docScanDocument.InitZNm = Convert.ToSingle(this.m_txtbxSetInitZ.Text.Trim());
            this.m_docScanDocument.Annotation = this.textBox1.Text.Trim();

            bool _boolPrimaryValidationPassed = false;

            _boolPrimaryValidationPassed = this.validationProvider1.Validate();
            this.validationProvider1.ValidationMessages(!_boolPrimaryValidationPassed);

            if (_boolPrimaryValidationPassed == false)
            {
                //MessageBox.Show(m_strInvalidScanSettingsMsg1, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show("Error", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }


            if (_boolPrimaryValidationPassed == true)
            {
                this.OnUpdateParameters();
                this.Visible = false;
            }
        }

        private void m_btnCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void m_btnDefault_Click(object sender, EventArgs e)
        {
            this.m_docScanDocument = new ScanSettings();
            this.UpdateThis();
        }
    }
}

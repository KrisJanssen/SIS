// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationRuleDesignForm.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   ValidationRuleDesignForm use to setup validation rule and assign it to a
//   specific control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace KUL.MDS.Validation.Design
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Windows.Forms;

    /// <summary>
    /// ValidationRuleDesignForm use to setup validation rule and assign it to a
    /// specific control.
    /// </summary>
    public class ValidationRuleDesignForm : System.Windows.Forms.Form
    {
        #region Fields

        /// <summary>
        /// The close button.
        /// </summary>
        private Button CloseButton;

        /// <summary>
        /// The controls drop down list.
        /// </summary>
        private ComboBox ControlsDropDownList;

        /// <summary>
        /// The load patterns button.
        /// </summary>
        private Button LoadPatternsButton;

        /// <summary>
        /// The new button.
        /// </summary>
        private Button NewButton;

        /// <summary>
        /// The reg ex error message text box.
        /// </summary>
        private TextBox RegExErrorMessageTextBox;

        /// <summary>
        /// The reg ex pattern text box.
        /// </summary>
        private TextBox RegExPatternTextBox;

        /// <summary>
        /// The reg ex patterns drop down list.
        /// </summary>
        private ComboBox RegExPatternsDropDownList;

        /// <summary>
        /// The reg ex test value text box.
        /// </summary>
        private TextBox RegExTestValueTextBox;

        /// <summary>
        /// The save button.
        /// </summary>
        private Button SaveButton;

        /// <summary>
        /// The use pattern button.
        /// </summary>
        private Button UsePatternButton;

        /// <summary>
        /// The _ component change service.
        /// </summary>
        private IComponentChangeService _ComponentChangeService = null;

        /// <summary>
        /// The _ designer host.
        /// </summary>
        private IDesignerHost _DesignerHost = null;

        /// <summary>
        /// The _ local validation provider.
        /// </summary>
        private ValidationProvider _LocalValidationProvider = new ValidationProvider();

        /// <summary>
        /// The _ reg ex patterns.
        /// </summary>
        private RegExPatternCollection _RegExPatterns = null;

        /// <summary>
        /// The _ test value validation rule.
        /// </summary>
        private ValidationRule _TestValueValidationRule = new ValidationRule();

        /// <summary>
        /// The _ validation components.
        /// </summary>
        private Hashtable _ValidationComponents = new Hashtable();

        /// <summary>
        /// The _ validation provider.
        /// </summary>
        private ValidationProvider _ValidationProvider = null;

        /// <summary>
        /// The components.
        /// </summary>
        private IContainer components;

        /// <summary>
        /// The label 1.
        /// </summary>
        private Label label1;

        /// <summary>
        /// The label 2.
        /// </summary>
        private Label label2;

        /// <summary>
        /// The label 3.
        /// </summary>
        private Label label3;

        /// <summary>
        /// The label 4.
        /// </summary>
        private Label label4;

        /// <summary>
        /// The label 5.
        /// </summary>
        private Label label5;

        /// <summary>
        /// The open file dialog 1.
        /// </summary>
        private OpenFileDialog openFileDialog1;

        /// <summary>
        /// The property grid 1.
        /// </summary>
        private PropertyGrid propertyGrid1;

        /// <summary>
        /// The tab control 1.
        /// </summary>
        private TabControl tabControl1;

        /// <summary>
        /// The tab page 1.
        /// </summary>
        private TabPage tabPage1;

        /// <summary>
        /// The tab page 2.
        /// </summary>
        private TabPage tabPage2;

        /// <summary>
        /// The tool tip 1.
        /// </summary>
        private ToolTip toolTip1;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRuleDesignForm"/> class. 
        /// Default Ctor.
        /// </summary>
        public ValidationRuleDesignForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationRuleDesignForm"/> class. 
        /// Validation Designer Ctor.
        /// </summary>
        /// <param name="designerHost">
        /// </param>
        /// <param name="validationProvider">
        /// </param>
        /// <param name="editorSelectedComponents">
        /// </param>
        public ValidationRuleDesignForm(
            IDesignerHost designerHost, 
            ValidationProvider validationProvider, 
            object[] editorSelectedComponents)
        {
            this.InitializeComponent();
            this._DesignerHost = designerHost;
            this._ComponentChangeService =
                this._DesignerHost.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            this._ValidationProvider = validationProvider;
            if (editorSelectedComponents == null || editorSelectedComponents.Length <= 0)
            {
                this.GetInputControls(this._DesignerHost.Container.Components);
            }
            else
            {
                foreach (object inputControl in editorSelectedComponents)
                {
                    this.AddInputControl(inputControl);
                }
            }

            if (this.ControlsDropDownList.Items.Count > 0)
            {
                this.ControlsDropDownList.SelectedIndex = 0;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// The add input control.
        /// </summary>
        /// <param name="inputControl">
        /// The input control.
        /// </param>
        private void AddInputControl(object inputControl)
        {
            if (this._ValidationProvider.CanExtend(inputControl))
            {
                this._ValidationComponents.Add(TypeDescriptor.GetComponentName(inputControl), inputControl);
                this.ControlsDropDownList.Items.Add(TypeDescriptor.GetComponentName(inputControl));
            }
        }

        /// <summary>
        /// The close button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// The controls drop down list_ selected index changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ControlsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.NewButton.Enabled = this.ControlsDropDownList.SelectedItem != null;
            this.SaveButton.Enabled = this.NewButton.Enabled;
            if (this.NewButton.Enabled)
            {
                ValidationRule vr =
                    this._ValidationProvider.GetValidationRule(
                        this._ValidationComponents[this.ControlsDropDownList.SelectedItem]);
                this.propertyGrid1.SelectedObject = vr;
            }
        }

        /// <summary>
        /// The get input controls.
        /// </summary>
        /// <param name="components">
        /// The components.
        /// </param>
        private void GetInputControls(ComponentCollection components)
        {
            try
            {
                foreach (object inputControl in components)
                {
                    this.AddInputControl(inputControl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            ComponentResourceManager resources =
                new System.ComponentModel.ComponentResourceManager(typeof(ValidationRuleDesignForm));
            this.SaveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ControlsDropDownList = new System.Windows.Forms.ComboBox();
            this.CloseButton = new System.Windows.Forms.Button();
            this.NewButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RegExErrorMessageTextBox = new System.Windows.Forms.TextBox();
            this.RegExTestValueTextBox = new System.Windows.Forms.TextBox();
            this.RegExPatternTextBox = new System.Windows.Forms.TextBox();
            this.LoadPatternsButton = new System.Windows.Forms.Button();
            this.RegExPatternsDropDownList = new System.Windows.Forms.ComboBox();
            this.UsePatternButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();

            // SaveButton
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(320, 336);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(72, 24);
            this.SaveButton.TabIndex = 0;
            this.SaveButton.Text = "Save";
            this.toolTip1.SetToolTip(this.SaveButton, "Save ValidationRule settings to Form.");
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);

            // label1
            this.label1.Font = new System.Drawing.Font(
                "Microsoft Sans Serif", 
                8.25F, 
                System.Drawing.FontStyle.Bold, 
                System.Drawing.GraphicsUnit.Point, 
                (byte)0);
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Controls:";

            // ControlsDropDownList
            this.ControlsDropDownList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ControlsDropDownList.Location = new System.Drawing.Point(64, 8);
            this.ControlsDropDownList.Name = "ControlsDropDownList";
            this.ControlsDropDownList.Size = new System.Drawing.Size(296, 21);
            this.ControlsDropDownList.TabIndex = 3;
            this.toolTip1.SetToolTip(this.ControlsDropDownList, "Controls that can be validate.");
            this.ControlsDropDownList.SelectedIndexChanged +=
                new System.EventHandler(this.ControlsDropDownList_SelectedIndexChanged);

            // CloseButton
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(408, 336);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "Close";
            this.toolTip1.SetToolTip(this.CloseButton, "Close and go back to Form Designer.");
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);

            // NewButton
            this.NewButton.Enabled = false;
            this.NewButton.Location = new System.Drawing.Point(232, 336);
            this.NewButton.Name = "NewButton";
            this.NewButton.Size = new System.Drawing.Size(75, 23);
            this.NewButton.TabIndex = 7;
            this.NewButton.Text = "New";
            this.toolTip1.SetToolTip(this.NewButton, "Create new ValidationRule for selected Control.");
            this.NewButton.Click += new System.EventHandler(this.NewButton_Click);

            // tabControl1
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(8, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(480, 280);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);

            // tabPage1
            this.tabPage1.Controls.Add(this.propertyGrid1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(472, 254);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ValidationRule";

            // propertyGrid1
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Top;
            this.propertyGrid1.HelpVisible = false;
            this.propertyGrid1.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(472, 256);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.ToolbarVisible = false;

            // tabPage2
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.RegExErrorMessageTextBox);
            this.tabPage2.Controls.Add(this.RegExTestValueTextBox);
            this.tabPage2.Controls.Add(this.RegExPatternTextBox);
            this.tabPage2.Controls.Add(this.LoadPatternsButton);
            this.tabPage2.Controls.Add(this.RegExPatternsDropDownList);
            this.tabPage2.Controls.Add(this.UsePatternButton);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(472, 254);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Regular Expression Tester";

            // label5
            this.label5.BackColor = System.Drawing.Color.Red;
            this.label5.Font = new System.Drawing.Font(
                "Arial", 
                8.25F, 
                System.Drawing.FontStyle.Bold, 
                System.Drawing.GraphicsUnit.Point, 
                (byte)0);
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(24, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 23);
            this.label5.TabIndex = 10;
            this.label5.Text = "Test Value:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // label4
            this.label4.Location = new System.Drawing.Point(40, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "Pattern:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;

            // label3
            this.label3.Location = new System.Drawing.Point(8, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "Error Message:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;

            // label2
            this.label2.Location = new System.Drawing.Point(16, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "Pattern Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // RegExErrorMessageTextBox
            this.RegExErrorMessageTextBox.Location = new System.Drawing.Point(96, 56);
            this.RegExErrorMessageTextBox.Name = "RegExErrorMessageTextBox";
            this.RegExErrorMessageTextBox.Size = new System.Drawing.Size(352, 20);
            this.RegExErrorMessageTextBox.TabIndex = 6;
            this.toolTip1.SetToolTip(
                this.RegExErrorMessageTextBox, 
                "Error message to use when pattern failed to validate.");

            // RegExTestValueTextBox
            this.RegExTestValueTextBox.Location = new System.Drawing.Point(96, 168);
            this.RegExTestValueTextBox.Multiline = true;
            this.RegExTestValueTextBox.Name = "RegExTestValueTextBox";
            this.RegExTestValueTextBox.Size = new System.Drawing.Size(352, 40);
            this.RegExTestValueTextBox.TabIndex = 5;
            this.toolTip1.SetToolTip(
                this.RegExTestValueTextBox, 
                "Pattern validate as test value are input into this textbox.");
            this.RegExTestValueTextBox.TextChanged += new System.EventHandler(this.RegExTestValueTextBox_TextChanged);

            // RegExPatternTextBox
            this.RegExPatternTextBox.Location = new System.Drawing.Point(96, 88);
            this.RegExPatternTextBox.Multiline = true;
            this.RegExPatternTextBox.Name = "RegExPatternTextBox";
            this.RegExPatternTextBox.Size = new System.Drawing.Size(352, 72);
            this.RegExPatternTextBox.TabIndex = 4;
            this.toolTip1.SetToolTip(this.RegExPatternTextBox, "Regular Expression Pattern string.");

            // LoadPatternsButton
            this.LoadPatternsButton.Location = new System.Drawing.Point(352, 24);
            this.LoadPatternsButton.Name = "LoadPatternsButton";
            this.LoadPatternsButton.Size = new System.Drawing.Size(88, 23);
            this.LoadPatternsButton.TabIndex = 3;
            this.LoadPatternsButton.Text = "Load Patterns";
            this.toolTip1.SetToolTip(this.LoadPatternsButton, "Load saved patterns xml file.");
            this.LoadPatternsButton.Click += new System.EventHandler(this.LoadPatternsButton_Click);

            // RegExPatternsDropDownList
            this.RegExPatternsDropDownList.Location = new System.Drawing.Point(96, 24);
            this.RegExPatternsDropDownList.Name = "RegExPatternsDropDownList";
            this.RegExPatternsDropDownList.Size = new System.Drawing.Size(248, 21);
            this.RegExPatternsDropDownList.TabIndex = 1;
            this.toolTip1.SetToolTip(this.RegExPatternsDropDownList, "Name of pattern.");

            // UsePatternButton
            this.UsePatternButton.Enabled = false;
            this.UsePatternButton.Location = new System.Drawing.Point(368, 224);
            this.UsePatternButton.Name = "UsePatternButton";
            this.UsePatternButton.Size = new System.Drawing.Size(75, 23);
            this.UsePatternButton.TabIndex = 5;
            this.UsePatternButton.Text = "Use Pattern";
            this.toolTip1.SetToolTip(this.UsePatternButton, "Copy current pattern to selected Control ValidationRule.");
            this.UsePatternButton.Click += new System.EventHandler(this.UsePatternButton_Click);

            // openFileDialog1
            this.openFileDialog1.FileName = "ReExPatternStore.xml";
            this.openFileDialog1.Filter = "Xml files|*.xml|All files|*.*";
            this.openFileDialog1.Title = "Select a Regular Expression pattern store xml file...";

            // ValidationRuleDesignForm
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(490, 368);
            this.Controls.Add(this.NewButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.ControlsDropDownList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.HelpButton = true;
            this.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ValidationRuleDesignForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ValidationRules Designer";
            this.TopMost = true;
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
        }

        /// <summary>
        /// The load patterns button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void LoadPatternsButton_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == this.openFileDialog1.ShowDialog())
            {
                object repc =
                    ValidationUtil.XmlStringToObject(
                        ValidationUtil.FileToString(this.openFileDialog1.FileName), 
                        typeof(RegExPatternCollection));
                this._RegExPatterns = repc as RegExPatternCollection;
                if (repc != null)
                {
                    this.RegExPatternsDropDownList.DataSource = this._RegExPatterns;
                    this.RegExPatternsDropDownList.DisplayMember = "PatternName";
                    this.RegExErrorMessageTextBox.DataBindings.Add("Text", this._RegExPatterns, "ErrorMessage");
                    this.RegExPatternTextBox.DataBindings.Add("Text", this._RegExPatterns, "Pattern");
                    this.RegExTestValueTextBox.DataBindings.Add("Text", this._RegExPatterns, "TestValue");
                }
            }
        }

        /// <summary>
        /// The new button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void NewButton_Click(object sender, EventArgs e)
        {
            this.propertyGrid1.SelectedObject = new ValidationRule();
        }

        /// <summary>
        /// The reg ex test value text box_ text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void RegExTestValueTextBox_TextChanged(object sender, EventArgs e)
        {
            ValidationRule vr = this._TestValueValidationRule;
            vr.RegExPattern = this.RegExPatternTextBox.Text;
            vr.ErrorMessage = this.RegExErrorMessageTextBox.Text;
            if (vr.RegExPattern.Length > 0)
            {
                this._LocalValidationProvider.SetValidationRule(this.RegExTestValueTextBox, vr);
                this._LocalValidationProvider.SetIconAlignment(
                    this.RegExTestValueTextBox, 
                    ErrorIconAlignment.BottomLeft);
                bool isValid = this._LocalValidationProvider.Validate();
                this._LocalValidationProvider.ValidationMessages(!isValid);
                this.UsePatternButton.Enabled = isValid;
                if (isValid)
                {
                    this.label5.BackColor = System.Drawing.Color.Green;
                }
                else
                {
                    this.label5.BackColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                this.UsePatternButton.Enabled = false;
            }
        }

        /// <summary>
        /// The save button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                object selectedComponent = null;
                if (this.ControlsDropDownList.SelectedIndex >= 0)
                {
                    selectedComponent = this._ValidationComponents[this.ControlsDropDownList.SelectedItem];
                }

                if (selectedComponent != null)
                {
                    this._ComponentChangeService.OnComponentChanging(this, null);
                    this._ValidationProvider.SetValidationRule(
                        selectedComponent, 
                        this.propertyGrid1.SelectedObject as ValidationRule);
                    this._ComponentChangeService.OnComponentChanged(selectedComponent, null, null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        /// <summary>
        /// The test pattern button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TestPatternButton_Click(object sender, EventArgs e)
        {
            bool isValid = this._LocalValidationProvider.Validate();
            this._LocalValidationProvider.ValidationMessages(!isValid);
            if (isValid)
            {
                MessageBox.Show("Test Value is valid.", this.Name);
            }
        }

        /// <summary>
        /// The use pattern button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void UsePatternButton_Click(object sender, EventArgs e)
        {
            ValidationRule vr = this.propertyGrid1.SelectedObject as ValidationRule;
            if (vr != null)
            {
                if (!this.RegExErrorMessageTextBox.Text.Trim().Equals(string.Empty))
                {
                    vr.ErrorMessage = this.RegExErrorMessageTextBox.Text;
                }

                if (!this.RegExErrorMessageTextBox.Text.Trim().Equals(string.Empty))
                {
                    vr.RegExPattern = this.RegExPatternTextBox.Text;
                }

                this.tabControl1.SelectedIndex = 0;
                this.propertyGrid1.Update();
            }
        }

        /// <summary>
        /// The property grid 1_ property value changed.
        /// </summary>
        /// <param name="s">
        /// The s.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ValidationRule vr = this.propertyGrid1.SelectedObject as ValidationRule;
            Control ctrl = this._ValidationComponents[this.ControlsDropDownList.SelectedItem] as Control;
            if (vr != null && ctrl != null & vr.IsRequired == true && !ctrl.Text.Equals(string.Empty)
                && !ctrl.Text.Equals(vr.InitialValue))
            {
                vr.InitialValue = ctrl.Text;
            }
        }

        /// <summary>
        /// The tab control 1_ selected index changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidationRule vr = this.propertyGrid1.SelectedObject as ValidationRule;

            if (this.tabControl1.SelectedIndex == 1 && vr != null)
            {
                this.RegExPatternsDropDownList.Text = string.Empty;
                this.RegExPatternTextBox.Text = vr.RegExPattern;
                this.RegExErrorMessageTextBox.Text = vr.ErrorMessage;
            }
        }

        #endregion
    }
}
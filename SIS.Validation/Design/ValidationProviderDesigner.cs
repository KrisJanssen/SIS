// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationProviderDesigner.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   ValidationProviderDesigner add verbs for Validation setup on the PropertyGrid.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace KUL.MDS.Validation.Design
{
    using System;
    using System.ComponentModel.Design;

    /// <summary>
    /// ValidationProviderDesigner add verbs for Validation setup on the PropertyGrid.
    /// </summary>
    public class ValidationProviderDesigner : System.ComponentModel.Design.ComponentDesigner
    {
        #region Fields

        /// <summary>
        /// The _ component change service.
        /// </summary>
        private IComponentChangeService _ComponentChangeService = null;

        /// <summary>
        /// The _ designer host.
        /// </summary>
        private IDesignerHost _DesignerHost = null;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationProviderDesigner"/> class. 
        /// Default Ctor.
        /// </summary>
        public ValidationProviderDesigner()
        {
            this.Verbs.Add(
                new DesignerVerb("Edit ValidationRules...", new EventHandler(this.EditValidationRulesHandler)));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Makes sure local variables are valid.
        /// </summary>
        private void CustomInitialize()
        {
            if (this._DesignerHost == null)
            {
                this._DesignerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
            }

            if (this._ComponentChangeService == null)
            {
                this._ComponentChangeService =
                    this._DesignerHost.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            }
        }

        /// <summary>
        /// Handle "Edit ValidationRules..." verb event.
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        private void EditValidationRulesHandler(object sender, EventArgs e)
        {
            this.CustomInitialize();
            ValidationRuleDesignForm vrdf = new ValidationRuleDesignForm(
                this._DesignerHost, 
                this.Component as ValidationProvider, 
                null);
            vrdf.ShowDialog();
        }

        #endregion
    }
}
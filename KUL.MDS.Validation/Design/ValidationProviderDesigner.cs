using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace SIS.Validation.Design
{
	/// <summary>
	/// ValidationProviderDesigner add verbs for Validation setup on the PropertyGrid.
	/// </summary>
	public class ValidationProviderDesigner : System.ComponentModel.Design.ComponentDesigner
	{
		private IComponentChangeService			_ComponentChangeService = null;
		private IDesignerHost					_DesignerHost			= null;

		/// <summary>
		/// Default Ctor.
		/// </summary>
		public ValidationProviderDesigner()
		{
			this.Verbs.Add(new DesignerVerb("Edit ValidationRules...", new EventHandler(this.EditValidationRulesHandler)));
		}

		/// <summary>
		/// Handle "Edit ValidationRules..." verb event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EditValidationRulesHandler(object sender, EventArgs e)
		{
			this.CustomInitialize();
			ValidationRuleDesignForm vrdf = new ValidationRuleDesignForm(this._DesignerHost, this.Component as ValidationProvider, null);
			vrdf.ShowDialog();

		}

		/// <summary>
		/// Makes sure local variables are valid.
		/// </summary>
		private void CustomInitialize()
		{
			if (this._DesignerHost == null)
				this._DesignerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;

			if (this._ComponentChangeService == null)
				this._ComponentChangeService = this._DesignerHost.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
		}
	}
}

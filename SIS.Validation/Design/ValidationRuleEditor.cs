using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace SIS.Validation.Design
{
	internal class ValidationRuleEditor : UITypeEditor
	{
		/// <summary>
		/// Setup to display our ValidationRuleDesignForm
		/// </summary>
		/// <param name="context"></param>
		/// <param name="provider"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if ((context != null) && (provider != null))
			{				
				// get ValidationProvider name - "ValidationRule on validationProvider1"
				string sValidationProviderName = context.PropertyDescriptor.DisplayName.Split(' ')[2];

				// find component matches provider name
				ValidationProvider valueVP = null;
				foreach(IComponent c in context.Container.Components)
				{
					valueVP = c as ValidationProvider;
					if ((valueVP != null) && (valueVP.Site.Name == sValidationProviderName))
						break;
				}

				// get component that are selected
				object[] selectedComponents = context.Instance as object[];
				if (selectedComponents == null)
				{
					selectedComponents = new object[] { context.Instance };
				}

				// create ValidationRuleDesignForm 
				ValidationRuleDesignForm vrdf = new ValidationRuleDesignForm((IDesignerHost) provider.GetService(typeof(IDesignerHost)), valueVP, selectedComponents);
				vrdf.ShowDialog();

				// reselect the component on the UI
				ISelectionService selectionService = (ISelectionService) provider.GetService(typeof(ISelectionService));
				selectionService.SetSelectedComponents(selectedComponents);
			}
			return base.EditValue(context, provider, value);
		}

		/// <summary>
		/// Tell designer that our editor is a Modal Form.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			if (context != null) return UITypeEditorEditStyle.Modal;
			return base.GetEditStyle(context);

		}
	}
 
}

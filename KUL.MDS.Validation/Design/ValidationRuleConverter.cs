using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace SIS.Validation.Design
{
	/// <summary>
	/// ValidationRuleConverter allow ValidationRule to be Designer's serializable.
	/// </summary>
	public class ValidationRuleConverter : TypeConverter
	{
		/// <summary>
		/// Override so Designer can Serialize ValidationRule.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return (destinationType == typeof(InstanceDescriptor)) ? true : base.CanConvertTo(context, destinationType);
		}

		/// <summary>
		/// Override so designer can Deserialize ValidationRule.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				Type[] typeArray = new Type[0] {} ;
 
				// get default constructor
				System.Reflection.ConstructorInfo ci =
					typeof(ValidationRule).GetConstructor(typeArray);
            
				return new InstanceDescriptor(ci, typeArray, false);
			}
    
			return base.ConvertTo(context, culture, value, destinationType);

		}
	}
}

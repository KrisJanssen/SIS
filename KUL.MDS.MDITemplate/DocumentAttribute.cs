namespace SIS.MDITemplate
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class DocumentAttribute : Attribute
	{
		private string m_sName = null;
		private string m_sExtension = null;
		
		public DocumentAttribute(string sName, string sExtension)
		{			
			this.m_sName = sName;

			if (sExtension.Length == 0)
			{
				throw new Exception("You must specify and extension for the document");
			}

			if (sExtension[0] != '.')
			{
				throw new Exception("The extension for the document must begin with a period ('.')");
			}

			this.m_sExtension = sExtension;
		}

		public string Name
		{
			get
			{
				return this.m_sName;
			}
		}

		public string Extension
		{
			get
			{
				return this.m_sExtension;
			}
		}
	}
}

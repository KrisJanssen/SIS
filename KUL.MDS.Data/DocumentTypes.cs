using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace SIS.Data
{
	public class DocumentTypes
	{
        // We want DocumentTypes to be a singleton. This way it will always be available and it will hold
        // all information we will ever need on the documents we will try to load. We can subsequently create
        // the singleton instance in our code and simply call its opendocument or any other methods.
        #region Singleton Pattern
        private static volatile DocumentTypes m_instance;
        private static object m_syncRoot = new object();

        public static DocumentTypes Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_syncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new DocumentTypes();
                        }
                    }
                }

                return m_instance;
            }
        }
        #endregion

		private Hashtable m_mapExtensionToType = null;
		private string m_sLoadFilter = null;
		private string m_sCreateFilter = null;
		
        // The constructor obviously needs to be private to prevent normal instantiation.
		private DocumentTypes()
		{
			Load();
		}

		public string OpenDialogFilter
		{
			get
			{
				return m_sLoadFilter;
			}
		}

		public int Count
		{
			get
			{
				return m_mapExtensionToType.Count;
			}
		}

        // This method is obsolete.
        //public DataSet OpenDocument()
        //{
        //    foreach (System.Type typeDocument in m_mapExtensionToType.Values)
        //    {
        //        DataSet document = typeDocument.GetConstructor(new System.Type[0]).Invoke(new object[0]) as DataSet;

        //        if (document != null)
        //        {
        //            return document;
        //        }
        //    }

        //    return null;
        //}

		private DataSet CreateDocumentHelper(string sFileName)
		{
			int nLastDot = sFileName.LastIndexOf('.');

			if (nLastDot < 0)
			{
				return null;
			}

			string sExtension = sFileName.Substring(nLastDot);

			System.Type typeDocument = m_mapExtensionToType[sExtension] as System.Type;

			if (typeDocument == null)
			{
				return null;
			}

			return typeDocument.GetConstructor(new System.Type[0]).Invoke(new object[0]) as DataSet;
		}

		public DataSet CreateDocument(string sFileName)
		{
			DataSet document = CreateDocumentHelper(sFileName);
			
            //if (!document.SaveDocument(sFileName))
            //{
            //    return null;
            //}

			return document;
		}

		public DataSet OpenDocument(string sFileName)
		{
			DataSet document = CreateDocumentHelper(sFileName);

            //if (document == null || !document.LoadDocument(sFileName, true))
            //{
            //    return null;
            //}
            
			return document;
		}

		private void Load()
		{
			System.Reflection.Assembly assembly = System.Reflection.Assembly.GetCallingAssembly();
			m_mapExtensionToType = new Hashtable();

			if (assembly != null)
			{
				System.Type [] aTypes = assembly.GetTypes();

				StringBuilder stringBuilderFilter = new StringBuilder();
				StringBuilder stringBuilderCreateFilter = new StringBuilder();

				foreach (System.Type type in aTypes)
				{
					DocumentAttribute [] aDocumentAttributes = GetDocumentAttributes(type);

					if (aDocumentAttributes != null && aDocumentAttributes.Length > 0)
					{
						LoadAttributes(type, aDocumentAttributes);
						LoadOpenFilterString(stringBuilderFilter, aDocumentAttributes);
						LoadCreateFilterString(stringBuilderCreateFilter, aDocumentAttributes);
					}
				}

				stringBuilderFilter.Append("All files (*.*)|*.*||");
				m_sLoadFilter = stringBuilderFilter.ToString();

				stringBuilderCreateFilter.Append("|");
				m_sCreateFilter = stringBuilderCreateFilter.ToString();
			}
		}

		private DocumentAttribute[] GetDocumentAttributes(System.Type type)
		{
			return type.GetCustomAttributes(typeof(DocumentAttribute), true) as DocumentAttribute[];
		}

		private void LoadAttributes(System.Type type, DocumentAttribute [] aDocumentAttributes)
		{
			foreach (DocumentAttribute attribute in aDocumentAttributes)
			{
				m_mapExtensionToType.Add(attribute.Extension, type);
			}
		}

		private void BuildExtensionMap(Hashtable mapNameToExtension, DocumentAttribute [] aDocumentAttributes)
		{
			foreach (DocumentAttribute attribute in aDocumentAttributes)
			{
				if (mapNameToExtension.Contains(attribute.Name))
				{
					StringBuilder extensionBuilder = mapNameToExtension[attribute.Name] as StringBuilder;
					extensionBuilder.Append("|");
					extensionBuilder.Append(attribute.Extension);
				}
				else
				{
					StringBuilder extensionBuilder = new StringBuilder();
					extensionBuilder.Append(attribute.Extension);
					mapNameToExtension.Add(attribute.Name, extensionBuilder);
				}
			}
		}

		private void LoadCreateFilterString(StringBuilder stringBuilder, DocumentAttribute [] aDocumentAttributes)
		{
			foreach (DocumentAttribute attribute in aDocumentAttributes)
			{
				stringBuilder.Append(attribute.Name);
				stringBuilder.Append(" (*");
				stringBuilder.Append(attribute.Extension);
				stringBuilder.Append(")|*");
				stringBuilder.Append(attribute.Extension);
				stringBuilder.Append("|");
			}
		}				

		private void LoadOpenFilterString(StringBuilder stringBuilder, DocumentAttribute [] aDocumentAttributes)
		{
			Hashtable mapNameToExtension = new Hashtable();

			BuildExtensionMap(mapNameToExtension, aDocumentAttributes);

			foreach (string sName in mapNameToExtension.Keys)
			{
				StringBuilder extensions = mapNameToExtension[sName] as StringBuilder;
				string sExtensions = extensions.ToString();

				stringBuilder.Append(sName);
				stringBuilder.Append(" (");
				stringBuilder.Append(sExtensions.Replace("|", ", "));
				stringBuilder.Append(")|");
				
				string [] asExtensions = sExtensions.Split(new char[] { '|' });

				for (int nExtension = 0 ; nExtension < asExtensions.Length; nExtension++)
				{
					if (nExtension > 0)
					{
						stringBuilder.Append(';');
					}

					stringBuilder.Append('*');
					stringBuilder.Append(asExtensions[nExtension]);
				}

				stringBuilder.Append("|");
			}			
		}

        //public string GetSaveFilter(MdiViewForm view)
        //{
        //    DocumentAttribute [] aDocumentAttributes = GetDocumentAttributes(view.Document.GetType());
			
        //    StringBuilder stringBuilder = new StringBuilder();
        //    LoadOpenFilterString(stringBuilder, aDocumentAttributes);

        //    stringBuilder.Append("All files (*.*)|*.*||");

        //    return stringBuilder.ToString();
        //}

		public string CreateFilter
		{
			get
			{
				return m_sCreateFilter;
			}
		}

		public string [] Extensions
		{
			get
			{
				ArrayList asExtensions = new ArrayList();

				foreach (string sExtension in m_mapExtensionToType.Keys)
				{
					asExtensions.Add(sExtension);
				}

				return asExtensions.ToArray(typeof(string)) as string [];
			}
		}
	}
}

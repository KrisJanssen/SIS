// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentTypes.cs" company="">
//   
// </copyright>
// <summary>
//   The document types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SIS.Data
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// The document types.
    /// </summary>
    public class DocumentTypes
    {
        // We want DocumentTypes to be a singleton. This way it will always be available and it will hold
        // all information we will ever need on the documents we will try to load. We can subsequently create
        // the singleton instance in our code and simply call its opendocument or any other methods.
        #region Static Fields

        /// <summary>
        /// The m_instance.
        /// </summary>
        private static volatile DocumentTypes m_instance;

        /// <summary>
        /// The m_sync root.
        /// </summary>
        private static object m_syncRoot = new object();

        #endregion

        #region Fields

        /// <summary>
        /// The m_map extension to type.
        /// </summary>
        private Hashtable m_mapExtensionToType = null;

        /// <summary>
        /// The m_s create filter.
        /// </summary>
        private string m_sCreateFilter = null;

        /// <summary>
        /// The m_s load filter.
        /// </summary>
        private string m_sLoadFilter = null;

        #endregion

        // The constructor obviously needs to be private to prevent normal instantiation.
        #region Constructors and Destructors

        /// <summary>
        /// Prevents a default instance of the <see cref="DocumentTypes"/> class from being created.
        /// </summary>
        private DocumentTypes()
        {
            this.Load();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
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

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                return this.m_mapExtensionToType.Count;
            }
        }

        /// <summary>
        /// Gets the create filter.
        /// </summary>
        public string CreateFilter
        {
            get
            {
                return this.m_sCreateFilter;
            }
        }

        /// <summary>
        /// Gets the extensions.
        /// </summary>
        public string[] Extensions
        {
            get
            {
                ArrayList asExtensions = new ArrayList();

                foreach (string sExtension in this.m_mapExtensionToType.Keys)
                {
                    asExtensions.Add(sExtension);
                }

                return asExtensions.ToArray(typeof(string)) as string[];
            }
        }

        /// <summary>
        /// Gets the open dialog filter.
        /// </summary>
        public string OpenDialogFilter
        {
            get
            {
                return this.m_sLoadFilter;
            }
        }

        #endregion

        // This method is obsolete.
        // public DataSet OpenDocument()
        // {
        // foreach (System.Type typeDocument in m_mapExtensionToType.Values)
        // {
        // DataSet document = typeDocument.GetConstructor(new System.Type[0]).Invoke(new object[0]) as DataSet;

        // if (document != null)
        // {
        // return document;
        // }
        // }

        // return null;
        // }
        #region Public Methods and Operators

        /// <summary>
        /// The create document.
        /// </summary>
        /// <param name="sFileName">
        /// The s file name.
        /// </param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        public DataSet CreateDocument(string sFileName)
        {
            DataSet document = this.CreateDocumentHelper(sFileName);

            // if (!document.SaveDocument(sFileName))
            // {
            // return null;
            // }
            return document;
        }

        /// <summary>
        /// The open document.
        /// </summary>
        /// <param name="sFileName">
        /// The s file name.
        /// </param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        public DataSet OpenDocument(string sFileName)
        {
            DataSet document = this.CreateDocumentHelper(sFileName);

            // if (document == null || !document.LoadDocument(sFileName, true))
            // {
            // return null;
            // }
            return document;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The build extension map.
        /// </summary>
        /// <param name="mapNameToExtension">
        /// The map name to extension.
        /// </param>
        /// <param name="aDocumentAttributes">
        /// The a document attributes.
        /// </param>
        private void BuildExtensionMap(Hashtable mapNameToExtension, DocumentAttribute[] aDocumentAttributes)
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

        /// <summary>
        /// The create document helper.
        /// </summary>
        /// <param name="sFileName">
        /// The s file name.
        /// </param>
        /// <returns>
        /// The <see cref="DataSet"/>.
        /// </returns>
        private DataSet CreateDocumentHelper(string sFileName)
        {
            int nLastDot = sFileName.LastIndexOf('.');

            if (nLastDot < 0)
            {
                return null;
            }

            string sExtension = sFileName.Substring(nLastDot);

            Type typeDocument = this.m_mapExtensionToType[sExtension] as System.Type;

            if (typeDocument == null)
            {
                return null;
            }

            return typeDocument.GetConstructor(new Type[0]).Invoke(new object[0]) as DataSet;
        }

        /// <summary>
        /// The get document attributes.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="DocumentAttribute[]"/>.
        /// </returns>
        private DocumentAttribute[] GetDocumentAttributes(Type type)
        {
            return type.GetCustomAttributes(typeof(DocumentAttribute), true) as DocumentAttribute[];
        }

        /// <summary>
        /// The load.
        /// </summary>
        private void Load()
        {
            Assembly assembly = System.Reflection.Assembly.GetCallingAssembly();
            this.m_mapExtensionToType = new Hashtable();

            if (assembly != null)
            {
                Type[] aTypes = assembly.GetTypes();

                StringBuilder stringBuilderFilter = new StringBuilder();
                StringBuilder stringBuilderCreateFilter = new StringBuilder();

                foreach (Type type in aTypes)
                {
                    DocumentAttribute[] aDocumentAttributes = this.GetDocumentAttributes(type);

                    if (aDocumentAttributes != null && aDocumentAttributes.Length > 0)
                    {
                        this.LoadAttributes(type, aDocumentAttributes);
                        this.LoadOpenFilterString(stringBuilderFilter, aDocumentAttributes);
                        this.LoadCreateFilterString(stringBuilderCreateFilter, aDocumentAttributes);
                    }
                }

                stringBuilderFilter.Append("All files (*.*)|*.*||");
                this.m_sLoadFilter = stringBuilderFilter.ToString();

                stringBuilderCreateFilter.Append("|");
                this.m_sCreateFilter = stringBuilderCreateFilter.ToString();
            }
        }

        /// <summary>
        /// The load attributes.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="aDocumentAttributes">
        /// The a document attributes.
        /// </param>
        private void LoadAttributes(Type type, DocumentAttribute[] aDocumentAttributes)
        {
            foreach (DocumentAttribute attribute in aDocumentAttributes)
            {
                this.m_mapExtensionToType.Add(attribute.Extension, type);
            }
        }

        /// <summary>
        /// The load create filter string.
        /// </summary>
        /// <param name="stringBuilder">
        /// The string builder.
        /// </param>
        /// <param name="aDocumentAttributes">
        /// The a document attributes.
        /// </param>
        private void LoadCreateFilterString(StringBuilder stringBuilder, DocumentAttribute[] aDocumentAttributes)
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

        /// <summary>
        /// The load open filter string.
        /// </summary>
        /// <param name="stringBuilder">
        /// The string builder.
        /// </param>
        /// <param name="aDocumentAttributes">
        /// The a document attributes.
        /// </param>
        private void LoadOpenFilterString(StringBuilder stringBuilder, DocumentAttribute[] aDocumentAttributes)
        {
            Hashtable mapNameToExtension = new Hashtable();

            this.BuildExtensionMap(mapNameToExtension, aDocumentAttributes);

            foreach (string sName in mapNameToExtension.Keys)
            {
                StringBuilder extensions = mapNameToExtension[sName] as StringBuilder;
                string sExtensions = extensions.ToString();

                stringBuilder.Append(sName);
                stringBuilder.Append(" (");
                stringBuilder.Append(sExtensions.Replace("|", ", "));
                stringBuilder.Append(")|");

                string[] asExtensions = sExtensions.Split(new[] { '|' });

                for (int nExtension = 0; nExtension < asExtensions.Length; nExtension++)
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

        #endregion

        // public string GetSaveFilter(MdiViewForm view)
        // {
        // DocumentAttribute [] aDocumentAttributes = GetDocumentAttributes(view.Document.GetType());

        // StringBuilder stringBuilder = new StringBuilder();
        // LoadOpenFilterString(stringBuilder, aDocumentAttributes);

        // stringBuilder.Append("All files (*.*)|*.*||");

        // return stringBuilder.ToString();
        // }
    }
}
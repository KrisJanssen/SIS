// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentTypes.cs" company="Kris Janssen">
//   Copyright (c) 2014 Kris Janssen
// </copyright>
// <summary>
//   The document types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SIS.MDITemplate
{
    using System;
    using System.Collections;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// The document types.
    /// </summary>
    internal class DocumentTypes
    {
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

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentTypes"/> class.
        /// </summary>
        public DocumentTypes()
        {
            this.Load();
        }

        #endregion

        #region Public Properties

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
        // public MdiDocument OpenDocument()
        // {
        // foreach (System.Type typeDocument in m_mapExtensionToType.Values)
        // {
        // MdiDocument document = typeDocument.GetConstructor(new System.Type[0]).Invoke(new object[0]) as MdiDocument;

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
        /// The <see cref="MdiDocument"/>.
        /// </returns>
        public MdiDocument CreateDocument(string sFileName)
        {
            MdiDocument document = this.CreateDocumentHelper(sFileName);

            if (!document.SaveDocument(sFileName))
            {
                return null;
            }

            return document;
        }

        /// <summary>
        /// The get save filter.
        /// </summary>
        /// <param name="view">
        /// The view.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetSaveFilter(MdiViewForm view)
        {
            DocumentAttribute[] aDocumentAttributes = this.GetDocumentAttributes(view.Document.GetType());

            StringBuilder stringBuilder = new StringBuilder();
            this.LoadOpenFilterString(stringBuilder, aDocumentAttributes);

            stringBuilder.Append("All files (*.*)|*.*||");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// The open document.
        /// </summary>
        /// <param name="sFileName">
        /// The s file name.
        /// </param>
        /// <returns>
        /// The <see cref="MdiDocument"/>.
        /// </returns>
        public MdiDocument OpenDocument(string sFileName)
        {
            MdiDocument document = this.CreateDocumentHelper(sFileName);

            if (document == null || !document.LoadDocument(sFileName, true))
            {
                return null;
            }

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
        /// The <see cref="MdiDocument"/>.
        /// </returns>
        private MdiDocument CreateDocumentHelper(string sFileName)
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

            return typeDocument.GetConstructor(new Type[0]).Invoke(new object[0]) as MdiDocument;
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
            Assembly assembly = System.Reflection.Assembly.GetEntryAssembly();
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
    }
}
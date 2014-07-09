// --------------------------------------------------------------------------------------------------------------------
// <copyright company="" file="ZedGraphControl.ContextMenu.cs">
//   
// </copyright>
// <summary>
//   The zed graph control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ZedGraph.ZedGraph
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    /// <summary>
    /// The zed graph control.
    /// </summary>
    partial class ZedGraphControl
    {
        // Revision: JCarpenter 10/06
        #region Enums

        /// <summary>
        /// Public enumeration that specifies the type of 
        /// object present at the Context Menu's mouse location
        /// </summary>
        public enum ContextMenuObjectState
        {
            /// <summary>
            /// The object is an Inactive Curve Item at the Context Menu's mouse position
            /// </summary>
            InactiveSelection, 

            /// <summary>
            /// The object is an active Curve Item at the Context Menu's mouse position
            /// </summary>
            ActiveSelection, 

            /// <summary>
            /// There is no selectable object present at the Context Menu's mouse position
            /// </summary>
            Background
        }

        #endregion

        // Revision: JCarpenter 10/06
        #region Public Methods and Operators

        /// <summary>
        /// Handler for the "Copy" context menu item.  Copies the current image to a bitmap on the
        /// clipboard.
        /// </summary>
        /// <param name="isShowMessage">
        /// boolean value that determines whether or not a prompt will be
        /// displayed.  true to show a message of "Image Copied to ClipBoard".
        /// </param>
        public void Copy(bool isShowMessage)
        {
            if (this._masterPane != null)
            {
                // Clipboard.SetDataObject( _masterPane.GetImage(), true );

                // Threaded copy mode to avoid crash with MTA
                // Contributed by Dave Moor
                Thread ct = new Thread(new ThreadStart(this.ClipboardCopyThread));

                // ct.ApartmentState = ApartmentState.STA;
                ct.SetApartmentState(ApartmentState.STA);
                ct.Start();
                ct.Join();

                if (isShowMessage)
                {
                    string str = this._resourceManager.GetString("copied_to_clip");

                    // MessageBox.Show( "Image Copied to ClipBoard" );
                    MessageBox.Show(str);
                }
            }
        }

        /// <summary>
        /// Special handler that copies the current image to an Emf file on the clipboard.
        /// </summary>
        /// <remarks>
        /// This version is similar to the regular <see cref="Copy"/> method, except that
        /// it will place an Emf image (vector) on the ClipBoard instead of the regular bitmap.
        /// </remarks>
        /// <param name="isShowMessage">
        /// boolean value that determines whether or not a prompt will be
        /// displayed.  true to show a message of "Image Copied to ClipBoard".
        /// </param>
        public void CopyEmf(bool isShowMessage)
        {
            if (this._masterPane != null)
            {
                // Threaded copy mode to avoid crash with MTA
                // Contributed by Dave Moor
                Thread ct = new Thread(new ThreadStart(this.ClipboardCopyThreadEmf));

                // ct.ApartmentState = ApartmentState.STA;
                ct.SetApartmentState(ApartmentState.STA);
                ct.Start();
                ct.Join();

                if (isShowMessage)
                {
                    string str = this._resourceManager.GetString("copied_to_clip");
                    MessageBox.Show(str);
                }
            }
        }

        /// <summary>
        /// Handler for the "Set Scale to Default" context menu item.  Sets the scale ranging to
        /// full auto mode for all axes.
        /// </summary>
        /// <remarks>
        /// This method differs from the <see cref="ZoomOutAll"/> method in that it sets the scales
        /// to full auto mode.  The <see cref="ZoomOutAll"/> method sets the scales to their initial
        /// setting prior to any user actions (which may or may not be full auto mode).
        /// </remarks>
        /// <param name="primaryPane">
        /// The <see cref="GraphPane"/> object which is to have the
        /// scale restored
        /// </param>
        public void RestoreScale(GraphPane primaryPane)
        {
            if (primaryPane != null)
            {
                // Go ahead and save the old zoomstates, which provides an "undo"-like capability
                // ZoomState oldState = primaryPane.ZoomStack.Push( primaryPane, ZoomState.StateType.Zoom );
                ZoomState oldState = new ZoomState(primaryPane, ZoomState.StateType.Zoom);

                using (Graphics g = this.CreateGraphics())
                {
                    if (this._isSynchronizeXAxes || this._isSynchronizeYAxes)
                    {
                        foreach (GraphPane pane in this._masterPane._paneList)
                        {
                            pane.ZoomStack.Push(pane, ZoomState.StateType.Zoom);
                            this.ResetAutoScale(pane, g);
                        }
                    }
                    else
                    {
                        primaryPane.ZoomStack.Push(primaryPane, ZoomState.StateType.Zoom);
                        this.ResetAutoScale(primaryPane, g);
                    }

                    // Provide Callback to notify the user of zoom events
                    if (this.ZoomEvent != null)
                    {
                        this.ZoomEvent(this, oldState, new ZoomState(primaryPane, ZoomState.StateType.Zoom));
                    }

                    // g.Dispose();
                }

                this.Refresh();
            }
        }

        /// <summary>
        /// Handler for the "Save Image As" context menu item.  Copies the current image to the selected
        /// file in either the Emf (vector), or a variety of Bitmap formats.
        /// </summary>
        /// <remarks>
        /// Note that <see cref="SaveAsBitmap" /> and <see cref="SaveAsEmf" /> methods are provided
        /// which allow for Bitmap-only or Emf-only handling of the "Save As" context menu item.
        /// </remarks>
        public void SaveAs()
        {
            this.SaveAs(null);
        }

        /// <summary>
        /// Copies the current image to the selected file in  
        /// Emf (vector), or a variety of Bitmap formats.
        /// </summary>
        /// <param name="DefaultFileName">
        /// Accepts a default file name for the file dialog (if "" or null, default is not used)
        /// </param>
        /// <returns>
        /// The file name saved, or "" if cancelled.
        /// </returns>
        /// <remarks>
        /// Note that <see cref="SaveAsBitmap"/> and <see cref="SaveAsEmf"/> methods are provided
        /// which allow for Bitmap-only or Emf-only handling of the "Save As" context menu item.
        /// </remarks>
        public string SaveAs(string DefaultFileName)
        {
            if (this._masterPane != null)
            {
                this._saveFileDialog.Filter = "Emf Format (*.emf)|*.emf|" + "PNG Format (*.png)|*.png|"
                                              + "Gif Format (*.gif)|*.gif|" + "Jpeg Format (*.jpg)|*.jpg|"
                                              + "Tiff Format (*.tif)|*.tif|" + "Bmp Format (*.bmp)|*.bmp";

                if (DefaultFileName != null && DefaultFileName.Length > 0)
                {
                    string ext = System.IO.Path.GetExtension(DefaultFileName).ToLower();
                    switch (ext)
                    {
                        case ".emf":
                            this._saveFileDialog.FilterIndex = 1;
                            break;
                        case ".png":
                            this._saveFileDialog.FilterIndex = 2;
                            break;
                        case ".gif":
                            this._saveFileDialog.FilterIndex = 3;
                            break;
                        case ".jpeg":
                        case ".jpg":
                            this._saveFileDialog.FilterIndex = 4;
                            break;
                        case ".tiff":
                        case ".tif":
                            this._saveFileDialog.FilterIndex = 5;
                            break;
                        case ".bmp":
                            this._saveFileDialog.FilterIndex = 6;
                            break;
                    }

                    // If we were passed a file name, not just an extension, use it
                    if (DefaultFileName.Length > ext.Length)
                    {
                        this._saveFileDialog.FileName = DefaultFileName;
                    }
                }

                if (this._saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Stream myStream = this._saveFileDialog.OpenFile();
                    if (myStream != null)
                    {
                        if (this._saveFileDialog.FilterIndex == 1)
                        {
                            myStream.Close();
                            this.SaveEmfFile(this._saveFileDialog.FileName);
                        }
                        else
                        {
                            ImageFormat format = ImageFormat.Png;
                            switch (this._saveFileDialog.FilterIndex)
                            {
                                case 2:
                                    format = ImageFormat.Png;
                                    break;
                                case 3:
                                    format = ImageFormat.Gif;
                                    break;
                                case 4:
                                    format = ImageFormat.Jpeg;
                                    break;
                                case 5:
                                    format = ImageFormat.Tiff;
                                    break;
                                case 6:
                                    format = ImageFormat.Bmp;
                                    break;
                            }

                            this.ImageRender().Save(myStream, format);

                            // _masterPane.GetImage().Save( myStream, format );
                            myStream.Close();
                        }

                        return this._saveFileDialog.FileName;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Handler for the "Save Image As" context menu item.  Copies the current image to the selected
        /// Bitmap file.
        /// </summary>
        /// <remarks>
        /// Note that this handler saves as a bitmap only.  The default handler is
        /// <see cref="SaveAs()" />, which allows for Bitmap or EMF formats
        /// </remarks>
        public void SaveAsBitmap()
        {
            if (this._masterPane != null)
            {
                this._saveFileDialog.Filter = "PNG Format (*.png)|*.png|" + "Gif Format (*.gif)|*.gif|"
                                              + "Jpeg Format (*.jpg)|*.jpg|" + "Tiff Format (*.tif)|*.tif|"
                                              + "Bmp Format (*.bmp)|*.bmp";

                if (this._saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ImageFormat format = ImageFormat.Png;
                    if (this._saveFileDialog.FilterIndex == 2)
                    {
                        format = ImageFormat.Gif;
                    }
                    else if (this._saveFileDialog.FilterIndex == 3)
                    {
                        format = ImageFormat.Jpeg;
                    }
                    else if (this._saveFileDialog.FilterIndex == 4)
                    {
                        format = ImageFormat.Tiff;
                    }
                    else if (this._saveFileDialog.FilterIndex == 5)
                    {
                        format = ImageFormat.Bmp;
                    }

                    Stream myStream = this._saveFileDialog.OpenFile();
                    if (myStream != null)
                    {
                        // _masterPane.GetImage().Save( myStream, format );
                        this.ImageRender().Save(myStream, format);
                        myStream.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Handler for the "Save Image As" context menu item.  Copies the current image to the selected
        /// Emf format file.
        /// </summary>
        /// <remarks>
        /// Note that this handler saves as an Emf format only.  The default handler is
        /// <see cref="SaveAs()" />, which allows for Bitmap or EMF formats.
        /// </remarks>
        public void SaveAsEmf()
        {
            if (this._masterPane != null)
            {
                this._saveFileDialog.Filter = "Emf Format (*.emf)|*.emf";

                if (this._saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Stream myStream = this._saveFileDialog.OpenFile();
                    if (myStream != null)
                    {
                        myStream.Close();

                        // _masterPane.GetMetafile().Save( _saveFileDialog.FileName );
                        this.SaveEmfFile(this._saveFileDialog.FileName);
                    }
                }
            }
        }

        /// <summary>
        /// Handler for the "UnZoom/UnPan" context menu item.  Restores the scale ranges to the values
        /// before the last zoom, pan, or scroll operation.
        /// </summary>
        /// <remarks>
        /// Triggers a <see cref="ZoomEvent"/> for any type of undo (including pan, scroll, zoom, and
        /// wheelzoom).  This method will affect all the
        /// <see cref="GraphPane"/> objects in the <see cref="MasterPane"/> if
        /// <see cref="IsSynchronizeXAxes"/> or <see cref="IsSynchronizeYAxes"/> is true.
        /// </remarks>
        /// <param name="primaryPane">
        /// The primary <see cref="GraphPane"/> object which is to be
        /// zoomed out
        /// </param>
        public void ZoomOut(GraphPane primaryPane)
        {
            if (primaryPane != null && !primaryPane.ZoomStack.IsEmpty)
            {
                ZoomState.StateType type = primaryPane.ZoomStack.Top.Type;

                ZoomState oldState = new ZoomState(primaryPane, type);
                ZoomState newState = null;
                if (this._isSynchronizeXAxes || this._isSynchronizeYAxes)
                {
                    foreach (GraphPane pane in this._masterPane._paneList)
                    {
                        ZoomState state = pane.ZoomStack.Pop(pane);
                        if (pane == primaryPane)
                        {
                            newState = state;
                        }
                    }
                }
                else
                {
                    newState = primaryPane.ZoomStack.Pop(primaryPane);
                }

                // Provide Callback to notify the user of zoom events
                if (this.ZoomEvent != null)
                {
                    this.ZoomEvent(this, oldState, newState);
                }

                this.Refresh();
            }
        }

        /// <summary>
        /// Handler for the "Undo All Zoom/Pan" context menu item.  Restores the scale ranges to the values
        /// before all zoom and pan operations
        /// </summary>
        /// <remarks>
        /// This method differs from the <see cref="RestoreScale"/> method in that it sets the scales
        /// to their initial setting prior to any user actions.  The <see cref="RestoreScale"/> method
        /// sets the scales to full auto mode (regardless of what the initial setting may have been).
        /// </remarks>
        /// <param name="primaryPane">
        /// The <see cref="GraphPane"/> object which is to be zoomed out
        /// </param>
        public void ZoomOutAll(GraphPane primaryPane)
        {
            if (primaryPane != null && !primaryPane.ZoomStack.IsEmpty)
            {
                ZoomState.StateType type = primaryPane.ZoomStack.Top.Type;

                ZoomState oldState = new ZoomState(primaryPane, type);

                // ZoomState newState = pane.ZoomStack.PopAll( pane );
                ZoomState newState = null;
                if (this._isSynchronizeXAxes || this._isSynchronizeYAxes)
                {
                    foreach (GraphPane pane in this._masterPane._paneList)
                    {
                        ZoomState state = pane.ZoomStack.PopAll(pane);
                        if (pane == primaryPane)
                        {
                            newState = state;
                        }
                    }
                }
                else
                {
                    newState = primaryPane.ZoomStack.PopAll(primaryPane);
                }

                // Provide Callback to notify the user of zoom events
                if (this.ZoomEvent != null)
                {
                    this.ZoomEvent(this, oldState, newState);
                }

                this.Refresh();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Save the current Graph to the specified filename in EMF (vector) format.
        /// See <see cref="SaveAsEmf()"/> for public access.
        /// </summary>
        /// <param name="fileName">
        /// The file Name.
        /// </param>
        /// <remarks>
        /// Note that this handler saves as an Emf format only.  The default handler is
        /// <see cref="SaveAs()"/>, which allows for Bitmap or EMF formats.
        /// </remarks>
        internal void SaveEmfFile(string fileName)
        {
            using (Graphics g = this.CreateGraphics())
            {
                IntPtr hdc = g.GetHdc();
                Metafile metaFile = new Metafile(hdc, EmfType.EmfPlusOnly);
                using (Graphics gMeta = Graphics.FromImage(metaFile))
                {
                    // PaneBase.SetAntiAliasMode( gMeta, IsAntiAlias );
                    // gMeta.CompositingMode = CompositingMode.SourceCopy; 
                    // gMeta.CompositingQuality = CompositingQuality.HighQuality;
                    // gMeta.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    // gMeta.SmoothingMode = SmoothingMode.AntiAlias;
                    // gMeta.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; 
                    this._masterPane.Draw(gMeta);

                    // gMeta.Dispose();
                }

                ClipboardMetafileHelper.SaveEnhMetafileToFile(metaFile, fileName);

                g.ReleaseHdc(hdc);

                // g.Dispose();
            }
        }

        /// <summary>
        /// Handler for the "Copy" context menu item.  Copies the current image to a bitmap on the
        /// clipboard.
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        protected void MenuClick_Copy(object sender, EventArgs e)
        {
            this.Copy(this._isShowCopyMessage);
        }

        /// <summary>
        /// Handler for the "Set Scale to Default" context menu item.  Sets the scale ranging to
        /// full auto mode for all axes.
        /// </summary>
        /// <remarks>
        /// This method differs from the <see cref="ZoomOutAll"/> method in that it sets the scales
        /// to full auto mode.  The <see cref="ZoomOutAll"/> method sets the scales to their initial
        /// setting prior to any user actions (which may or may not be full auto mode).
        /// </remarks>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        protected void MenuClick_RestoreScale(object sender, EventArgs e)
        {
            if (this._masterPane != null)
            {
                GraphPane pane = this._masterPane.FindPane(this._menuClickPt);
                this.RestoreScale(pane);
            }
        }

        /// <summary>
        /// Handler for the "Save Image As" context menu item.  Copies the current image to the selected
        /// file.
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        protected void MenuClick_SaveAs(object sender, EventArgs e)
        {
            this.SaveAs();
        }

        /// <summary>
        /// Handler for the "Show Values" context menu item.  Toggles the <see cref="IsShowPointValues"/>
        /// property, which activates the point value tooltips.
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        protected void MenuClick_ShowValues(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                this.IsShowPointValues = !item.Checked;
            }
        }

        /*
				public void RestoreScale( GraphPane primaryPane )
				{
					if ( primaryPane != null )
					{
						Graphics g = this.CreateGraphics();
						ZoomState oldState = new ZoomState( primaryPane, ZoomState.StateType.Zoom );
						//ZoomState newState = null;

						if ( _isSynchronizeXAxes || _isSynchronizeYAxes )
						{
							foreach ( GraphPane pane in _masterPane._paneList )
							{
								if ( pane == primaryPane )
								{
									pane.XAxis.ResetAutoScale( pane, g );
									foreach ( YAxis axis in pane.YAxisList )
										axis.ResetAutoScale( pane, g );
									foreach ( Y2Axis axis in pane.Y2AxisList )
										axis.ResetAutoScale( pane, g );
								}
							}
						}
						else
						{
							primaryPane.XAxis.ResetAutoScale( primaryPane, g );
							foreach ( YAxis axis in primaryPane.YAxisList )
								axis.ResetAutoScale( primaryPane, g );
							foreach ( Y2Axis axis in primaryPane.Y2AxisList )
								axis.ResetAutoScale( primaryPane, g );
						}

						// Provide Callback to notify the user of zoom events
						if ( this.ZoomEvent != null )
							this.ZoomEvent( this, oldState, new ZoomState( primaryPane, ZoomState.StateType.Zoom ) );

						g.Dispose();
						Refresh();
					}
				}
		*/
        /*
				public void ZoomOutAll( GraphPane primaryPane )
				{
					if ( primaryPane != null && !primaryPane.ZoomStack.IsEmpty )
					{
						ZoomState.StateType type = primaryPane.ZoomStack.Top.Type;

						ZoomState oldState = new ZoomState( primaryPane, type );
						//ZoomState newState = pane.ZoomStack.PopAll( pane );
						ZoomState newState = null;
						if ( _isSynchronizeXAxes || _isSynchronizeYAxes )
						{
							foreach ( GraphPane pane in _masterPane._paneList )
							{
								ZoomState state = pane.ZoomStack.PopAll( pane );
								if ( pane == primaryPane )
									newState = state;
							}
						}
						else
							newState = primaryPane.ZoomStack.PopAll( primaryPane );

						// Provide Callback to notify the user of zoom events
						if ( this.ZoomEvent != null )
							this.ZoomEvent( this, oldState, newState );

						Refresh();
					}
				}

		*/

        /// <summary>
        /// Handler for the "UnZoom/UnPan" context menu item.  Restores the scale ranges to the values
        /// before the last zoom or pan operation.
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        protected void MenuClick_ZoomOut(object sender, EventArgs e)
        {
            if (this._masterPane != null)
            {
                GraphPane pane = this._masterPane.FindPane(this._menuClickPt);
                this.ZoomOut(pane);
            }
        }

        /// <summary>
        /// Handler for the "Undo All Zoom/Pan" context menu item.  Restores the scale ranges to the values
        /// before all zoom and pan operations
        /// </summary>
        /// <remarks>
        /// This method differs from the <see cref="RestoreScale"/> method in that it sets the scales
        /// to their initial setting prior to any user actions.  The <see cref="RestoreScale"/> method
        /// sets the scales to full auto mode (regardless of what the initial setting may have been).
        /// </remarks>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        protected void MenuClick_ZoomOutAll(object sender, EventArgs e)
        {
            if (this._masterPane != null)
            {
                GraphPane pane = this._masterPane.FindPane(this._menuClickPt);
                this.ZoomOutAll(pane);
            }
        }

        /// <summary>
        /// A threaded version of the copy method to avoid crash with MTA
        /// </summary>
        private void ClipboardCopyThread()
        {
            Clipboard.SetDataObject(this.ImageRender(), true);
        }

        /// <summary>
        /// A threaded version of the copy method to avoid crash with MTA
        /// </summary>
        private void ClipboardCopyThreadEmf()
        {
            using (Graphics g = this.CreateGraphics())
            {
                IntPtr hdc = g.GetHdc();
                Metafile metaFile = new Metafile(hdc, EmfType.EmfPlusOnly);
                g.ReleaseHdc(hdc);

                using (Graphics gMeta = Graphics.FromImage(metaFile))
                {
                    this._masterPane.Draw(gMeta);
                }

                // IntPtr hMeta = metaFile.GetHenhmetafile();
                ClipboardMetafileHelper.PutEnhMetafileOnClipboard(this.Handle, metaFile);

                // System.Windows.Forms.Clipboard.SetDataObject(hMeta, true);

                // g.Dispose();
            }
        }

        /// <summary>
        /// Find the object currently under the mouse cursor, and return its state.
        /// </summary>
        /// <returns>
        /// The <see cref="ContextMenuObjectState"/>.
        /// </returns>
        private ContextMenuObjectState GetObjectState()
        {
            ContextMenuObjectState objState = ContextMenuObjectState.Background;

            // Determine object state
            Point mousePt = this.PointToClient(Control.MousePosition);
            int iPt;
            GraphPane pane;
            object nearestObj;

            using (Graphics g = this.CreateGraphics())
            {
                if (this.MasterPane.FindNearestPaneObject(mousePt, g, out pane, out nearestObj, out iPt))
                {
                    CurveItem item = nearestObj as CurveItem;

                    if (item != null && iPt >= 0)
                    {
                        if (item.IsSelected)
                        {
                            objState = ContextMenuObjectState.ActiveSelection;
                        }
                        else
                        {
                            objState = ContextMenuObjectState.InactiveSelection;
                        }
                    }
                }
            }

            return objState;
        }

        /// <summary>
        /// Setup for creation of a new image, applying appropriate anti-alias properties and
        /// returning the resultant image file
        /// </summary>
        /// <returns>
        /// The <see cref="Image"/>.
        /// </returns>
        private Image ImageRender()
        {
            return this._masterPane.GetImage(this._masterPane.IsAntiAlias);
        }

        /// <summary>
        /// The reset auto scale.
        /// </summary>
        /// <param name="pane">
        /// The pane.
        /// </param>
        /// <param name="g">
        /// The g.
        /// </param>
        private void ResetAutoScale(GraphPane pane, Graphics g)
        {
            pane.XAxis.ResetAutoScale(pane, g);
            pane.X2Axis.ResetAutoScale(pane, g);
            foreach (YAxis axis in pane.YAxisList)
            {
                axis.ResetAutoScale(pane, g);
            }

            foreach (Y2Axis axis in pane.Y2AxisList)
            {
                axis.ResetAutoScale(pane, g);
            }
        }

        /// <summary>
        /// protected method to handle the popup context menu in the <see cref="ZedGraphControl"/>.
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            // disable context menu by default
            e.Cancel = true;
            ContextMenuStrip menuStrip = sender as ContextMenuStrip;

            // Revision: JCarpenter 10/06
            ContextMenuObjectState objState = this.GetObjectState();

            if (this._masterPane != null && menuStrip != null)
            {
                menuStrip.Items.Clear();

                this._isZooming = false;
                this._isPanning = false;
                Cursor.Current = Cursors.Default;

                this._menuClickPt = this.PointToClient(Control.MousePosition);
                GraphPane pane = this._masterPane.FindPane(this._menuClickPt);

                if (this._isShowContextMenu)
                {
                    string menuStr = string.Empty;

                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Name = "copy";
                    item.Tag = "copy";
                    item.Text = this._resourceManager.GetString("copy");
                    item.Click += new System.EventHandler(this.MenuClick_Copy);
                    menuStrip.Items.Add(item);

                    item = new ToolStripMenuItem();
                    item.Name = "save_as";
                    item.Tag = "save_as";
                    item.Text = this._resourceManager.GetString("save_as");
                    item.Click += new System.EventHandler(this.MenuClick_SaveAs);
                    menuStrip.Items.Add(item);

                    item = new ToolStripMenuItem();
                    item.Name = "page_setup";
                    item.Tag = "page_setup";
                    item.Text = this._resourceManager.GetString("page_setup");
                    item.Click += new System.EventHandler(this.MenuClick_PageSetup);
                    menuStrip.Items.Add(item);

                    item = new ToolStripMenuItem();
                    item.Name = "print";
                    item.Tag = "print";
                    item.Text = this._resourceManager.GetString("print");
                    item.Click += new System.EventHandler(this.MenuClick_Print);
                    menuStrip.Items.Add(item);

                    item = new ToolStripMenuItem();
                    item.Name = "show_val";
                    item.Tag = "show_val";
                    item.Text = this._resourceManager.GetString("show_val");
                    item.Click += new System.EventHandler(this.MenuClick_ShowValues);
                    item.Checked = this.IsShowPointValues;
                    menuStrip.Items.Add(item);

                    item = new ToolStripMenuItem();
                    item.Name = "unzoom";
                    item.Tag = "unzoom";

                    if (pane == null || pane.ZoomStack.IsEmpty)
                    {
                        menuStr = this._resourceManager.GetString("unzoom");
                    }
                    else
                    {
                        switch (pane.ZoomStack.Top.Type)
                        {
                            case ZoomState.StateType.Zoom:
                            case ZoomState.StateType.WheelZoom:
                                menuStr = this._resourceManager.GetString("unzoom");
                                break;
                            case ZoomState.StateType.Pan:
                                menuStr = this._resourceManager.GetString("unpan");
                                break;
                            case ZoomState.StateType.Scroll:
                                menuStr = this._resourceManager.GetString("unscroll");
                                break;
                        }
                    }

                    // menuItem.Text = "Un-" + ( ( pane == null || pane.zoomStack.IsEmpty ) ?
                    // 	"Zoom" : pane.zoomStack.Top.TypeString );
                    item.Text = menuStr;
                    item.Click += new EventHandler(this.MenuClick_ZoomOut);
                    if (pane == null || pane.ZoomStack.IsEmpty)
                    {
                        item.Enabled = false;
                    }

                    menuStrip.Items.Add(item);

                    item = new ToolStripMenuItem();
                    item.Name = "undo_all";
                    item.Tag = "undo_all";
                    menuStr = this._resourceManager.GetString("undo_all");
                    item.Text = menuStr;
                    item.Click += new EventHandler(this.MenuClick_ZoomOutAll);
                    if (pane == null || pane.ZoomStack.IsEmpty)
                    {
                        item.Enabled = false;
                    }

                    menuStrip.Items.Add(item);

                    item = new ToolStripMenuItem();
                    item.Name = "set_default";
                    item.Tag = "set_default";
                    menuStr = this._resourceManager.GetString("set_default");
                    item.Text = menuStr;
                    item.Click += new EventHandler(this.MenuClick_RestoreScale);
                    if (pane == null)
                    {
                        item.Enabled = false;
                    }

                    menuStrip.Items.Add(item);

                    // if e.Cancel is set to false, the context menu does not display
                    // it is initially set to false because the context menu has no items
                    e.Cancel = false;

                    // Provide Callback for User to edit the context menu
                    // Revision: JCarpenter 10/06 - add ContextMenuObjectState objState
                    if (this.ContextMenuBuilder != null)
                    {
                        this.ContextMenuBuilder(this, menuStrip, this._menuClickPt, objState);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// The clipboard metafile helper.
        /// </summary>
        internal class ClipboardMetafileHelper
        {
            #region Methods

            /// <summary>
            /// The put enh metafile on clipboard.
            /// </summary>
            /// <param name="hWnd">
            /// The h wnd.
            /// </param>
            /// <param name="mf">
            /// The mf.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            internal static bool PutEnhMetafileOnClipboard(IntPtr hWnd, Metafile mf)
            {
                bool bResult = false;
                IntPtr hEMF, hEMF2;
                hEMF = mf.GetHenhmetafile(); // invalidates mf 
                if (!hEMF.Equals(new IntPtr(0)))
                {
                    hEMF2 = CopyEnhMetaFile(hEMF, null);
                    if (!hEMF2.Equals(new IntPtr(0)))
                    {
                        if (OpenClipboard(hWnd))
                        {
                            if (EmptyClipboard())
                            {
                                IntPtr hRes = SetClipboardData(14 /*CF_ENHMETAFILE*/, hEMF2);
                                bResult = hRes.Equals(hEMF2);
                                CloseClipboard();
                            }
                        }
                    }

                    DeleteEnhMetaFile(hEMF);
                }

                return bResult;
            }

            /// <summary>
            /// The save enh metafile to file.
            /// </summary>
            /// <param name="mf">
            /// The mf.
            /// </param>
            /// <param name="fileName">
            /// The file name.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            internal static bool SaveEnhMetafileToFile(Metafile mf, string fileName)
            {
                bool bResult = false;
                IntPtr hEMF;
                hEMF = mf.GetHenhmetafile(); // invalidates mf 
                if (!hEMF.Equals(new IntPtr(0)))
                {
                    StringBuilder tempName = new StringBuilder(fileName);
                    CopyEnhMetaFile(hEMF, tempName);
                    DeleteEnhMetaFile(hEMF);
                }

                return bResult;
            }

            /// <summary>
            /// The save enh metafile to file.
            /// </summary>
            /// <param name="mf">
            /// The mf.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            internal static bool SaveEnhMetafileToFile(Metafile mf)
            {
                bool bResult = false;
                IntPtr hEMF;
                hEMF = mf.GetHenhmetafile(); // invalidates mf 
                if (!hEMF.Equals(new IntPtr(0)))
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Extended Metafile (*.emf)|*.emf";
                    sfd.DefaultExt = ".emf";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder temp = new StringBuilder(sfd.FileName);
                        CopyEnhMetaFile(hEMF, temp);
                    }

                    DeleteEnhMetaFile(hEMF);
                }

                return bResult;
            }

            /// <summary>
            /// The close clipboard.
            /// </summary>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            [DllImport("user32.dll")]
            private static extern bool CloseClipboard();

            /// <summary>
            /// The copy enh meta file.
            /// </summary>
            /// <param name="hemfSrc">
            /// The hemf src.
            /// </param>
            /// <param name="hNULL">
            /// The h null.
            /// </param>
            /// <returns>
            /// The <see cref="IntPtr"/>.
            /// </returns>
            [DllImport("gdi32.dll")]
            private static extern IntPtr CopyEnhMetaFile(IntPtr hemfSrc, StringBuilder hNULL);

            /// <summary>
            /// The delete enh meta file.
            /// </summary>
            /// <param name="hemf">
            /// The hemf.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            [DllImport("gdi32.dll")]
            private static extern bool DeleteEnhMetaFile(IntPtr hemf);

            /// <summary>
            /// The empty clipboard.
            /// </summary>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            [DllImport("user32.dll")]
            private static extern bool EmptyClipboard();

            /// <summary>
            /// The open clipboard.
            /// </summary>
            /// <param name="hWndNewOwner">
            /// The h wnd new owner.
            /// </param>
            /// <returns>
            /// The <see cref="bool"/>.
            /// </returns>
            [DllImport("user32.dll")]
            private static extern bool OpenClipboard(IntPtr hWndNewOwner);

            /// <summary>
            /// The set clipboard data.
            /// </summary>
            /// <param name="uFormat">
            /// The u format.
            /// </param>
            /// <param name="hMem">
            /// The h mem.
            /// </param>
            /// <returns>
            /// The <see cref="IntPtr"/>.
            /// </returns>
            [DllImport("user32.dll")]
            private static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);

            #endregion
        }
    }
}
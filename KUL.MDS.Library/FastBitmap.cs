namespace SIS.Library 
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;

    public unsafe class FastBitmap 
    { 
        public struct PixelData 
        { 
            public byte blue; 
            public byte green; 
            public byte red; 
        } 

        Bitmap Subject; 
        int SubjectWidth; 
        BitmapData bitmapData = null; 
        Byte* pBase = null; 
        
        public FastBitmap(Bitmap SubjectBitmap) 
        { 
            this.Subject = SubjectBitmap; 
            
            try 
            { 
                this.LockBitmap(); 
            } 
            
            catch (Exception ex) 
            { 
                throw ex; 
            } 
        } 
        
        public void Release() 
        { 
            try 
            { 
                this.UnlockBitmap(); 
            } 
            
            catch (Exception ex) 
            { 
                throw ex; 
            } 
        } 
        
        public Bitmap Bitmap 
        { 
            get 
            { 
                return this.Subject; 
            } 
        } 
        
        public void SetPixel(int X, int Y, Color Colour) 
        { 
            try 
            { 
                PixelData* p = this.PixelAt(X, Y); 
                p->red = Colour.R; 
                p->green = Colour.G; 
                p->blue = Colour.B; 
            } 
            
            catch (AccessViolationException ave) 
            { 
                throw (ave); 
            } 
            
            catch (Exception ex) 
            { 
                throw ex; 
            } 
        } 
        
        public Color GetPixel(int X, int Y) 
        { 
            try 
            { 
                PixelData* p = this.PixelAt(X, Y); 
                return Color.FromArgb((int)p->red, (int)p->green, (int)p->blue); 
            } 
            
            catch (AccessViolationException ave) 
            { 
                throw (ave); 
            } 
            
            catch (Exception ex) 
            { 
                throw ex; 
            } 
        } 
        
        private void LockBitmap() 
        { 
            GraphicsUnit unit = GraphicsUnit.Pixel; 
            RectangleF boundsF = this.Subject.GetBounds(ref unit); 
            Rectangle bounds = new Rectangle((int)boundsF.X, (int)boundsF.Y, (int)boundsF.Width, (int)boundsF.Height); 
            this.SubjectWidth = (int)boundsF.Width * sizeof(PixelData); 
            
            if (this.SubjectWidth % 4 != 0) 
            { 
                this.SubjectWidth = 4 * (this.SubjectWidth / 4 + 1); 
            } 
            
            this.bitmapData = this.Subject.LockBits(bounds, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb); 
            this.pBase = (Byte*)this.bitmapData.Scan0.ToPointer(); 
        } 
        
        private PixelData* PixelAt(int x, int y) 
        { 
            return (PixelData*)(this.pBase + y * this.SubjectWidth + x * sizeof(PixelData)); 
        } 
        
        private void UnlockBitmap() 
        { 
            this.Subject.UnlockBits(this.bitmapData); 
            this.bitmapData = null; 
            this.pBase = null; 
        } 
    } 
}
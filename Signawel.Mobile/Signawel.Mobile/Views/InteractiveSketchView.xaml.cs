using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Signawel.Mobile.ViewModels;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Signawel.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InteractiveSketchView : ContentPage
    {
        private SKBitmap _sketchBitmap;
        private IList<SKPath> _paths; 
        public InteractiveSketchView()
        {
            InitializeComponent();

            _paths = new List<SKPath>();
        }

        private void InteractiveSketchViewCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKCanvas canvas = e.Surface.Canvas;
            canvas.Clear();

            ReconstructBitmap(((InteractiveSketchViewModel)BindingContext).ImageUrlBytes);
            var points = ((InteractiveSketchViewModel) BindingContext).Points;
            _paths = CalculatePaths(info.Height, info.Width, points);

            var paint = CreateSkPaint();

            canvas.DrawBitmap(_sketchBitmap, info.Rect);

            foreach (var path in _paths)
            {
                canvas.DrawPath(path, paint);
            }
        }

        private IList<SKPath> CalculatePaths(float canvasHeight, float canvasWidth, IList<List<Point>> points)
        {
            var paths = new List<SKPath>();

            foreach (var dataBasePointsList in points)
            {
                var pathPointsList = new List<SKPoint>();
                foreach (var point in dataBasePointsList)
                {
                    var recalculatedPoint = new SKPoint
                    {
                        X = (float)point.X * canvasWidth,
                        Y = (float)point.Y * canvasHeight
                    };
                    pathPointsList.Add(recalculatedPoint);
                }

                var calculatedPath = new SKPath();
                calculatedPath.AddPoly(pathPointsList.ToArray());
                paths.Add(calculatedPath);
            } 

            return paths;
        }

        private void ReconstructBitmap(byte[] imageBytes)
        {
            var stream = new MemoryStream(imageBytes);
            _sketchBitmap = SKBitmap.Decode(stream);
            SetCanvasHeightToBitmapHeight();
        }

        private void SetCanvasHeightToBitmapHeight()
        {
            InteractiveSketchSkCanvasView.HeightRequest = _sketchBitmap.Height;
        }

        private SKPaint CreateSkPaint()
        {
            return new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = new SKColor(255, 0, 0),
                StrokeWidth = 5,
                StrokeCap = SKStrokeCap.Round
            };
        }

        private void InteractiveSketchSkCanvasView_OnTouch(object sender, SKTouchEventArgs e)
        {
            if (e.ActionType == SKTouchAction.Pressed)
            {
                foreach (var path in _paths)
                {
                    if (path.Contains(e.Location.X, e.Location.Y))
                    {
                        
                    }
                }
            }
        }
    }
}
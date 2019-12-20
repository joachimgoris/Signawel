using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Signawel.Dto.BBox;
using Signawel.Dto.RoadworkSchema;
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
        private bool _loaded;
        private SKBitmap _sketchBitmap;
        private Dictionary<BBoxResponseDto, SKPath> _paths; 
        public InteractiveSketchView()
        {
            InitializeComponent();

            _paths = new Dictionary<BBoxResponseDto, SKPath>();
            _loaded = false;
        }

        public void SetupLoadedEvent()
        {
            ((InteractiveSketchViewModel) BindingContext).OnLoaded += new EventHandler(Loaded);
        }

        private void Loaded(object sender, EventArgs e)
        {
            _loaded = true;
            InteractiveSketchSkCanvasView.InvalidateSurface();
        }

        private void InteractiveSketchViewCanvasView_OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if(!_loaded)
                return;

            SKImageInfo info = e.Info;
            SKCanvas canvas = e.Surface.Canvas;

            canvas.Clear();

            ReconstructBitmap(((InteractiveSketchViewModel)BindingContext).ImageUrlBytes);
            var schema = ((InteractiveSketchViewModel) BindingContext).Schema;
            _paths = CalculatePaths(info.Height, info.Width, schema);

            var paint = CreateSkPaint();

            canvas.DrawBitmap(_sketchBitmap, info.Rect);

            foreach (var path in _paths)
            {
                canvas.DrawPath(path.Value, paint);
            }
        }

        private Dictionary<BBoxResponseDto, SKPath> CalculatePaths(float canvasHeight, float canvasWidth, RoadworkSchemaResponseDto schema)
        {
            var paths = new Dictionary<BBoxResponseDto, SKPath>();

            foreach (var bbox in schema.BoundingBoxes)
            {
                var pathPointsList = new List<SKPoint>();
                foreach (var point in bbox.Points.OrderBy(p => p.Order))
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
                paths.Add(bbox, calculatedPath);
            } 

            return paths;
        }

        private void ReconstructBitmap(byte[] imageBytes)
        {
            if(imageBytes == null)
            {
                // TODO navigate back?
                return;
            }

            var stream = new MemoryStream(imageBytes);
            _sketchBitmap = SKBitmap.Decode(stream);
            ScaleSketchCanvasToImage();
        }

        private void ScaleSketchCanvasToImage()
        {
            double scalePercentage;
            if(_sketchBitmap.Width < InteractiveSketchSkCanvasView.Width)
            {
                scalePercentage = _sketchBitmap.Width / InteractiveSketchSkCanvasView.Width;
            }
            else
            {
                scalePercentage = 1 - (InteractiveSketchSkCanvasView.Width / _sketchBitmap.Width);
            }

            InteractiveSketchSkCanvasView.HeightRequest = _sketchBitmap.Height * scalePercentage;
        }

        private SKPaint CreateSkPaint()
        {
            return new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = new SKColor(255, 0, 0, 0),
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
                    if (path.Value.Contains(e.Location.X, e.Location.Y))
                    {
                        ((InteractiveSketchViewModel)BindingContext).SelectedBoudingBoxCommand.Execute(path.Key.Id);
                    }
                }
            }
        }
    }
}
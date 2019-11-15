using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;

namespace Signawel.Mobile.ViewModels
{
    public class InteractiveSketchViewModel : ViewModelBase
    {
        private IList<List<Point>> _points;
        private byte[] _imageUrlBytes;

        public IList<List<Point>> Points
        {
            get => _points;
            set
            {
                if(_points == value) return;
                _points = value;
            }
        }

        public byte[] ImageUrlBytes
        {
            get => _imageUrlBytes;
            set
            {
                if (_imageUrlBytes == value) return;
                _imageUrlBytes = value;
            }
        }

        public InteractiveSketchViewModel()
        {
            _points = DummyPoints();
            _imageUrlBytes = RetrieveBitmap("https://upload.wikimedia.org/wikipedia/commons/e/e0/Long_March_2D_launching_VRSS-1.jpg");
        }

        private byte[] RetrieveBitmap(string url)
        {
            var httpClient = new HttpClient();
            var bytes = httpClient.GetByteArrayAsync(url).Result;

            return bytes;
        }

        private IList<List<Point>> DummyPoints()
        {
            return new List<List<Point>>
            {
                new List<Point>
                {
                    new Point(0.1, 0.1),
                    new Point(0.5,0.5),
                    new Point(0.1,0.5) 
                },
                new List<Point>
                {
                    new Point(0.5, 0.1),
                    new Point(0.5, 0.5),
                    new Point(0.8, 0.4),
                    new Point(0.7, 0.2)
                }
            };
        }
    }
}

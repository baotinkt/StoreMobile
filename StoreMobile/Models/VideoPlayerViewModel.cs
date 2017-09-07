using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMobile.Models
{
    class VideoPlayerViewModel
    {
        public VideoPlayerViewModel()
        {
            Videos = new ObservableCollection<VideoItem>();

            Videos.Add(new VideoItem { Title = "Big Buck Bunny", PlaybackUrl = "https://www.youtube.com/watch?v=r2BFJUJfRHU" });
            Videos.Add(new VideoItem
            {
                Title = "Dash MSE Test - Car",
                PlaybackUrl = "http://yt-dash-mse-test.commondatastorage.googleapis.com/media/car-20120827-87.mp4"
            });
            SelectedVideo = Videos.First();
        }

        public ICollection<VideoItem> Videos { get; set; }
        public VideoItem SelectedVideo { get; set; }
    }

    public class VideoItem
    {
        public string Title { get; set; }
        public string PlaybackUrl { get; set; }
    }
}


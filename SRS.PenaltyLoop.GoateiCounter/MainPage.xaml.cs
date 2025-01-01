using CommunityToolkit.Maui.Views;

namespace SRS.PenaltyLoop.GoateiCounter
{
    public partial class MainPage : ContentPage
    {
        private int _goasseiCounterValue = 0;
        private int _goasseiImageIndex = 1;

        public MainPage()
        {
            InitializeComponent();
            Button_NewGoasseiOrdered.Clicked += async (sender, args) => await NewGoasseiOrdered();
        }

        private async Task NewGoasseiOrdered()
        {
            _goasseiCounterValue++;
            _goasseiImageIndex++;
            if (_goasseiImageIndex > 6) _goasseiImageIndex = 1;

            // Animation für Zähler Vergrößern 
            await AnimationBefore();

            // Video abspielen
            var path = Path.GetFullPath($".\\goassei_{_goasseiImageIndex}_vid.mp4");
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                await AfterVideoPlayback();
                return;
            }

            VideoPlayer.Source = MediaSource.FromFile(path);
            VideoPlayer.IsVisible = true;
            VideoPlayer.Play();
        }

        private async Task AnimationBefore()
        {
            var centerX = (Image_Background.Width / 2) - (Label_Counter.DesiredSize.Width / 2);
            var centerY = (Image_Background.Height / 2) - (Label_Counter.DesiredSize.Height / 2);

            await Task.WhenAll
            (
                Label_Counter.TranslateTo(centerX, centerY, 500),
                Label_Counter.RotateXTo(360, 1)
            );
            await Task.Delay(100);

            Label_Counter.Text = _goasseiCounterValue.ToString();
            await Task.Delay(75);

            var maxScale = (Height / Label_Counter.DesiredSize.Height) + 1;
            for (var jumps = 0; jumps < 5; jumps++)
            {
                await Label_Counter.ScaleTo(maxScale, 250);
                await Task.Delay(75);
                await Label_Counter.ScaleTo(1, 250);
            }
        }

        private async Task AnimationAfter()
        {
            var startX = Image_Background.Width - (Image_Background.Width / 5) - (Label_Counter.DesiredSize.Width / 2);
            var startY = (Image_Background.Height / 2) - (Label_Counter.DesiredSize.Height / 2);
            await Task.WhenAll
            (
                Label_Counter.TranslateTo(startX, startY, 500),
                Label_Counter.RotateXTo(-360, 1)
            );
        }

        private void VideoPlayer_MediaEnded(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(async () => await AfterVideoPlayback());
        }

        private async Task AfterVideoPlayback()
        {
            Image_Background.Source = $"goassei_{_goasseiImageIndex}_bg.png";
            VideoPlayer.IsVisible = false;
            VideoPlayer.Source = null;

            await AnimationAfter();
        }
    }
}

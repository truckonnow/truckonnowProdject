using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Media;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Java.Interop;
using MDispatch.Droid.NewrRender.RebderVideoCamera;
using MDispatch.NewElement.CustomVideoCam;
using Xamarin.Forms.Platform.Android;
using static Android.Hardware.Camera;
using static Android.Views.TextureView;

[assembly: Xamarin.Forms.ExportRenderer(typeof(VideoCameraPage), typeof(VideoRecorderRenderer))]
namespace MDispatch.Droid.NewrRender.RebderVideoCamera
{
    class VideoRecorderRenderer : PageRenderer, ISurfaceTextureListener, IAutoFocusCallback
    {
        Android.Widget.RelativeLayout mainLayout;
        TextureView liveView;
        Button capturePhotoButton;
        [Obsolete]
        Android.Hardware.Camera camera;
        MediaRecorder recorder;
        Activity Activity => this.Context as Activity;
        private bool isFocus = false;

        public VideoRecorderRenderer(Context context) : base(context)
        {
        }

        protected override async void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);
            (Element as VideoCameraPage).IsPreviewing = false;
            (Element as VideoCameraPage).IsRecording = false;
            SetupUserInterface();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            capturePhotoButton.Click += async (sender, e) =>
            {
                AutoFocus();
            };
            liveView.SurfaceTextureListener = this;
        }

        [Obsolete]
        private async void AutoFocus()
        {
            if ((Element as VideoCameraPage).IsRecording)
            {
                await StopRecorddVideo();
            }
            else
            {
                if (!isFocus)
                {
                    isFocus = true;
                    await Task.Run(() =>
                    {
                        camera.AutoFocus(this);
                    });
                }
            }
        }

        private void SetupUserInterface()
        {
            mainLayout = new Android.Widget.RelativeLayout(Context);
            liveView = new TextureView(Context);

            Android.Widget.RelativeLayout.LayoutParams liveViewParams = new Android.Widget.RelativeLayout.LayoutParams(
                LayoutParams.MatchParent,
                LayoutParams.MatchParent);
            liveView.LayoutParameters = liveViewParams;
            mainLayout.AddView(liveView);

            capturePhotoButton = new Button(Context);
            Android.Widget.RelativeLayout.LayoutParams captureButtonParams = new Android.Widget.RelativeLayout.LayoutParams(
                LayoutParams.WrapContent,
                LayoutParams.WrapContent);
            captureButtonParams.Height = 120;
            captureButtonParams.Width = 120;
            capturePhotoButton.LayoutParameters = captureButtonParams;
            mainLayout.AddView(capturePhotoButton);
            AddView(mainLayout);
        }

        [Obsolete]
        private void StopCamera()
        {
            if ((Element as VideoCameraPage).IsPreviewing)
            {
                camera.StopPreview();
                camera.Release();
                (Element as VideoCameraPage).IsPreviewing = false;
            }
        }

        [Obsolete]
        private void StartCamera()
        {
            if (!(Element as VideoCameraPage).IsPreviewing)
            {
                if (mainLayout.Width < mainLayout.Height)
                {
                    camera.SetDisplayOrientation(90);
                }
                else
                {
                    camera.SetDisplayOrientation(0);
                }
                camera.StartPreview();
                (Element as VideoCameraPage).IsPreviewing = true;
            }
        }

        public bool GetOrientation()
        {
            IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            var rotation = windowManager.DefaultDisplay.Rotation;
            return rotation == SurfaceOrientation.Rotation0 || rotation == SurfaceOrientation.Rotation180;
        }

        [Obsolete]
        public async void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
            if (ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.Camera) != Permission.Granted || ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.RecordAudio) != Permission.Granted || ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
            {
                if (ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.Camera) != Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(Activity, new string[] { Manifest.Permission.Camera }, 50);
                }
                if (ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(Activity, new string[] { Manifest.Permission.WriteExternalStorage }, 50);
                }
                if (ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.RecordAudio) != Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(Activity, new string[] { Manifest.Permission.RecordAudio }, 50);
                }
                await (Element as VideoCameraPage).Navigation.PopAsync();
            }
            else
            {
                decimal aspect = 0;
                camera = Android.Hardware.Camera.Open();
                var parameters = camera.GetParameters();
                if (GetOrientation())
                {
                    aspect = ((decimal)height) / ((decimal)width);
                }
                else
                {
                    aspect = ((decimal)width) / ((decimal)height);
                }
                var previewSize = parameters.SupportedPreviewSizes
                                            .OrderBy(s => System.Math.Abs(s.Width / (decimal)s.Height - aspect))
                                            .First();
                parameters.SetPreviewSize(previewSize.Width, previewSize.Height);
                camera.SetParameters(parameters);
                camera.SetPreviewTexture(surface);
                StartCamera();
            }
        }

        [Obsolete]
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            if (!changed)
                return;
            try
            {
                var msw1 = MeasureSpec.MakeMeasureSpec(r, MeasureSpecMode.Exactly);
                var msh1 = MeasureSpec.MakeMeasureSpec(b, MeasureSpecMode.Exactly);
                mainLayout.Measure(msw1, msh1);
                mainLayout.Layout(l, t, r, b);
                if (mainLayout.Width < mainLayout.Height)
                {
                    if (camera != null)
                    {
                        camera.SetDisplayOrientation(90);

                    }
                    int tmpPr = (mainLayout.Width / 100) * 10;
                    capturePhotoButton.SetX(mainLayout.Width / 2 + tmpPr);
                    capturePhotoButton.SetY(mainLayout.Height - 200);
                }
                else
                {
                    if (camera != null)
                    {
                        camera.SetDisplayOrientation(0);
                    }
                    int tmpPr = (mainLayout.Width / 100) * 10;
                    capturePhotoButton.SetY(mainLayout.Height / 2 + tmpPr);
                    capturePhotoButton.SetX(mainLayout.Width - 200);
                }
            }
            catch (NullReferenceException)
            { }
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface)
        {
            if (camera != null)
            {
                StopCamera();
            }
            return true;
        }

        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
        }

        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
        }

        public async void OnAutoFocus(bool success, Android.Hardware.Camera camera)
        {
            if (success)
            {
                await RecordVideo();
            }
            else
            {

            }
            isFocus = false;
        }

        [Obsolete]
        private async Task RecordVideo()
        {
            if (!(Element as VideoCameraPage).IsRecording)
            {
                capturePhotoButton.Background.SetColorFilter(Android.Graphics.Color.ParseColor("#FF0040"), PorterDuff.Mode.Add);
                string filepath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string filename = System.IO.Path.Combine(filepath, "video.mp4");
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
                recorder = new MediaRecorder();
                camera.Unlock();
                recorder.SetCamera(camera);
                recorder.SetVideoSource(VideoSource.Camera);
                recorder.SetAudioSource(AudioSource.Mic);
                recorder.SetProfile(CamcorderProfile.Get(0, CamcorderQuality.High));
                //recorder.SetVideoEncoder(VideoEncoder.Default);
                //recorder.SetAudioEncoder(AudioEncoder.Default);
                //recorder.SetOutputFormat(OutputFormat.Mpeg4);
                recorder.SetOutputFile(filename);
                recorder.Prepare();
                recorder.Start();
                (Element as VideoCameraPage).IsRecording = true;
            }
        }

        private async Task StopRecorddVideo()
        {
            if ((Element as VideoCameraPage).IsRecording)
            {
                capturePhotoButton.Background.SetColorFilter(Android.Graphics.Color.ParseColor("#FAFAFA"), PorterDuff.Mode.Add);
                recorder.Stop();
                recorder.Release();
                (Element as VideoCameraPage).IsRecording = false;
                string filepath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string filename = System.IO.Path.Combine(filepath, "video.mp4");
                if (File.Exists(filename))
                {
                    byte[] bytes = File.ReadAllBytes(filename);
                    (Element as VideoCameraPage).SetPhotoResult(bytes);
                    File.Delete(filename);
                }
            }
        }
    }
}
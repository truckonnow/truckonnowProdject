using System;
using System.Linq;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MDispatch.Droid.NewrRender;
using MDispatch.Droid.NewrRender.NewElementXamarin.Forms;
using MDispatch.NewElement;
using Xamarin.Forms.Platform.Android;
using static Android.Hardware.Camera;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CameraPage), typeof(CameraPageRender))]
namespace MDispatch.Droid.NewrRender
{
    [Obsolete]
    class CameraPageRender : PageRenderer, TextureView.ISurfaceTextureListener, IAutoFocusCallback
    {
        bool isPermissions = false;

        public CameraPageRender(Context context) : base(context)
        { }

        RelativeLayout mainLayout;
        TextureView liveView;
        PaintCodeButton capturePhotoButton;
        [Obsolete]
        Android.Hardware.Camera camera;

        Activity Activity => this.Context as Activity;

        protected override async void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);
            SetupUserInterface();
            SetupEventHandlers();
        }

        void SetupUserInterface()
        {
            mainLayout = new RelativeLayout(Context);
            liveView = new TextureView(Context);

            RelativeLayout.LayoutParams liveViewParams = new RelativeLayout.LayoutParams(
                RelativeLayout.LayoutParams.MatchParent,
                RelativeLayout.LayoutParams.MatchParent);
            liveView.LayoutParameters = liveViewParams;
            mainLayout.AddView(liveView);

            capturePhotoButton = new PaintCodeButton(Context);
            RelativeLayout.LayoutParams captureButtonParams = new RelativeLayout.LayoutParams(
                RelativeLayout.LayoutParams.WrapContent,
                RelativeLayout.LayoutParams.WrapContent);
            captureButtonParams.Height = 120;
            captureButtonParams.Width = 120;
            capturePhotoButton.LayoutParameters = captureButtonParams;
            mainLayout.AddView(capturePhotoButton);

            AddView(mainLayout);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            if (!changed)
                return;
            try
            {
                var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
                var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);
                mainLayout.Measure(msw, msh);
                mainLayout.Layout(0, 0, r - l, b - t);
                if (mainLayout.Width < mainLayout.Height)
                {
                    if (camera != null)
                    {
                        try
                        {
                            camera.SetDisplayOrientation(90);
                        }
                        catch(RuntimeException)
                        { }
                    }
                    capturePhotoButton.SetX(mainLayout.Width / 2 - 60);
                    capturePhotoButton.SetY(mainLayout.Height - 200);
                }
                else
                {
                    if (camera != null)
                    {
                        camera.SetDisplayOrientation(0);
                    }
                    capturePhotoButton.SetY(mainLayout.Height / 2 - 60);
                    capturePhotoButton.SetX(mainLayout.Width - 200);
                }
            }
            catch(NullReferenceException)
            { }
        }

        public void SetupEventHandlers()
        {
            capturePhotoButton.Click += async (sender, e) =>
            {
                AutoFocus();
            };
            liveView.SurfaceTextureListener = this;
        }

        private async void AutoFocus()
        {
            await Task.Run(() =>
            {
                camera.AutoFocus(this);
            });
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                (Element as CameraPage).Cancel();
                return false;
            }
            return base.OnKeyDown(keyCode, e);
        }

        public async Task<byte[]> TakePhoto()
        {
            camera.StopPreview();
            var ratio = ((decimal)Height) / Width;
            var image = Bitmap.CreateBitmap(liveView.Bitmap, 0, 0, liveView.Bitmap.Width, (int)(liveView.Bitmap.Width * ratio));
            byte[] imageBytes = null;
            using (var imageStream = new System.IO.MemoryStream())
            {
                await image.CompressAsync(Bitmap.CompressFormat.Jpeg, 70, imageStream);
                image.Recycle();
                imageBytes = imageStream.ToArray();
            }
            camera.StartPreview();
            return imageBytes;
        }

        private void StopCamera()
        {
            camera.StopPreview();
            camera.Release();
        }

        private void StartCamera()
        {
            if (mainLayout.Width < mainLayout.Height)
            {
                camera.SetDisplayOrientation(90);
                camera.StartPreview();
            }
            else
            {
                camera.SetDisplayOrientation(0);
                camera.StartPreview();
            }
        }


        #region TextureView.ISurfaceTextureListener implementations

        [Obsolete]
        public void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
            if (ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.Camera) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(Activity, new string[] { Manifest.Permission.Camera }, 50);
                (Element as CameraPage).Cancel();
            }
            else
            {
                camera = Android.Hardware.Camera.Open();
                var parameters = camera.GetParameters();
                var aspect = ((decimal)height) / ((decimal)width);
                var previewSize = parameters.SupportedPreviewSizes
                                            .OrderBy(s => System.Math.Abs(s.Width / (decimal)s.Height - aspect))
                                            .First();
                System.Diagnostics.Debug.WriteLine($"Preview sizes: {parameters.SupportedPreviewSizes.Count}");
                parameters.SetPreviewSize(previewSize.Width, previewSize.Height);
                camera.SetParameters(parameters);
                camera.SetPreviewTexture(surface);
                StartCamera();
            }
        }

       
        public bool OnSurfaceTextureDestroyed(Android.Graphics.SurfaceTexture surface)
        {
            if (camera != null)
            {
                StopCamera();
            }
            return true;
        }

        public void OnSurfaceTextureSizeChanged(Android.Graphics.SurfaceTexture surface, int width, int height)
        {
        }

        public void OnSurfaceTextureUpdated(Android.Graphics.SurfaceTexture surface)
        {

        }

        [Obsolete]
        public async void OnAutoFocus(bool success, Android.Hardware.Camera camera)
        {
            var bytes = await TakePhoto();
            (Element as CameraPage).SetPhotoResult(bytes, liveView.Bitmap.Width, liveView.Bitmap.Height);
        }
        #endregion
    }
}
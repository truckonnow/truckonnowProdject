using System;
using System.Linq;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
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
        private bool isFocus = false;
        public CameraPageRender(Context context) : base(context)
        {
        }

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
            catch (NullReferenceException)
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
            if (!isFocus)
            {
                isFocus = true;
                await Task.Run(() =>
                {
                    camera.AutoFocus(this);
                });
            }
            else
            {

            }

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
                await image.CompressAsync(Bitmap.CompressFormat.Jpeg, 60, imageStream);
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

        public bool GetOrientation()
        {
            IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            var rotation = windowManager.DefaultDisplay.Rotation;
            return rotation == SurfaceOrientation.Rotation0 || rotation == SurfaceOrientation.Rotation180;
        }

        [Obsolete]
        public async void OnSurfaceTextureAvailable(SurfaceTexture surface, int width, int height)
        {
            if (ContextCompat.CheckSelfPermission(Activity, Manifest.Permission.Camera) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(Activity, new string[] { Manifest.Permission.Camera }, 50);
                await (Element as CameraPage).Navigation.PopAsync();
                (Element as CameraPage).Cancel();
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
            if (success)
            {
                var bytes = await TakePhoto();
                (Element as CameraPage).SetPhotoResult(bytes, liveView.Bitmap.Width, liveView.Bitmap.Height);
            }
            else
            {

            }
            isFocus = false;
        }
        #endregion
    }
}
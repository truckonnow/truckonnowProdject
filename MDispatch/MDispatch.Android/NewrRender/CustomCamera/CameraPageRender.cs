using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using MDispatch.Droid.NewrRender;
using MDispatch.NewElement;
using Xamarin.Forms.Platform.Android;
using static Android.Hardware.Camera;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CameraPage), typeof(CameraPageRender))]
namespace MDispatch.Droid.NewrRender
{

    [Obsolete]
    class CameraPageRender : PageRenderer, TextureView.ISurfaceTextureListener, IAutoFocusCallback
    {
        private string type = "Photo";
        private bool isFocus = false;

        public CameraPageRender(Context context) : base(context)
        {
           
        }

        RelativeLayout mainLayout;
        TextureView liveView;
        ProgressBar progressBar;
        Button capturePhotoButton;
        Button scanPhotoButton;

        [Obsolete]
        Android.Hardware.Camera camera;

        Activity Activity => this.Context as Activity;

        protected override async void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);
            type = ((CameraPage)Element).TypeCamera == null ? "Photo" : ((CameraPage)Element).TypeCamera;
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


            capturePhotoButton = new Button(Context);
            scanPhotoButton = new Button(Context);
            if (((CameraPage)Element).TypeCamera == null || ((CameraPage)Element).TypeCamera == "Photo")
            {
                capturePhotoButton.SetBackgroundDrawable(ContextCompat.GetDrawable(Context, Resource.Drawable.Take));
                RelativeLayout.LayoutParams captureButtonParams = new RelativeLayout.LayoutParams(
                    RelativeLayout.LayoutParams.WrapContent,
                    RelativeLayout.LayoutParams.WrapContent);
                captureButtonParams.Height = 120;
                captureButtonParams.Width = 120;
                capturePhotoButton.LayoutParameters = captureButtonParams;
                mainLayout.AddView(capturePhotoButton);
            }
            else if (((CameraPage)Element).TypeCamera == "DetectText")
            {
                scanPhotoButton.Text = "Scan";
                RelativeLayout.LayoutParams captureButtonParams = new RelativeLayout.LayoutParams(
                    RelativeLayout.LayoutParams.WrapContent,
                    RelativeLayout.LayoutParams.WrapContent);
                captureButtonParams.Height = 120;
                captureButtonParams.Width = 120;
                capturePhotoButton.LayoutParameters = captureButtonParams;
                mainLayout.AddView(scanPhotoButton);
            }
            else
            {
                capturePhotoButton.SetBackgroundDrawable(ContextCompat.GetDrawable(Context, Resource.Drawable.Take));
                RelativeLayout.LayoutParams captureButtonParams = new RelativeLayout.LayoutParams(
                    RelativeLayout.LayoutParams.WrapContent,
                    RelativeLayout.LayoutParams.WrapContent);
                captureButtonParams.Height = 120;
                captureButtonParams.Width = 120;
                capturePhotoButton.LayoutParameters = captureButtonParams;
                mainLayout.AddView(capturePhotoButton);
            }


            progressBar = new ProgressBar(Context);
            RelativeLayout.LayoutParams progresBarParams = new RelativeLayout.LayoutParams(
                RelativeLayout.LayoutParams.WrapContent,
                RelativeLayout.LayoutParams.WrapContent);
            progresBarParams.Height = 120;
            progresBarParams.Width = 120;
            progressBar.LayoutParameters = progresBarParams;
            progressBar.Indeterminate = true;
            progressBar.KeepScreenOn = true;
            mainLayout.AddView(progressBar);

            AddView(mainLayout);
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            if (!changed)
                return;
            try
            {
                progressBar.Visibility = Android.Views.ViewStates.Invisible;
                var msw1 = MeasureSpec.MakeMeasureSpec(r, MeasureSpecMode.Exactly);
                var msh1 = MeasureSpec.MakeMeasureSpec(b, MeasureSpecMode.Exactly);
                mainLayout.Measure(msw1, msh1);
                mainLayout.Layout(l, t, r, b);
                if (mainLayout.Width < mainLayout.Height)
                {
                    if (camera != null)
                    {
                        //camera.SetDisplayOrientation(90);

                    }
                    int tmpPr = (mainLayout.Width / 100) * 10;
                    capturePhotoButton.SetX(mainLayout.Width / 2 + tmpPr);
                    capturePhotoButton.SetY(mainLayout.Height - 200);
                    scanPhotoButton.SetX(mainLayout.Width / 2 + tmpPr);
                    scanPhotoButton.SetY(mainLayout.Height - 200);
                    progressBar.SetX(mainLayout.Width / 2 + tmpPr);
                    progressBar.SetY(mainLayout.Height - 200);
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
                    scanPhotoButton.SetY(mainLayout.Height / 2 + tmpPr);
                    scanPhotoButton.SetX(mainLayout.Width - 200);
                    progressBar.SetY(mainLayout.Height / 2 + tmpPr);
                    progressBar.SetX(mainLayout.Width - 200);
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
            scanPhotoButton.Click += async (sender, e) =>
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
                progressBar.Visibility = Android.Views.ViewStates.Visible;
                capturePhotoButton.Visibility = Android.Views.ViewStates.Invisible;
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
            byte[] imageBytes = null;
            camera.StopPreview();
            var ratio = ((decimal)Height) / Width;
            try
            {
                var image = Bitmap.CreateBitmap(liveView.Bitmap, 0, 0, liveView.Bitmap.Width, (int)(liveView.Bitmap.Width * ratio));
                using (var imageStream = new System.IO.MemoryStream())
                {
                    await image.CompressAsync(Bitmap.CompressFormat.Jpeg, 70, imageStream);
                    image.Recycle();
                    imageBytes = imageStream.ToArray();
                }
                camera.StartPreview();
            }
            catch
            {

            }
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
            }
            else
            {
                camera.SetDisplayOrientation(0);
            }
            camera.StartPreview();
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
                //(Element as CameraPage).Cancel();
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
            var bytes = await TakePhoto();
            if (GetOrientation())
            {
                bytes = ResizeImage(bytes, 720, 1280);
            }
            else
            {
                bytes = ResizeImage(bytes, 1280, 720);
            }
            if (type == "Photo")
            {
                (Element as CameraPage).SetPhotoResult(bytes, liveView.Bitmap.Width, liveView.Bitmap.Height);
            }
            else if(type == "DetectText")
            {
                (Element as CameraPage).SetScan(bytes, liveView.Bitmap.Width, liveView.Bitmap.Height);
            }
            progressBar.Visibility = Android.Views.ViewStates.Invisible;
            capturePhotoButton.Visibility = Android.Views.ViewStates.Visible;
            isFocus = false;
        }

        public byte[] ResizeImage(byte[] imageData, float width, float height)
        {
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);
            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Jpeg, 75, ms);
                return ms.ToArray();
            }
        }
        #endregion
    }
}
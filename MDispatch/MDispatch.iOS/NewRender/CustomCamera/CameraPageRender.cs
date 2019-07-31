using System;
using System.Threading.Tasks;
using AVFoundation;
using CoreGraphics;
using Foundation;
using MDispatch.iOS.NewRender.CustomCamera;
using MDispatch.NewElement;
using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.ExportRenderer(typeof(CameraPage), typeof(CameraPageRender))]
namespace MDispatch.iOS.NewRender.CustomCamera
{
    public class CameraPageRender : PageRenderer
    {
        AVCaptureSession captureSession;
        AVCaptureDeviceInput captureDeviceInput;
        AVCaptureStillImageOutput stillImageOutput;

        AVCaptureVideoPreviewLayer videoPreviewLayer = null;
        UIPaintCodeButton takePhotoButton;
        UIView liveCameraStream;

        public CameraPageRender()
        {
        }

        

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupUserInterface();
            SetupEventHandlers();
            await AuthorizeCameraUse();
            SetupLiveCameraStream();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }

        private async Task AuthorizeCameraUse()
        {
            var authorizationStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);
            if (authorizationStatus != AVAuthorizationStatus.Authorized)
            {
                await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
            }
        }

        private void SetupLiveCameraStream()
        {
            captureSession = new AVCaptureSession();
            videoPreviewLayer = new AVCaptureVideoPreviewLayer(captureSession)
            {
                Frame = liveCameraStream.Bounds,
                Orientation = GetCameraForOrientation()
            };
            liveCameraStream.Layer.AddSublayer(videoPreviewLayer);
            var captureDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);
            ConfigureCameraForDevice(captureDevice);
            captureDeviceInput = AVCaptureDeviceInput.FromDevice(captureDevice);
            var dictionary = new NSMutableDictionary();
            dictionary[AVVideo.CodecKey] = new NSNumber((int)AVVideoCodec.JPEG);
            stillImageOutput = new AVCaptureStillImageOutput()
            {
                OutputSettings = new NSDictionary()
            };
            captureSession.AddOutput(stillImageOutput);
            captureSession.AddInput(captureDeviceInput);
            captureSession.StartRunning();
        }

        public async Task<NSData> CapturePhoto()
        {
            var videoConnection = stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
            var sampleBuffer = await stillImageOutput.CaptureStillImageTaskAsync(videoConnection);
            var jpegImageAsNsData = AVCaptureStillImageOutput.JpegStillToNSData(sampleBuffer);
            return jpegImageAsNsData;
        }

        public AVCaptureVideoOrientation GetCameraForOrientation()
        {
            var currentOrientation = UIApplication.SharedApplication.StatusBarOrientation;
            AVCaptureVideoOrientation aVCaptureVideoOrientation = AVCaptureVideoOrientation.Portrait;

            if (currentOrientation == UIInterfaceOrientation.Portrait)
            {
                aVCaptureVideoOrientation = AVCaptureVideoOrientation.Portrait;
            }
            else if (currentOrientation == UIInterfaceOrientation.LandscapeRight)
            {
                aVCaptureVideoOrientation = AVCaptureVideoOrientation.LandscapeRight;
            }
            return aVCaptureVideoOrientation;
        }

        private void SetupEventHandlers()
        {
            takePhotoButton.TouchUpInside += async (s, e) =>
            {
                var data = await CapturePhoto();
                UIImage imageInfo = new UIImage(data);
                (Element as CameraPage).SetPhotoResult(data.ToArray(),
                                                            (int)imageInfo.Size.Width,
                                                            (int)imageInfo.Size.Height);
            };
        }

        private void SetupUserInterface()
        {
            var centerButtonX = View.Bounds.GetMidX() + 150;
            var bottomButtonY = View.Bounds.Bottom - 85;
            var topRightX = View.Bounds.Right - 65;
            var buttonWidth = 70;
            var buttonHeight = 70;
            liveCameraStream = new UIView()
            {
                Frame = new CGRect(0f, 0f, View.Bounds.Width, View.Bounds.Height)
            };

            takePhotoButton = new UIPaintCodeButton(DrawTakePhotoButton)
            {
                Frame = new CGRect(centerButtonX, bottomButtonY, buttonWidth, buttonHeight)
            };
            View.InsertSubview(liveCameraStream, 0);
            View.Add(takePhotoButton);
        }

        public void ConfigureCameraForDevice(AVCaptureDevice device)
        {
            var error = new NSError();
            if (device.IsFocusModeSupported(AVCaptureFocusMode.ContinuousAutoFocus))
            {
                device.LockForConfiguration(out error);
                device.FocusMode = AVCaptureFocusMode.ContinuousAutoFocus;
                device.UnlockForConfiguration();
            }
            else if (device.IsExposureModeSupported(AVCaptureExposureMode.ContinuousAutoExposure))
            {
                device.LockForConfiguration(out error);
                device.ExposureMode = AVCaptureExposureMode.ContinuousAutoExposure;
                device.UnlockForConfiguration();
            }
            else if (device.IsWhiteBalanceModeSupported(AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance))
            {
                device.LockForConfiguration(out error);
                device.WhiteBalanceMode = AVCaptureWhiteBalanceMode.ContinuousAutoWhiteBalance;
                device.UnlockForConfiguration();
            }
        }

        private void DrawTakePhotoButton(CGRect frame)
        {
            var color = UIColor.White;
            var bezierPath = new UIBezierPath();
            bezierPath.MoveTo(new CGPoint(frame.GetMinX() + 0.50000f * frame.Width, frame.GetMinY() + 0.08333f * frame.Height));
            bezierPath.AddCurveToPoint(new CGPoint(frame.GetMinX() + 0.27302f * frame.Width, frame.GetMinY() + 0.15053f * frame.Height), new CGPoint(frame.GetMinX() + 0.41628f * frame.Width, frame.GetMinY() + 0.08333f * frame.Height), new CGPoint(frame.GetMinX() + 0.33832f * frame.Width, frame.GetMinY() + 0.10803f * frame.Height));
            bezierPath.AddCurveToPoint(new CGPoint(frame.GetMinX() + 0.08333f * frame.Width, frame.GetMinY() + 0.50000f * frame.Height), new CGPoint(frame.GetMinX() + 0.15883f * frame.Width, frame.GetMinY() + 0.22484f * frame.Height), new CGPoint(frame.GetMinX() + 0.08333f * frame.Width, frame.GetMinY() + 0.35360f * frame.Height));
            bezierPath.AddCurveToPoint(new CGPoint(frame.GetMinX() + 0.50000f * frame.Width, frame.GetMinY() + 0.91667f * frame.Height), new CGPoint(frame.GetMinX() + 0.08333f * frame.Width, frame.GetMinY() + 0.73012f * frame.Height), new CGPoint(frame.GetMinX() + 0.26988f * frame.Width, frame.GetMinY() + 0.91667f * frame.Height));
            bezierPath.AddCurveToPoint(new CGPoint(frame.GetMinX() + 0.91667f * frame.Width, frame.GetMinY() + 0.50000f * frame.Height), new CGPoint(frame.GetMinX() + 0.73012f * frame.Width, frame.GetMinY() + 0.91667f * frame.Height), new CGPoint(frame.GetMinX() + 0.91667f * frame.Width, frame.GetMinY() + 0.73012f * frame.Height));
            bezierPath.AddCurveToPoint(new CGPoint(frame.GetMinX() + 0.50000f * frame.Width, frame.GetMinY() + 0.08333f * frame.Height), new CGPoint(frame.GetMinX() + 0.91667f * frame.Width, frame.GetMinY() + 0.26988f * frame.Height), new CGPoint(frame.GetMinX() + 0.73012f * frame.Width, frame.GetMinY() + 0.08333f * frame.Height));
            bezierPath.ClosePath();
            bezierPath.MoveTo(new CGPoint(frame.GetMinX() + 1.00000f * frame.Width, frame.GetMinY() + 0.50000f * frame.Height));
            bezierPath.AddCurveToPoint(new CGPoint(frame.GetMinX() + 0.50000f * frame.Width, frame.GetMinY() + 1.00000f * frame.Height), new CGPoint(frame.GetMinX() + 1.00000f * frame.Width, frame.GetMinY() + 0.77614f * frame.Height), new CGPoint(frame.GetMinX() + 0.77614f * frame.Width, frame.GetMinY() + 1.00000f * frame.Height));
            bezierPath.AddCurveToPoint(new CGPoint(frame.GetMinX() + 0.00000f * frame.Width, frame.GetMinY() + 0.50000f * frame.Height), new CGPoint(frame.GetMinX() + 0.22386f * frame.Width, frame.GetMinY() + 1.00000f * frame.Height), new CGPoint(frame.GetMinX() + 0.00000f * frame.Width, frame.GetMinY() + 0.77614f * frame.Height));
            bezierPath.AddCurveToPoint(new CGPoint(frame.GetMinX() + 0.19894f * frame.Width, frame.GetMinY() + 0.10076f * frame.Height), new CGPoint(frame.GetMinX() + 0.00000f * frame.Width, frame.GetMinY() + 0.33689f * frame.Height), new CGPoint(frame.GetMinX() + 0.07810f * frame.Width, frame.GetMinY() + 0.19203f * frame.Height));
            bezierPath.AddCurveToPoint(new CGPoint(frame.GetMinX() + 0.50000f * frame.Width, frame.GetMinY() + 0.00000f * frame.Height), new CGPoint(frame.GetMinX() + 0.28269f * frame.Width, frame.GetMinY() + 0.03751f * frame.Height), new CGPoint(frame.GetMinX() + 0.38696f * frame.Width, frame.GetMinY() + 0.00000f * frame.Height));
            bezierPath.AddCurveToPoint(new CGPoint(frame.GetMinX() + 1.00000f * frame.Width, frame.GetMinY() + 0.50000f * frame.Height), new CGPoint(frame.GetMinX() + 0.77614f * frame.Width, frame.GetMinY() + 0.00000f * frame.Height), new CGPoint(frame.GetMinX() + 1.00000f * frame.Width, frame.GetMinY() + 0.22386f * frame.Height));
            bezierPath.ClosePath();
            color.SetFill();
            bezierPath.Fill();
            UIColor.Black.SetStroke();
            bezierPath.LineWidth = 1.0f;
            bezierPath.Stroke();
            var ovalPath = UIBezierPath.FromOval(new CGRect(frame.GetMinX() + NMath.Floor(frame.Width * 0.12500f + 0.5f), frame.GetMinY() + NMath.Floor(frame.Height * 0.12500f + 0.5f), NMath.Floor(frame.Width * 0.87500f + 0.5f) - NMath.Floor(frame.Width * 0.12500f + 0.5f), NMath.Floor(frame.Height * 0.87500f + 0.5f) - NMath.Floor(frame.Height * 0.12500f + 0.5f)));
            color.SetFill();
            ovalPath.Fill();
            UIColor.Black.SetStroke();
            ovalPath.LineWidth = 1.0f;
            ovalPath.Stroke();
        }
    }
}
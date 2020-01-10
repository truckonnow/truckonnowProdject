using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using AVFoundation;
using CoreGraphics;
using Foundation;
using MDispatch.iOS.NewRender.RebderVideoCamera;
using MDispatch.NewElement.CustomVideoCam;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(VideoCameraPage), typeof(VideoRecorderRenderer))]
namespace MDispatch.iOS.NewRender.RebderVideoCamera
{
    public class VideoRecorderRenderer : PageRenderer, IAVCaptureFileOutputRecordingDelegate
    {
        AVCaptureSession captureSession;
        AVCaptureDeviceInput captureDeviceInput;
        AVCaptureMovieFileOutput aVCaptureMovieFileOutput;

        AVCaptureVideoPreviewLayer videoPreviewLayer = null;
        UIButton startVideo;
        UIButton stopVideo;
        UIView liveCameraStream;

        public override void WillAnimateRotation(UIInterfaceOrientation toInterfaceOrientation, double duration)
        {
            base.WillAnimateRotation(toInterfaceOrientation, duration);
            ReSetOrientation(toInterfaceOrientation);
        }

        public override async void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupUserInterface();
            SetupEventHandlers();
            await AuthorizeCameraUse();
            SetupLiveCameraStream();
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
            captureSession.SessionPreset = AVCaptureSession.PresetMedium;
            videoPreviewLayer = new AVCaptureVideoPreviewLayer(captureSession)
            {
                Frame = new CGRect(0f, 0f, View.Bounds.Width, View.Bounds.Height),
                Orientation = GetCameraForOrientation()
            };
            liveCameraStream.Layer.AddSublayer(videoPreviewLayer);
            var captureDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);
            ConfigureCameraForDevice(captureDevice);
            captureDeviceInput = AVCaptureDeviceInput.FromDevice(captureDevice);
            aVCaptureMovieFileOutput = new AVCaptureMovieFileOutput();

            var audioDevice = AVCaptureDevice.GetDefaultDevice(AVMediaType.Audio);
            var audioDeviceInput = AVCaptureDeviceInput.FromDevice(audioDevice);


            captureSession.AddOutput(aVCaptureMovieFileOutput);
            captureSession.AddInput(captureDeviceInput);
            captureSession.AddInput(audioDeviceInput);
            aVCaptureMovieFileOutput.ConnectionFromMediaType(AVMediaType.Video).VideoOrientation = GetCameraForOrientation();
            captureSession.StartRunning();
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

        public AVCaptureVideoOrientation GetCameraForOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            AVCaptureVideoOrientation aVCaptureVideoOrientation = AVCaptureVideoOrientation.Portrait;

            if (toInterfaceOrientation == UIInterfaceOrientation.Portrait)
            {
                aVCaptureVideoOrientation = AVCaptureVideoOrientation.Portrait;
            }
            else if (toInterfaceOrientation == UIInterfaceOrientation.LandscapeRight)
            {
                aVCaptureVideoOrientation = AVCaptureVideoOrientation.LandscapeRight;
            }
            return aVCaptureVideoOrientation;
        }


        private void SetupEventHandlers()
        {
            startVideo.TouchUpInside += async (s, e) =>
            {
                if (!(Element as VideoCameraPage).IsRecording)
                {
                    var rightButtonX = View.Bounds.Right - 85;
                    var bottomButtonY = View.Bounds.Bottom;
                    var buttonWidth = 70;
                    var buttonHeight = 70;
                    UIView.Animate(0, 0, UIViewAnimationOptions.BeginFromCurrentState, () =>
                    {
                        stopVideo.Frame = new CGRect(rightButtonX, bottomButtonY - 85, buttonWidth, buttonHeight);
                    }, () =>
                    {
                        UIView.Animate(0.5, 0, UIViewAnimationOptions.BeginFromCurrentState, () =>
                        {
                            stopVideo.Enabled = true;
                            stopVideo.Alpha = 1;
                            startVideo.Alpha = 0;
                        }, () =>
                        {
                            UIView.Animate(0, 0, UIViewAnimationOptions.BeginFromCurrentState, () =>
                            {
                                startVideo.Frame = new CGRect(rightButtonX, bottomButtonY - 200, buttonWidth, buttonHeight);
                                startVideo.Enabled = false;
                            }, null);
                        });
                    });
                    await RecordVideo();
                }
            };

            stopVideo.TouchUpInside += async (s, e) =>
            {
                if ((Element as VideoCameraPage).IsRecording)
                {
                    await StopRecorddVideo();
                }
            };
        }

        private async Task RecordVideo()
        {
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var outputFilePath = Path.Combine(basePath, Path.ChangeExtension("video", "mp4"));
            (Element as VideoCameraPage).IsRecording = true;
            aVCaptureMovieFileOutput.StartRecordingToOutputFile(NSUrl.FromFilename(outputFilePath), this);
        }

        private async Task StopRecorddVideo()
        {

            (Element as VideoCameraPage).IsRecording = false;
            aVCaptureMovieFileOutput.StopRecording();
            string filepath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            string filename = System.IO.Path.Combine(filepath, "video.mp4");
            if (File.Exists(filename))
            {
                byte[] bytes = File.ReadAllBytes(filename);
                (Element as VideoCameraPage).SetPhotoResult(bytes);
                File.Delete(filename);
            }
        }

        public static byte[] ResizeImageIOS(UIImage originalImage, float width, float height)
        {
            UIImageOrientation orientation = originalImage.Orientation;
            using (CGBitmapContext context = new CGBitmapContext(IntPtr.Zero,
                                                 (int)width, (int)height, 8,
                                                 4 * (int)width, CGColorSpace.CreateDeviceRGB(),
                                                 CGImageAlphaInfo.PremultipliedFirst))
            {
                //RectangleF imageRect = new RectangleF(0, 0, width, height);
                //context.DrawImage(imageRect, originalImage.CGImage);
                UIKit.UIImage resizedImage = UIKit.UIImage.FromImage(context.ToImage(), 0, orientation);
                return resizedImage.AsJPEG(.5f).ToArray();
            }
        }

        public static UIKit.UIImage ImageFromByteArray(byte[] data)
        {
            if (data == null)
            {
                return null;
            }
            UIKit.UIImage image;
            try
            {
                image = new UIKit.UIImage(Foundation.NSData.FromArray(data));
            }
            catch (Exception e)
            {
                return null;
            }
            return image;
        }

        private void ReSetOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            var rightButtonX = View.Bounds.Right - 85;
            var bottomButtonY = View.Bounds.Bottom - 85;
            liveCameraStream.Frame = new CGRect(0f, 0f, View.Bounds.Width, View.Bounds.Height);
            videoPreviewLayer.Frame = liveCameraStream.Bounds;
            videoPreviewLayer.Connection.VideoOrientation = GetCameraForOrientation();
            videoPreviewLayer.Orientation = GetCameraForOrientation(toInterfaceOrientation);
            startVideo.Frame = new CGRect(rightButtonX, bottomButtonY, 70, 70);
            stopVideo.Frame = new CGRect(rightButtonX, bottomButtonY, 70, 70);
        }

        private void SetupUserInterface()
        {
            var rightButtonX = View.Bounds.Right - 85;
            var bottomButtonY = View.Bounds.Bottom - 85;
            var buttonWidth = 70;
            var buttonHeight = 70;
            liveCameraStream = new UIView()
            {
                Frame = new CGRect(0f, 0f, View.Bounds.Width, View.Bounds.Height)
            };
            startVideo = new UIButton(UIButtonType.Custom)
            {
                Frame = new CGRect(rightButtonX, bottomButtonY, buttonWidth, buttonHeight)
            };
            startVideo.SetBackgroundImage(UIImage.FromBundle("startVideo.png"), UIControlState.Normal);
            stopVideo = new UIButton(UIButtonType.Custom)
            {
                Frame = new CGRect(rightButtonX, bottomButtonY - 120, buttonWidth, buttonHeight),
                Alpha = 0,
                Enabled = false
            };
            stopVideo.SetBackgroundImage(UIImage.FromBundle("stopVideo.png"), UIControlState.Normal);
            View.InsertSubview(liveCameraStream, 0);
            View.Add(startVideo);
            View.Add(stopVideo);
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

        public void FinishedRecording(AVCaptureFileOutput captureOutput, NSUrl outputFileUrl, NSObject[] connections, NSError error)
        {
            
        }
    }
}
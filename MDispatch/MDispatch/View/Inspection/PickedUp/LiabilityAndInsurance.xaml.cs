using MDispatch.Models;
using MDispatch.NewElement.ImageSize;
using MDispatch.Service;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using MDispatch.ViewModels.InspectionMV.PickedUpMV.TypeVe;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.Inspection.PickedUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LiabilityAndInsurance : ContentPage
	{
        private LiabilityAndInsuranceMV liabilityAndInsuranceMV = null;

        public LiabilityAndInsurance (ManagerDispatchMob managerDispatchMob, string idVech, string idShip, InitDasbordDelegate initDasbordDelegate)
		{
            liabilityAndInsuranceMV = new LiabilityAndInsuranceMV(managerDispatchMob, idVech, idShip, Navigation, initDasbordDelegate);
            InitializeComponent ();
            BindingContext = liabilityAndInsuranceMV;
            InitElemnt();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (isSignatureAsk)
            {
                liabilityAndInsuranceMV.SaveSig();
            }
            else
            {
                CheckAsk();
            }
        }

        public async void InitElemnt()
        {
            await Wait();
            if (liabilityAndInsuranceMV.Shipping != null && liabilityAndInsuranceMV.Shipping.VehiclwInformations != null)
            {
                foreach (var VehiclwInformation in liabilityAndInsuranceMV.Shipping.VehiclwInformations)
                {
                    VechInfoSt.Children.Add(new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label()
                            {
                                Text = VehiclwInformation.Year,
                                FontSize = 18,
                                TextColor = Color.Black
                            },
                            new Label()
                            {
                                Text = VehiclwInformation.Make,
                                FontSize = 18,
                                TextColor = Color.Black
                            },
                            new Label()
                            {
                                Text = VehiclwInformation.Model,
                                FontSize = 18,
                                TextColor = Color.Black
                            },
                        }
                    });
                    VechInfoSt.Children.Add(new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label()
                                    {
                                        Text = "VIN#",
                                        FontSize = 18,
                                    },
                                    new Label()
                                    {
                                        Text = VehiclwInformation.VIN,
                                        FontSize = 18,
                                        TextColor = Color.Black
                                    }
                        }
                    });
                    VechInfoSt.Children.Add(new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label()
                                    {
                                        Text = "Type:",
                                        FontSize = 18,
                                    },
                                    new Label()
                                    {
                                        Text = VehiclwInformation.Type,
                                        FontSize = 18,
                                        TextColor = Color.Black
                                    }
                        }
                    });
                    VechInfoSt.Children.Add(new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            new Label()
                            {
                                Text = "Color:",
                                FontSize = 18,
                            },
                            new Label()
                            {
                                Text = VehiclwInformation.Color,
                                FontSize = 18,
                                TextColor = Color.Black
                            }
                        }
                    });
                    await InitDamage();
                    VechInfoSt1.Children.Add(new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                                    new Image()
                                    {
                                        Source = "DamageP1.png",
                                        HeightRequest = 18,
                                        WidthRequest = 18
                                    },
                                    new Label()
                                    {
                                        HorizontalTextAlignment = TextAlignment.Center,
                                        Text = "Circles Yellow — pickup damages;",
                                        FontSize = 13
                                    },
                                    new Image()
                                    {
                                        Source = "DamageD1.png",
                                        HeightRequest = 18,
                                        WidthRequest = 18
                                    },
                                    new Label()
                                    {
                                        HorizontalTextAlignment = TextAlignment.Center,
                                        Text = "Circles Green — delivery damages;",
                                        FontSize = 13
                                    },
                        }
                    });

                    FlexLayout flexLayout = new FlexLayout()
                    {
                        Wrap = FlexWrap.Wrap,
                        Opacity = 0.7,
                        BackgroundColor = Color.FromHex("#F3F781"),
                        Children =
                        {
                            new Label()
                            {
                                HorizontalTextAlignment = TextAlignment.Center,
                                Text = "See inspection photo:",
                                FontSize = 16
                            },
                            new Label()
                            {
                                HorizontalTextAlignment = TextAlignment.Center,
                                TextColor = Color.Blue,
                                Text = $"http://192.168.0.100:22929/Photo/BOL/{VehiclwInformation.Id}",
                                FontSize = 16
                            }
                        }
                    };
                    flexLayout.GestureRecognizers.Add(new TapGestureRecognizer(GetPagePhotoInspection));
                    VechInfoSt1.Children.Add(flexLayout);
                }
            }
            liabilityAndInsuranceMV.StataLoadShip = 0;
        }
        

        private async void GetPagePhotoInspection(Xamarin.Forms.View v, object s)
        {
            Label label = (Label)((FlexLayout)v).Children[1];
            await Navigation.PushAsync(new PhotoInspectionWeb(label.Text));
        }

        bool isSignatureAsk = false;
        private async void Sign_StrokeCompleted(object sender, EventArgs e)
        {
            Photo photo = new Photo();
            isSignatureAsk = true;
            Stream stream = await sign.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);
            MemoryStream memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            byte[] image = memoryStream.ToArray();
            photo.Base64 = JsonConvert.SerializeObject(image);
            photo.path = $"../Photo/{liabilityAndInsuranceMV.Shipping.VehiclwInformations.Find(v => v.Id == liabilityAndInsuranceMV.IdVech)}/PikedUp/Signature/PikedUp.Png";
            liabilityAndInsuranceMV.SigPhoto = photo;
        }

        private void Sign_Cleared(object sender, EventArgs e)
        {
            isSignatureAsk = false;
            liabilityAndInsuranceMV.SigPhoto = null;
        }

        private async Task InitDamage()
        {
            AbsoluteLayout absoluteLayout = new AbsoluteLayout();
            Image image = new Image()
            {
                Source = "scan.png",
            };
            AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(image, new Rectangle(0.5, 0.5, 1, 1));
            VechInfoSt3.Children.Add(absoluteLayout);
            absoluteLayout.Children.Add(image);
            liabilityAndInsuranceMV.StataLoadShip = 0;
            Image image1 = new Image()
            {
                Source = "DamageP11.png"
            };
            ITypeScan typeScan = new TestCar();
            double x = typeScan.GetCordinatX(liabilityAndInsuranceMV.Shipping.VehiclwInformations[0].PhotoInspections[0].IndexPhoto.ToString(), liabilityAndInsuranceMV.Shipping.VehiclwInformations[0].PhotoInspections[0].Damages[0].XInterest);
            double y = typeScan.GetCordinatY(liabilityAndInsuranceMV.Shipping.VehiclwInformations[0].PhotoInspections[0].IndexPhoto.ToString(), liabilityAndInsuranceMV.Shipping.VehiclwInformations[0].PhotoInspections[0].Damages[0].YInterest);
            AbsoluteLayout.SetLayoutFlags(image1, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(image1, new Rectangle(x, y, 10, 10));
            absoluteLayout.Children.Add(image1);
        }

        private async Task Wait()
        {
            await Task.Run(() =>
            {
                while (liabilityAndInsuranceMV.StataLoadShip == 0)
                {

                }
            });
        }

        private void CheckAsk()
        {
            if (!isSignatureAsk)
            {
                askBlock3.BorderColor = Color.Red;
            }
            else
            {
                askBlock3.BorderColor = Color.BlueViolet;
            }
        }
    }
}
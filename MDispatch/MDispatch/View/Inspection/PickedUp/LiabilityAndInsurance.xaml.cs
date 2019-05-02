using MDispatch.Models;
using MDispatch.NewElement.TouchCordinate;
using MDispatch.Service;
using MDispatch.View.GlobalDialogView;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using MDispatch.ViewModels.InspectionMV.Servise.Paymmant;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
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
        public LiabilityAndInsuranceMV liabilityAndInsuranceMV = null;
        private IPaymmant Paymmant = null;

        public LiabilityAndInsurance (ManagerDispatchMob managerDispatchMob, string idVech, string idShip, InitDasbordDelegate initDasbordDelegate, string OnDeliveryToCarrier, string totalPaymentToCarrier)
		{
            liabilityAndInsuranceMV = new LiabilityAndInsuranceMV(managerDispatchMob, idVech, idShip, Navigation, initDasbordDelegate);
            InitializeComponent ();
            BindingContext = liabilityAndInsuranceMV;
            InitElemnt();
            InitPayment(OnDeliveryToCarrier, totalPaymentToCarrier);
        }

        private void InitPayment(string OnDeliveryToCarrier, string totalPaymentToCarrier)
        {
            if (totalPaymentToCarrier == "COP")
            {
                payBlockSelectPatment.IsVisible = true;
            }
            else if (totalPaymentToCarrier == "COD")
            {
                isAsk2 = true;
                askBlock2.IsVisible = false;
                liabilityAndInsuranceMV.What_form_of_payment_are_you_using_to_pay_for_transportation = "COP";
            }
            else
            {
                liabilityAndInsuranceMV.What_form_of_payment_are_you_using_to_pay_for_transportation = "Biling";
                isAsk2 = true;
                instructionL.Text = OnDeliveryToCarrier;
                bilingPay.IsVisible = true;
            }
        }

        [Obsolete]
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Paymmant != null)
            {
                isAsk2 = Paymmant.IsAskPaymmant;
            }
            if (isSignatureAsk && isAsk2)
            {
                liabilityAndInsuranceMV.SaveSigAndMethodPay();
            }
            else
            {
                await PopupNavigation.PushAsync(new Errror("You did not fill in all the required fields, you can continue the inspection only when filling in the required fields !!", null));
                CheckAsk();
            }
        }

        [Obsolete]
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

                    VechInfoSt3.Children.Add(new Image()
                    {
                        Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(VehiclwInformation.Scan.Base64)))
                    });

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
            photo.path = $"../Photo/{liabilityAndInsuranceMV.Shipping.VehiclwInformations.Find(v => v.Id == liabilityAndInsuranceMV.IdVech)}/PikedUp/Signature/PikedUp.jpg";
            liabilityAndInsuranceMV.SigPhoto = photo;
        }

        private void Sign_Cleared(object sender, EventArgs e)
        {
            isSignatureAsk = false;
            liabilityAndInsuranceMV.SigPhoto = null;
        }

        private void Touch(object sender, TouchActionEventArgs args)
        {
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

            if (!isAsk2)
            {
                askBlock2.BorderColor = Color.Red;
            }
            else
            {
                askBlock2.BorderColor = Color.BlueViolet;
            }
        }

        private bool isAsk2 = false;
        private void Dropdown_SelectedItemChanged(object sender, Plugin.InputKit.Shared.Utils.SelectedItemChangedArgs e)
        {
            liabilityAndInsuranceMV.What_form_of_payment_are_you_using_to_pay_for_transportation = (string)e.NewItem;
            Paymmant = GetPaymmant((string)e.NewItem);
            if (payBlockSelectPatment.Children.Count == 4)
            {
                payBlockSelectPatment.Children.RemoveAt(3);
            }
            payBlockSelectPatment.Children.Add(Paymmant.GetStackLayout());
            isAsk2 = false;
        }

        private IPaymmant GetPaymmant(string paymmantName)
        {
            IPaymmant paymmant = null;
            switch (paymmantName)
            {
                case "Cash":
                    paymmant = new CashPaymmant(this);
                    break;
                case "Check":
                    paymmant = new CheckPaymmant(this);
                    break;
                case "Cradit card":
                    paymmant = new CraditCardPaymant(this);
                    break;
            }
            return paymmant;
        }
    }
}
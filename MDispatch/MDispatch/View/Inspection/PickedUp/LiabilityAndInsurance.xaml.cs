using MDispatch.Models;
using MDispatch.NewElement.TouchCordinate;
using MDispatch.Service;
using MDispatch.View.GlobalDialogView;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using MDispatch.ViewModels.InspectionMV.Servise.Paymmant;
using Plugin.Settings;
using Rg.Plugins.Popup.Services;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
        private Timer timer = null;

        [Obsolete]
        public LiabilityAndInsurance (ManagerDispatchMob managerDispatchMob, string idVech, string idShip, InitDasbordDelegate initDasbordDelegate, string OnDeliveryToCarrier, string totalPaymentToCarrier, bool isproplem)
		{
            liabilityAndInsuranceMV = new LiabilityAndInsuranceMV(managerDispatchMob, idVech, idShip, Navigation, initDasbordDelegate, this);
            InitializeComponent ();
            BindingContext = liabilityAndInsuranceMV;
            InitElemnt(isproplem);
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

        protected override bool OnBackButtonPressed()
        {
            if (timer != null)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            return base.OnBackButtonPressed();
        }

        protected override void OnDisappearing()
        {
            if (timer != null)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
            base.OnDisappearing();
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
        public async void InitElemnt(bool isProplem)
        {
            if (isProplem)
            {
                btnSave.IsVisible = false;
                blockAskPay.IsVisible = true;
                btnYesPay.IsVisible = false;
                btnNoPay.IsVisible = false;
                btnNumberOffice.IsVisible = true;
                lReport.IsVisible = true;
                blockAsk.IsVisible = false;
                timer = new Timer(new TimerCallback(CheckProplem), null, 5000, 5000);
            }
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
                        AlignContent = FlexAlignContent.Center,
                        AlignItems = FlexAlignItems.Center,
                        Opacity = 0.7,
                        BackgroundColor = Color.FromHex("#F3F781"),
                        Children =
                        {
                            new Label()
                            {
                                TextColor = Color.BlueViolet,
                                Text = "Click ",
                                FontSize = 16,
                                VerticalOptions = LayoutOptions.Center,
                            },
                            new Label()
                            {
                                HorizontalTextAlignment = TextAlignment.Center,
                                Text = "See inspection photo",
                                FontSize = 16,
                                VerticalTextAlignment = TextAlignment.Center
                            }
                        }
                    };
                    flexLayout.GestureRecognizers.Add(new TapGestureRecognizer(flexLayout_Clicked));
                    VechInfoSt1.Children.Add(flexLayout);
                }
            }
            liabilityAndInsuranceMV.StataLoadShip = 0;
        }

        private async void flexLayout_Clicked(Xamarin.Forms.View arg1, object arg2)
        {
            await Navigation.PushAsync(new BOLPage(liabilityAndInsuranceMV.managerDispatchMob, liabilityAndInsuranceMV.IdShip, liabilityAndInsuranceMV.initDasbordDelegate));
        }

        private async void CheckProplem(object o)
        {
            bool isProplem = await liabilityAndInsuranceMV.CheckProplem();
            if(!isProplem)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    blockPsw.IsVisible = false;
                    bloclThank.IsVisible = false;
                    btnSave.IsVisible = true;
                    blockAskPay.IsVisible = false;
                    btnYesPay.IsVisible = true;
                    btnNoPay.IsVisible = true;
                    btnNumberOffice.IsVisible = false;
                    lReport.IsVisible = false;
                    blockAsk.IsVisible = true;
                });
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }
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
            photo.Base64 = Convert.ToBase64String(image);
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

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            liabilityAndInsuranceMV.What_form_of_payment_are_you_using_to_pay_for_transportation = (string)((Picker)sender).SelectedItem;
            Paymmant = GetPaymmant((string)((Picker)sender).SelectedItem);
            if (payBlockSelectPatment.Children.Count == 4)
            {
                payBlockSelectPatment.Children.RemoveAt(3);
            }
            payBlockSelectPatment.Children.Add(Paymmant.GetStackLayout());
            isAsk2 = false;
        }


        private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BOLPage(liabilityAndInsuranceMV.managerDispatchMob, liabilityAndInsuranceMV.IdShip, liabilityAndInsuranceMV.initDasbordDelegate));
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new ContactInfo());
        }

        [Obsolete]
        private async void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(CrossSettings.Current.GetValueOrDefault("Password", "") == e.NewTextValue)
            {
                if (!payBlockSelectPatment.IsVisible)
                {
                    if (Paymmant != null)
                    {
                        isAsk2 = Paymmant.IsAskPaymmant;
                    }
                    if (isSignatureAsk && isAsk2)
                    {
                        liabilityAndInsuranceMV.SaveSigAndMethodPay();
                        if (liabilityAndInsuranceMV.What_form_of_payment_are_you_using_to_pay_for_transportation == "COD" || liabilityAndInsuranceMV.What_form_of_payment_are_you_using_to_pay_for_transportation == "COP" || liabilityAndInsuranceMV.What_form_of_payment_are_you_using_to_pay_for_transportation == "Biling")
                        {
                            liabilityAndInsuranceMV.GoToContinue();
                            //await PopupNavigation.PopAllAsync(true);
                        }
                        else
                        {
                            await PopupNavigation.PopAllAsync(true);
                            await PopupNavigation.PushAsync(new TempPageHint4());
                            if (liabilityAndInsuranceMV.What_form_of_payment_are_you_using_to_pay_for_transportation == "Cash")
                            {
                                await Navigation.PushAsync(new VideoCameraPage(liabilityAndInsuranceMV, ""));
                            }
                            else if (liabilityAndInsuranceMV.What_form_of_payment_are_you_using_to_pay_for_transportation == "Check")
                            {
                                await Navigation.PushAsync(new CameraPaymmant(liabilityAndInsuranceMV, "", "CheckPaymment.png"));
                            }
                            else
                            {
                                await Navigation.PushAsync(new Ask2Page(liabilityAndInsuranceMV.managerDispatchMob, liabilityAndInsuranceMV.IdVech, liabilityAndInsuranceMV.IdShip, liabilityAndInsuranceMV.initDasbordDelegate));
                            }
                        }
                    }
                    else
                    {
                        ((Entry)sender).Text = "";
                        blockAskPay.IsVisible = false;
                        await PopupNavigation.PushAsync(new Errror("You did not fill in all the required fields, you can continue the inspection only when filling in the required fields !!", null));
                        CheckAsk();
                    }
                }
                else
                {
                    ((Entry)sender).TextColor = Color.Default;
                    //btnConntinue.IsVisible = true;
                    blockAskPay.IsVisible = true;
                }
            }
            else
            {
                ((Entry)sender).TextColor = Color.Red;
                //btnConntinue.IsVisible = false;
                blockAskPay.IsVisible = false;
            }
        }

        bool isAsk3 = false;

        [Obsolete]
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            isAsk3 = true;
            if (Paymmant != null)
            {
                isAsk2 = Paymmant.IsAskPaymmant;
            }
            if (isSignatureAsk && isAsk2)
            {
                btnSave.IsVisible = false;
                bloclThank.IsVisible = true;
                blockPsw.IsVisible = true;
                await PopupNavigation.PushAsync(new CopyLibaryAndInsurance(liabilityAndInsuranceMV));

            }
            else
            {
                await PopupNavigation.PushAsync(new Errror("You did not fill in all the required fields, you can continue the inspection only when filling in the required fields !!", null));
                CheckAsk();
            }

        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            btnYesPay.IsVisible = false;
            btnNoPay.IsVisible = false;
            btnNumberOffice.IsVisible = true;
            lReport.IsVisible = true;
            blockAsk.IsVisible = false;
            bloclThank.IsVisible = false;
            blockPsw.IsVisible = false;
            liabilityAndInsuranceMV.SetProblem();
            timer = new Timer(new TimerCallback(CheckProplem), null, 10000, 10000);
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        { 
            PhoneDialer.Open("+17734305155");
        }
    }
}
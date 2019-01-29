using MDispatch.Service;
using MDispatch.ViewModels.InspectionMV.PickedUpMV;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MDispatch.Service.ManagerDispatchMob;

namespace MDispatch.View.Inspection.PickedUp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LiabilityAndInsurance : ContentPage
	{
        LiabilityAndInsuranceMV liabilityAndInsuranceMV = null;

        public LiabilityAndInsurance (ManagerDispatchMob managerDispatchMob, string idVech, string idShip, InitDasbordDelegate initDasbordDelegate)
		{
            liabilityAndInsuranceMV = new LiabilityAndInsuranceMV(managerDispatchMob, idVech, idShip, Navigation, initDasbordDelegate);
            InitializeComponent ();
            BindingContext = liabilityAndInsuranceMV;
            InitElemnt();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            liabilityAndInsuranceMV.Continue();
        }

        public async void InitElemnt()
        {
            await Task.Run(() =>
            {
                while(liabilityAndInsuranceMV.StataLoadShip == 0)
                {

                }
            });
            if (liabilityAndInsuranceMV.Shipping != null && liabilityAndInsuranceMV.Shipping.VehiclwInformations != null)
            {
                foreach (var VehiclwInformation in liabilityAndInsuranceMV.Shipping.VehiclwInformations)
                {
                    Image image = new Image()
                    {
                        Source = "scan.png",
                    };
                    AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.All);
                    AbsoluteLayout.SetLayoutBounds(image, new Rectangle(0.5, 0.5, 1, 1));
                    stVech.Children.Add(new StackLayout()
                    {
                        Padding = 2,
                        Margin = 2,
                        Children =
                        {
                            new StackLayout()
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
                            },
                            new StackLayout()
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
                            },
                            new StackLayout()
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
                                },
                            },
                            new StackLayout()
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
                                },
                            },
                            new AbsoluteLayout()
                            {
                                Children =
                                {
                                    image
                                }
                            },
                            new StackLayout()
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
                            },
                            new FlexLayout()
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
                                        Text = $"http://localhost:22929/Photo/BOL/{VehiclwInformation.Id}",
                                        FontSize = 16
                                    }
                                }
                            },
                            new BoxView()
                            {
                                HeightRequest = 1,
                                BackgroundColor = Color.Silver
                            }
                        }
                    });
                }
            }
            liabilityAndInsuranceMV.StataLoadShip = 0;
        }
    }
}
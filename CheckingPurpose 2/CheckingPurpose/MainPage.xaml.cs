using Microsoft.Maui.Controls.PlatformConfiguration;
#if IOS
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform;
using UIKit;
using Foundation;
using System;
using CoreGraphics;
#endif

namespace CheckingPurpose
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
        }

      
    }

#if IOS
    public class CustomEntry : ContentView
    {
        Entry entry;
        private UIButton returnButton;
        private UIButton minusButton;
        private UIButton separatorButton;
        private UIView toolbarView;
        private UIView lineView1, lineView2, lineView3, lineView4;
        private nfloat buttonWidth;
        private string anotherSeparator = "|"; // You can customize this

        public ReturnType ReturnType { get; set; } = ReturnType.Default;

        public CustomEntry()
        {
             entry=new Entry();
             this.Content=entry;
            entry.Keyboard = Keyboard.Numeric;
            entry.HandlerChanged +=  this.TextBox_HandlerChanged;
            this.Loaded += CustomEntry_Loaded;
        }

         private void TextBox_HandlerChanged(object? sender, EventArgs e) {
                        if ((sender as Entry)?.Handler != null && (sender as Entry)?.Handler?.PlatformView is UIKit.UITextField macEntry) 
            {
                //macEntry.ShouldChangeCharacters += ValidateTextChanged;
                //macEntry.BorderStyle = UITextBorderStyle.None;
                macEntry.KeyboardType = UIKeyboardType.DecimalPad;

                macEntry.EditingDidBegin += IOSEntry_EditingDidBegin;

            }
         }

           private void IOSEntry_EditingDidBegin(object? sender, EventArgs e)
  {
      UITextField? macEntry = sender as UITextField;
      if (macEntry!.InputAccessoryView == null) { macEntry.InputAccessoryView = this.toolbarView; }
  }

            

        private void CustomEntry_Loaded(object? sender, EventArgs e)
        {
            this.AddToolBarItems();
        }

        //protected override void OnCreateInputConnection(Android.Views.InputMethods.InputConnection inputConnection)
        //{
        //    base.OnCreateInputConnection(inputConnection);
        //    AddToolBarItems();
        //}

        private void AddToolBarItems()
        {
            this.returnButton = new UIButton();
            this.returnButton.SetTitle(ReturnType == ReturnType.Default ? "Return" : ReturnType.ToString(), UIControlState.Normal);
            this.returnButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            this.returnButton.BackgroundColor = UIColor.FromRGB(210, 213, 218);
            this.returnButton.TouchDown += ReturnButton_TouchDown;

            this.minusButton = new UIButton();
            this.minusButton.SetTitle("-", UIControlState.Normal);
            this.minusButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            this.minusButton.BackgroundColor = UIColor.FromRGB(210, 213, 218);
            this.minusButton.TouchDown += MinusButton_TouchDown;

            this.separatorButton = new UIButton();
            this.separatorButton.SetTitle(anotherSeparator, UIControlState.Normal);
            this.separatorButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            this.separatorButton.BackgroundColor = UIColor.FromRGB(210, 213, 218);
            this.separatorButton.TouchDown += SeparatorButton_TouchDown;

            this.toolbarView = new UIView();
            this.lineView1 = new UIView { BackgroundColor = UIColor.FromRGB(175, 175, 180) };
            this.lineView2 = new UIView { BackgroundColor = UIColor.FromRGB(175, 175, 180) };
            this.lineView3 = new UIView { BackgroundColor = UIColor.FromRGB(175, 175, 180) };
            this.lineView4 = new UIView { BackgroundColor = UIColor.FromRGB(175, 175, 180) };

            this.toolbarView.AddSubview(this.lineView1);
            this.toolbarView.AddSubview(this.lineView2);
            this.toolbarView.AddSubview(this.lineView3);
            this.toolbarView.AddSubview(this.lineView4);
            this.toolbarView.AddSubview(this.minusButton);
            this.toolbarView.AddSubview(this.separatorButton);
            this.toolbarView.AddSubview(this.returnButton);
            this.toolbarView.BackgroundColor = UIColor.FromRGB(249, 249, 249);

            UpdateFrames();
        }

         private void UpdateFrames()
 {
     if (UIDevice.CurrentDevice.Orientation != UIDeviceOrientation.Unknown)
     {
         if ( toolbarView == null || minusButton == null || separatorButton == null || returnButton == null 
             || lineView1 == null || lineView2 == null || lineView3 == null || lineView4 == null )
             return;

         nfloat width = UIScreen.MainScreen.Bounds.Width;
         this.buttonWidth = width / 3;
         this.toolbarView.Frame = new CGRect(0.0f, 0.0f, (float)width, 50.0f);
         if (UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.Portrait || UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.PortraitUpsideDown || UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.FaceUp || UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.FaceDown)
         {
             if (width <= 375 && width > 320)
             {
                 this.minusButton.Frame = new CGRect(0, 0, this.buttonWidth - 2, 50);
                 this.separatorButton.Frame = new CGRect(this.buttonWidth - 2, 0, this.buttonWidth + 4, 50);
                 this.returnButton.Frame = new CGRect(this.toolbarView.Frame.Size.Width - this.buttonWidth + 2, 0, this.buttonWidth, 50);
                 this.lineView1.Frame = new CGRect(0, 0, this.toolbarView.Frame.Size.Width, 0.5f);
                 this.lineView2.Frame = new CGRect(0, this.toolbarView.Frame.Size.Height - 0.5f, this.toolbarView.Frame.Size.Width, 0.5f);
                 this.lineView3.Frame = new CGRect(this.minusButton.Frame.Right - 0.5f, 0, 0.5, this.minusButton.Frame.Size.Height);
                 this.lineView4.Frame = new CGRect(this.returnButton.Frame.Left, 0, 0.5, this.minusButton.Frame.Size.Height);
             }
             else if (width <= 320)
             {
                 this.minusButton.Frame = new CGRect(0, 0, this.buttonWidth - 1.5, 50);
                 this.separatorButton.Frame = new CGRect(this.buttonWidth - 1.5, 0, this.buttonWidth + 3, 50);
                 this.returnButton.Frame = new CGRect(this.toolbarView.Frame.Size.Width - this.buttonWidth + 1.5, 0, this.buttonWidth, 50);
                 this.lineView1.Frame = new CGRect(0, 0, this.toolbarView.Frame.Size.Width, 0.5f);
                 this.lineView2.Frame = new CGRect(0, this.toolbarView.Frame.Size.Height - 0.5f, this.toolbarView.Frame.Size.Width, 0.5f);
                 this.lineView3.Frame = new CGRect(this.minusButton.Frame.Right - 0.5f, 0, 0.5, this.minusButton.Frame.Size.Height);
                 this.lineView4.Frame = new CGRect(this.returnButton.Frame.Left, 0, 0.5, this.minusButton.Frame.Size.Height);
             }
             else if (width > 375)
             {
                 this.minusButton.Frame = new CGRect(0, 0, this.buttonWidth - 2, 50);
                 this.separatorButton.Frame = new CGRect(this.buttonWidth - 2, 0, this.buttonWidth + 4, 50);
                 this.returnButton.Frame = new CGRect(this.toolbarView.Frame.Size.Width - this.buttonWidth + 2, 0, this.buttonWidth, 50);
                 this.lineView1.Frame = new CGRect(0, 0, this.toolbarView.Frame.Size.Width, 0.5f);
                 this.lineView2.Frame = new CGRect(0, this.toolbarView.Frame.Size.Height - 0.5f, this.toolbarView.Frame.Size.Width, 0.5f);
                 this.lineView3.Frame = new CGRect(this.minusButton.Frame.Right - 0.5f, 0, 0.5, this.minusButton.Frame.Size.Height);
                 this.lineView4.Frame = new CGRect(this.returnButton.Frame.Left, 0, 0.5, this.minusButton.Frame.Size.Height);
             }
         }
         else if (UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeLeft || UIDevice.CurrentDevice.Orientation == UIDeviceOrientation.LandscapeRight)
         {
             if (width <= 667)
             {
                 this.minusButton.Frame = new CGRect(0, 0, this.buttonWidth - 2.5, 50);
                 this.separatorButton.Frame = new CGRect(this.buttonWidth - 2.5, 0, this.buttonWidth + 5, 50);
                 this.returnButton.Frame = new CGRect(this.toolbarView.Frame.Size.Width - this.buttonWidth + 2.5, 0, this.buttonWidth, 50);
                 this.lineView1.Frame = new CGRect(0, 0, this.toolbarView.Frame.Size.Width, 0.5f);
                 this.lineView2.Frame = new CGRect(0, this.toolbarView.Frame.Size.Height - 0.5f, this.toolbarView.Frame.Size.Width, 0.5f);
                 this.lineView3.Frame = new CGRect(this.minusButton.Frame.Right - 0.5f, 0, 0.5, this.minusButton.Frame.Size.Height);
                 this.lineView4.Frame = new CGRect(this.returnButton.Frame.Left, 0, 0.5, this.minusButton.Frame.Size.Height);
             }
             else if (width > 667)
             {
                 this.minusButton.Frame = new CGRect(0, 0, this.buttonWidth - 3.1, 50);
                 this.separatorButton.Frame = new CGRect(this.buttonWidth - 3.1, 0, this.buttonWidth + 6.1, 50);
                 this.returnButton.Frame = new CGRect(this.toolbarView.Frame.Size.Width - this.buttonWidth + 3, 0, this.buttonWidth, 50);
                 this.lineView1.Frame = new CGRect(0, 0, this.toolbarView.Frame.Size.Width, 0.5f);
                 this.lineView2.Frame = new CGRect(0, this.toolbarView.Frame.Size.Height - 0.5f, this.toolbarView.Frame.Size.Width, 0.5f);
                 this.lineView3.Frame = new CGRect(this.minusButton.Frame.Right - 0.5f, 0, 0.5, this.minusButton.Frame.Size.Height);
                 this.lineView4.Frame = new CGRect(this.returnButton.Frame.Left, 0, 0.5, this.minusButton.Frame.Size.Height);
             }
         }
     }
 }

        private void ReturnButton_TouchDown(object sender, EventArgs e)
        {
            // Implement your return button logic here
            Console.WriteLine("Return button pressed");
        }

        private void MinusButton_TouchDown(object sender, EventArgs e)
        {
            // Implement your minus button logic here
            Console.WriteLine("Minus button pressed");
        }

        private void SeparatorButton_TouchDown(object sender, EventArgs e)
        {
            // Implement your separator button logic here
            Console.WriteLine("Separator button pressed");
        }
    }

    public enum ReturnType
    {
        Default,
        Done,
        Next,
        Go
    }
#endif

}

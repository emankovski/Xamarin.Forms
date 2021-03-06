using System.ComponentModel;
using System.Drawing;
#if __UNIFIED__
using UIKit;

#else
using MonoTouch.UIKit;
#endif

namespace Xamarin.Forms.Platform.iOS
{
	public class FrameRenderer : VisualElementRenderer<Frame>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
				SetupLayer();
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName || e.PropertyName == Xamarin.Forms.Frame.OutlineColorProperty.PropertyName ||
				e.PropertyName == Xamarin.Forms.Frame.HasShadowProperty.PropertyName)
				SetupLayer();
		}

		void SetupLayer()
		{
			Layer.CornerRadius = 5;
			if (Element.BackgroundColor == Color.Default)
				Layer.BackgroundColor = UIColor.White.CGColor;
			else
				Layer.BackgroundColor = Element.BackgroundColor.ToCGColor();

			if (Element.HasShadow)
			{
				Layer.ShadowRadius = 5;
				Layer.ShadowColor = UIColor.Black.CGColor;
				Layer.ShadowOpacity = 0.8f;
				Layer.ShadowOffset = new SizeF();
			}
			else
				Layer.ShadowOpacity = 0;

			if (Element.OutlineColor == Color.Default)
				Layer.BorderColor = UIColor.Clear.CGColor;
			else
			{
				Layer.BorderColor = Element.OutlineColor.ToCGColor();
				Layer.BorderWidth = 1;
			}

			Layer.RasterizationScale = UIScreen.MainScreen.Scale;
			Layer.ShouldRasterize = true;
		}
	}
}
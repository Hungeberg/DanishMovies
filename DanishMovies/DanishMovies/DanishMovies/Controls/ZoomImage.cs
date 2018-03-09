using System;
using Xamarin.Forms;

namespace DanishMovies.Controls
{
    public class ZoomImage : Image
    {
        private const double MIN_SCALE = 1;
        private const double MAX_SCALE = 4;
        private const double OVERSHOOT = 0.15;
        private double StartScale, LastScale;
        private double LastX, LastY;

        public ZoomImage()
        {
            var pinch = new PinchGestureRecognizer();
            pinch.PinchUpdated += OnPinchUpdated;
            GestureRecognizers.Add(pinch);

            var pan = new PanGestureRecognizer();
            pan.PanUpdated += OnPanUpdated;
            GestureRecognizers.Add(pan);

            var tap = new TapGestureRecognizer { NumberOfTapsRequired = 2 };
            tap.Tapped += OnTapped;
            GestureRecognizers.Add(tap);

            SizeChanged += ZoomImage_SizeChanged;

            Initialize();
        }

        private void Initialize()
        {
            Scale = MIN_SCALE;
            TranslationX = TranslationY = 0;
            AnchorX = AnchorY = 0.5;
        }

        public void ResetZoom()
        {
            this.ScaleTo(MIN_SCALE, 250, Easing.CubicInOut);
            this.TranslateTo(0, 0, 250, Easing.CubicInOut);
            Initialize();
        }

        private void ZoomImage_SizeChanged(object sender, EventArgs e)
        {
            ResetZoom();
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            Scale = MIN_SCALE;
            TranslationX = TranslationY = 0;
            AnchorX = AnchorY = 0;
            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        private void OnTapped(object sender, EventArgs e)
        {
            if (Scale > MIN_SCALE)
            {
                ResetZoom();
            }
            else
            {
                AnchorX = AnchorY = 0.5; //TODO tapped position
                this.ScaleTo(MAX_SCALE, 250, Easing.CubicInOut);
            }
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (Scale == MIN_SCALE) return; // Ignore pan if image is not zoomed!

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    LastX = (1 - AnchorX) * Width;
                    LastY = (1 - AnchorY) * Height;
                    break;
                case GestureStatus.Running:
                    AnchorX = Clamp(1 - (LastX + e.TotalX) / Width, 0, 1);
                    AnchorY = Clamp(1 - (LastY + e.TotalY) / Height, 0, 1);
                    break;
            }
        }

        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            switch (e.Status)
            {
                case GestureStatus.Started:
                    LastScale = e.Scale;
                    StartScale = Scale;
                    AnchorX = e.ScaleOrigin.X;
                    AnchorY = e.ScaleOrigin.Y;
                    break;
                case GestureStatus.Running:
                    if (e.Scale < 0 || Math.Abs(LastScale - e.Scale) > (LastScale * 1.3) - LastScale)
                    {
                        // e.Scale sometimes returns wildly different values from 
                        // one update to the next. This causes flickering 
                        // By removing values that are too far off, we smooth that out. 
                        return;
                    }
                    LastScale = e.Scale;
                    var current = Scale + (e.Scale - 1) * StartScale;
                    Scale = Clamp(current, MIN_SCALE * (1 - OVERSHOOT), MAX_SCALE * (1 + OVERSHOOT));
                    break;
                case GestureStatus.Completed:
                    if (Scale > MAX_SCALE)
                        this.ScaleTo(MAX_SCALE, 250, Easing.SpringOut);
                    else if (Scale < MIN_SCALE)
                        this.ScaleTo(MIN_SCALE, 250, Easing.SpringOut);
                    break;
            }
        }

        private T Clamp<T>(T value, T minimum, T maximum) where T : IComparable
        {
            if (value.CompareTo(minimum) < 0)
                return minimum;
            else if (value.CompareTo(maximum) > 0)
                return maximum;
            else
                return value;
        }
    }
}

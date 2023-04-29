using Godot;

namespace PurpleCable
{
    public partial class ColorAnimation : SimpleAnimation
    {
        private readonly Color _originalColor = Colors.White;

        private Sprite2D Sprite2D => Node as Sprite2D;

        public Color EndColor = Colors.White;

        public ColorAnimation(Sprite2D sprite)
            : base(sprite)
        {
            _originalColor = Sprite2D.Modulate;
        }

        protected override void SetEndValue()
        {
            Sprite2D.Modulate = EndColor;
        }

        protected override bool MustUpdate()
        {
            return Sprite2D.Modulate.Equals(EndColor);
        }

        protected override void UpdateValue(double deltaTime, float t)
        {
            Sprite2D.Modulate = ColorEx.Lerp(Sprite2D.Modulate, EndColor, t);
        }

        public override void Reset()
        {
            base.Reset();

            Sprite2D.Modulate = _originalColor;
        }
    }
}

using Godot;

// https://stackoverflow.com/questions/64550864/calculate-a-3d-trajectory-by-start-point-end-point-and-height

namespace PurpleCable
{
    public class Trajectory
    {
        private readonly Linear _line;

        public float A { get; private set; }

        public float B { get; private set; }

        public float C { get; private set; }

        public Trajectory(Vector3 sp, Vector3 ep, float maxHeight)
        {
            _line = new Linear(sp, ep);

            Vector3 ap = (ep + sp) / 2;
            ap.Y = sp.Y > ep.Y ? sp.Y + maxHeight : ep.Y + maxHeight;

            float a1 = -sp.X * sp.X + ap.X * ap.X;
            float b1 = -sp.X + ap.X;
            float c1 = -sp.Y + ap.Y;

            float a2 = -ap.X * ap.X + ep.X * ep.X;
            float b2 = -ap.X + ep.X;
            float c2 = -ap.Y + ep.Y;

            float bm = -(b2 / b1);
            float a3 = bm * a1 + a2;
            float c3 = bm * c1 + c2;

            float a = c3 / a3;
            float b = (c1 - a1 * a) / b1;
            float c = sp.Y - a * sp.X * sp.X - b * sp.X;

            A = a;
            B = b;
            C = c;
        }

        public Vector3 Point(float x)
        {
            Vector3 p = _line.GetX(x);
            p.Y = A * p.X * p.X + B * p.X + C;
            return p;
        }

        public class Linear
        {
            private readonly Vector3 r0, v;

            public Linear(Vector3 r0, Vector3 r1)
            {
                this.r0 = r0;
                v = r1 - r0;
            }

            public Vector3 Point(float t) => r0 + t * v;

            public Vector3 GetX(float x) => r0 + (x - r0.X) / v.X * v;
        }
    }
}
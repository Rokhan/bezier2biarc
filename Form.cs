﻿using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace BiArcTutorial
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        public static PointF AsPointF(Vector2 v)
        {
            return new PointF(v.X, v.Y);
        }

        // Test curves
        // http://polymathprogrammer.com/2011/02/06/bezier-curve-inflection-points/

        // No inflexion point
        CubicBezier b1 = new CubicBezier(
            new Vector2(100, 500), new Vector2(150, 100), new Vector2(500, 150), new Vector2(350, 350));
        CubicBezier b2 = new CubicBezier(
            new Vector2(100, 500), new Vector2(250, 350), new Vector2(450, 350), new Vector2(500, 500));

        // One inflexion point
        CubicBezier b3 = new CubicBezier(
            new Vector2(150, 500), new Vector2(100, 100), new Vector2(500, 350), new Vector2(350, 150));

        // Two inflexion point
        CubicBezier b4 = new CubicBezier(
            new Vector2(100, 500), new Vector2(350, 100), new Vector2(100, 200), new Vector2(500, 400));


        protected override void OnPaint(PaintEventArgs e)
        {
            var bezier = b4;

            var biarcs = Algorithm.ApproxCubicBezier(bezier, 5, 1);

            Graphics g = e.Graphics;

            // Draw the original bezier
            g.DrawBezier(new Pen(Color.Black, 2),
                AsPointF(bezier.P1), AsPointF(bezier.C1), AsPointF(bezier.C2), AsPointF(bezier.P2));

            // The full approximation circles for better understanding
            foreach (var biarc in biarcs)
            {
                g.DrawEllipse(new Pen(Color.Green, 1),
                    biarc.A1.C.X - biarc.A1.r, biarc.A1.C.Y - biarc.A1.r, 2 * biarc.A1.r, 2 * biarc.A1.r);
                g.DrawEllipse(new Pen(Color.Green, 1),
                    biarc.A2.C.X - biarc.A2.r, biarc.A2.C.Y - biarc.A2.r, 2 * biarc.A2.r, 2 * biarc.A2.r);
            }

            // The approximation biarcs
            foreach (var biarc in biarcs)
            { 
                g.DrawArc(new Pen(Color.Red, 2),
                    biarc.A1.C.X - biarc.A1.r, biarc.A1.C.Y - biarc.A1.r, 2 * biarc.A1.r, 2 * biarc.A1.r, 
                    biarc.A1.startAngle * 180.0f / (float)Math.PI, biarc.A1.sweepAngle * 180.0f / (float)Math.PI);
                g.DrawArc(new Pen(Color.Red, 1),
                    biarc.A2.C.X - biarc.A2.r, biarc.A2.C.Y - biarc.A2.r, 2 * biarc.A2.r, 2 * biarc.A2.r, 
                    biarc.A2.startAngle * 180.0f / (float)Math.PI, biarc.A2.sweepAngle * 180.0f / (float)Math.PI);
            }    
        
        }
    }
}

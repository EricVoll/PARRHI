using FanucControllerLibrary.DataTypes;
using PARRHI.Objects.Points;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARRHI.HelperClasses
{
    public class ForwardKinematics
    {

        private Matrix phi1(double q1)
        {
            return new Matrix(new double[,] { { cos(q1), -sin(q1), 0 }, { sin(q1), cos(q1), 0 }, { 0, 0, 1 } });
        }
        private Matrix phi2(double q2)
        {
            return new Matrix(new double[,] { { cos(q2), 0, sin(q2) }, { 0, 1, 0 }, { -sin(q2), 0, cos(q2) } });
        }
        private Matrix phi3(double q3)
        {
            return new Matrix(new double[,] { { cos(q3), 0, -sin(q3) }, { 0, 1, 0 }, { sin(q3), 0, cos(q3) } });
        }
        private Matrix phi4(double q4)
        {
            return new Matrix(new double[,] { { 1, 0, 0 }, { 0, cos(q4), sin(q4) }, { 0, -sin(q4), cos(q4) } });
        }
        private Matrix phi5(double q5)
        {
            return new Matrix(new double[,] { { cos(q5), 0, -sin(q5) }, { 0, 1, 0 }, { sin(q5), 0, cos(q5) } });
        }
        private Matrix phi6(double q6)
        {
            return new Matrix(new double[,] { { 1, 0, 0 }, { 0, cos(q6), sin(q6) }, { 0, -sin(q6), cos(q6) } });
        }

        private Point L1 => new Point(50, 0, 330);
        private Point L2 => new Point(0, 0, 440);
        private Point L3 => new Point(100, 0, 35);
        private Point L4 => new Point(320, 0, 0);
        private Point L5 => new Point(80, 0, 0);
        private Point L6 => new Point(150, 0, 0);

        private Func<double, Matrix>[] phi;
        private Func<double, Matrix>[] Phi
        {
            get
            {
                if (phi == null)
                {
                    phi = new Func<double, Matrix>[]
                        {
                            phi1,phi2,phi3,phi4,phi5,phi6
                        };
                }
                return phi;
            }
        }
        private Point[] robotJointLenghts;
        private Point[] RobotJointLenghts
        {
            get
            {
                if (robotJointLenghts == null) robotJointLenghts = new Point[] { L1, L2, L3, L4, L5, L6 };
                return robotJointLenghts;

            }
        }



        private double cos(double angle)
        {
            return Math.Cos(angle);
        }
        private double sin(double angle)
        {
            return Math.Sin(angle);
        }


        /// <summary>
        /// Calculates all Jont positions with the joint angles as input
        /// <para>ONLY VALID FOR THE Cr7 Fanuc</para>
        /// </summary>
        /// <param name="JointAngles"></param>
        /// <returns></returns>
        public Point[] CaluclateForwardKinematics(Vector6 JointAngles)
        {
            for (int i = 0; i < 6; i++)
                JointAngles[i] = JointAngles[i] / 180 * Math.PI;                                        //transform all values to radiants

            int dim = 6;
            Point[] jointPositions = new Point[dim];


            //Construct Tranformation Matrices
            Matrix[] Phin = new Matrix[dim];
            for (int i = 0; i < dim; i++)
                Phin[i] = Phi[i](JointAngles[i]);                                                       //Coord tranformation matrix from CoordSys n to n-1

            //Construct part vectors step by step
            Point[] x = new Point[dim + 1];
            x[0] = new Point(0, 0, 0);
            for (int i = 1; i < dim + 1; i++)
                x[i] = Phin[dim - i] * (RobotJointLenghts[dim - i] + x[i - 1]);                         //Starting from the TCP going backwards to the base and transforming each L_i vector + the "payload" we allready transformed from prev. coord systems

            //Construct absolute tranformation matrices
            Matrix[] phi0n = new Matrix[dim];                                                           //Coord transformation matrix from CoordSys n to 0
            phi0n[0] = Phin[0];
            for (int i = 1; i < dim; i++)
                phi0n[i] = phi0n[i - 1] * Phin[i];

            //Calculate joint positions by subtracting the Transformed part (in 0-coord sys) from the TCP
            for (int i = 0; i < dim; i++)
                jointPositions[i] = x.Last() - phi0n[i] * (RobotJointLenghts[i] + x[dim - 1 - i]);

            //Unity is stupid and uses y as a z axis.
            for (int i = 0; i < dim; i++)
            {
                var bufferX = jointPositions[i].X;
                var bufferZ = jointPositions[i].Z;
                jointPositions[i].X = jointPositions[i].Y;
                jointPositions[i].Y = bufferZ;
                jointPositions[i].Z = bufferX;
            }

            return jointPositions;
        }
    }
}

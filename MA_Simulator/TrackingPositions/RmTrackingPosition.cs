using MA_Simulator.Enums;
using MA_Simulator.Models;
using MA_Simulator.TrackingPositions.Base;
using MA_Simulator.TrackingPositions.Models;
using System.Runtime.InteropServices;

namespace MA_Simulator.TrackingPositions
{
    public class RmTrackingPosition : TrackingPositionBase<RmTrackingMessage>
    {
        public double _decreaseFactorDimension { get; set; } = 1.0;

        public RmTrackingPosition(int positionNo, int port, int internalMessageId) : base(port, internalMessageId)
        {
            PositionName = $"RM{positionNo}";
        }

        public override void Accept(TrackingBillet billet)
        {
            base.Accept(billet);

            _billet!.Status = BilletStatus.EnteringRM;
            Console.WriteLine($"The billet {_billet.PlcSemiproductCode} has status {Enum.GetName(_billet.Status)} at {PositionName}.");
        }

        public override void Release()
        {
            if (_billet != null)
            {
                _billet.Status = BilletStatus.ExitedRM;
                Console.WriteLine($"The billet {_billet.PlcSemiproductCode} has status {Enum.GetName(_billet.Status)} at {PositionName}.");

                double originalDiameter = _billet.Dimension;
                double originalLength = _billet.Length;

                // Apply percentile reduction to diameter
                _billet.Dimension = originalDiameter * (1 - _decreaseFactorDimension);

                // Maintain constant volume for a cylinder: V = π * r² * h
                // So: newLength = originalVolume / (π * (newDiameter/2)²)
                double originalVolume = CalculateVolume(originalDiameter, originalLength);

                _billet.Length = CalculateLength(originalVolume, _billet.Dimension);
            }

            base.Release();
        }

        private double CalculateLength(double volume, double diameter)
        {
            return Math.Round((volume / (Math.PI * Math.Pow(diameter / 2, 2))) + 0.005, 2);
        }

        private static double CalculateVolume(double diameter, double length)
        {
            return Math.Round((Math.PI * Math.Pow(diameter / 2, 2) * length) + 0.005, 2);
        }

        public override void ConstructMesssage()
        {
            if(_billet != null)
            {
                _positionMessage = new RmTrackingMessage
                {
                    Name = _billet.Name,
                    L1TrackingId = _billet.L1TrackingId,
                    PlcSemiproductCode = _billet.PlcSemiproductCode,
                    SemiproductNo = _billet.SemiproductNo
                };
            }
        }

        public override void Process()
        {
            ConstructMesssage();
            base.Process();
        }
    }
}

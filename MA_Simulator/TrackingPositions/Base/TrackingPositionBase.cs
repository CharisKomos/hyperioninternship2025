using MA_MessageLib;
using MA_Simulator.Models;
using MessageLib;

namespace MA_Simulator.TrackingPositions.Base
{
    public abstract class TrackingPositionBase<TPositionMessage> : ITrackingPosition
    {
        #region Fields/Properties
        internal readonly MessageSender _messageSender;
        internal readonly int _internalMessageId;

        internal TrackingBillet? _billet;
        internal bool _isPositionProcessed;
        internal TPositionMessage? _positionMessage;

        public string PositionName { get; set; } = String.Empty;
        #endregion

        #region Constructor
        public TrackingPositionBase(int port = 0, int internalMessageId = 0)
        {
            _messageSender = new MessageSender(port);
            _internalMessageId = internalMessageId;
        }
        #endregion

        #region Public Methods
        public virtual void Accept(TrackingBillet billet)
        {
            _billet = billet;
            _isPositionProcessed = false;
        }

        public virtual bool CanAccept()
        {
            return _billet == null;
        }

        public virtual bool CanRelease()
        {
            return _isPositionProcessed;
        }

        public virtual TrackingBillet? BilletInPosition()
        {
            return _billet;
        }

        public virtual void Release()
        {
            _billet = null;
        }

        public virtual Task SendMessageAsync()
        {
            //await _messageSender.SendMessageAsync(Message.FromObject(_positionMessage, _internalMessageId));
            _isPositionProcessed = true;

            return Task.CompletedTask;
        }

        public virtual void ConstructMesssage() { }
        public virtual async void Process()
        {
            if (_billet != null)
            {
                await SendMessageAsync();
            }
        }
        #endregion
    }
}
